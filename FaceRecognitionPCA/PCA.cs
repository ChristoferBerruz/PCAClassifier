using Mapack;
using System;
using System.Collections.Generic;

namespace FaceRecognitionPCA
{
    class PCA
    {
        private IMatrix ImagesAsMatrix = null;

        private IMatrix EigenTransformationMatrix = null;
        private IMatrix EigenVectors = null;

        public double[] MeanFaceAsArray { get; private set; } = null;

        public List<ATTFace> DataSet { set; get; }

        public bool IsTrained { get; private set; }


        private bool DataAvailable { set { } get { return DataSet.Count > 1; } }


        private int WIDTH { get; set; }


        private int HEIGHT { get; set; }

        public List<double[]> EigenVectorList { set; get; } = new List<double[]>();

        public List<double[]> EigenFaceList { set; get; } = new List<double[]>();

        public PCA(String folderName)
        {
            this.LoadDataset(folderName); //Loading the List of ATT Faces
        }

        /// <summary>
        /// Trains the PCA Classifier with the data in the given folder
        /// </summary>
        /// <param name="NewDimensionality"> Desired new vector space dimension</param>
        public void Train(int NewDimensionality)
        {
            LoadImageMatrix();
            if (ImagesAsMatrix == null) throw new Exception("Images were not loaded to Matrix correctly");
            IMatrix Adjusted = MeanAdjust(this.ImagesAsMatrix);
            IMatrix Covariance = CalculateCovarianceMatrix(Adjusted);
            IEigenvalueDecomposition Decomposition = Covariance.GetEigenvalueDecomposition(); //Eigenvalues are sorted, they are stack column-wise
            EigenVectors = GetDesiredCountEigenvector(Decomposition, NewDimensionality);
            EigenTransformationMatrix = ImagesAsMatrix.Multiply(EigenVectors);
            EigenTransformationMatrix = NormalizeByColumn(EigenTransformationMatrix);
            PopulateEigenFaceList();
            CalculateProjectedImages(); //Calculating the projection of all images in the new basis space
            this.IsTrained = true;
        }


        /// <summary>
        /// Normalizes a columns vectors of a column-vector composed Matrix.
        /// </summary>
        /// <param name="M"> Matrix</param>
        /// <returns></returns>
        public IMatrix NormalizeByColumn(IMatrix M)
        {
            IMatrix R = new Matrix(M.Rows, M.Columns);
            double[] columnSum = new double[M.Columns];
            for (int j = 0; j < M.Columns; j++)
            {
                double sum = 0.0;
                for (int i = 0; i < M.Rows; i++)
                {
                    sum += M[i, j] * M[i, j];
                }
                columnSum[j] = Math.Sqrt(sum);
            }
            for (int j = 0; j < M.Columns; j++)
            {
                for (int i = 0; i < M.Rows; i++)
                {
                    R[i, j] = M[i, j] / columnSum[j];
                }
            }
            return R;

        }


        /// <summary>
        /// Finds the Best Match(es) as a list, and provides a guessed Person ID.
        /// </summary>
        /// <param name="imageAsVector"> Image of the person to be classified.</param>
        /// <param name="decisionType"> Either ClossestNeighbor or KKN</param>
        /// <param name="measureType"> Either Euclidean or Projection. Determines which measurement is to be used.</param>
        /// <param name="BestFaces"> List of ATTFaces passed by reference. Must be empty. BestMatches added to the list if onlyID = false</param>
        /// <param name="guessID">int passed by reference. The class of the person images guessed by PCA.</param>
        /// <param name="neighborNumber"> How many neighbors to use for KNN. Default is 3</param>
        /// <param name="numberFaces">How many Best Matches to add to BestFaces. Default is 6</param>
        /// <param name="onlyID"> Whether ONLY the class of the person is of interest. If the BestFaces are also of interest, set to false/</param>
        public void FindBestMatch(double[] imageAsVector, DecisionType decisionType, ClossenessMeasure measureType, ref List<ATTFace> BestFaces, ref int guessID, int neighborNumber = 3, int numberFaces = 6, bool onlyID = true)
        {
            double[] meanAdjusted = SubstractArrays(imageAsVector, MeanFaceAsArray);
            double[] projectedImage = ComputeProjection(meanAdjusted);
            foreach (ATTFace Person in DataSet)
            {
                double similarity;
                if (measureType == ClossenessMeasure.Euclidean)
                {
                    similarity = EuclideanDistance(Person.ImageVectorTransformed, projectedImage);
                    Person.Closeness = similarity;
                }
                else if (measureType == ClossenessMeasure.Projection)
                {
                    similarity = DotProduct(Person.ImageVectorTransformed, projectedImage);
                    Person.Closeness = -similarity; //A maximization is transformed to a minimization by putting a -
                }
                else
                {
                    throw new ArgumentException("ClosenessMeasure is not supported!");
                }
            }

            DataSet.Sort();

            // ------------------------------------------ Retrieving best guess ------------------------------//
            if (decisionType == DecisionType.ClossestNeighbor)
            {
                guessID = DataSet[0].personID;
            }
            else if (decisionType == DecisionType.KNN)
            {
                IDictionary<int, int> Tally = new Dictionary<int, int>();
                int maxVotes = 0;
                guessID = -1;
                for (int i = 0; i < neighborNumber; i++)
                {
                    ATTFace Person = DataSet[i];
                    if (Tally.ContainsKey(Person.personID))
                    {
                        Tally[Person.personID]++;
                        if (Tally[Person.personID] > maxVotes)
                        {
                            //To be here, a personID needs at leats two votes
                            maxVotes = Tally[Person.personID];
                            guessID = Person.personID;
                        }
                    }
                    else
                    {
                        Tally.Add(Person.personID, 1);
                    }
                }

            }
            else { throw new ArgumentException("DecisionType not valid!"); }

            // ---------------------------- Populating the list onlyID = False ----------------//
            if (!onlyID)
            {
                for (int i = 0; i < numberFaces; i++)
                {
                    BestFaces.Add((ATTFace)DataSet[i].Clone());
                }
            }
        }

        /// <summary>
        /// Returns one eigen vector that represents an eigen face.
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public double[] GetEigenFace(int idx)
        {
            return EigenFaceList[idx];
        }


        /// <summary>
        /// Gets a column from the EigenTransformation Matrix
        /// </summary>
        /// <param name="idx">index of the column. As always, must be inside bounds.</param>
        /// <returns></returns>
        private double[] GetColumnFromEF(int idx)
        {
            double[] arr = new double[EigenTransformationMatrix.Rows];
            for (int i = 0; i < EigenTransformationMatrix.Rows; i++)
            {
                arr[i] = EigenTransformationMatrix[i, idx];
            }
            return arr;
        }


        /// <summary>
        /// Populates the EigenFaceList for easy retrieval to display
        /// </summary>
        private void PopulateEigenFaceList()
        {
            for (int j = 0; j < EigenTransformationMatrix.Columns; j++)
            {
                double[] arr = GetColumnFromEF(j);
                EigenFaceList.Add(arr);
            }
        }


        /// <summary>
        /// Substract arrays as v1  - v2
        /// </summary>
        /// <param name="v1"> Left array </param>
        /// <param name="v2"> Right array</param>
        /// <returns></returns>
        public double[] SubstractArrays(double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length) throw new ArgumentException("Array length are not the same!");
            double[] res = new double[v1.Length];
            for (int i = 0; i < v1.Length; i++)
            {
                res[i] = v1[i] - v2[i];
            }
            return res;
        }

        /// <summary>
        /// Calculates Mean adjusted values for all Images
        /// </summary>
        /// <param name="Images"></param>
        /// <returns></returns>
        public IMatrix MeanAdjust(IMatrix Images)
        {
            MeanFaceAsArray = CalculateMean(Images);
            IMatrix MeanAdjusted = SubstractMean(Images, MeanFaceAsArray);
            return MeanAdjusted;
        }


        /// <summary>
        /// Calculates Mean along columns
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        private double[] CalculateMean(IMatrix Images)
        {
            double[] arr = new double[Images.Rows];
            for (int i = 0; i < Images.Rows; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < Images.Columns; j++)
                {
                    sum += Images[i, j];
                }
                arr[i] = sum / Images.Columns;
            }
            return arr;
        }

        /// <summary>
        /// Substracts the mean for all columns
        /// </summary>
        /// <param name="Images"></param>
        /// <param name="mean"></param>
        /// <returns></returns>
        private IMatrix SubstractMean(IMatrix Images, double[] mean)
        {
            IMatrix Res = new Matrix(Images.Rows, Images.Columns);
            for (int j = 0; j < Res.Columns; j++)
            {
                for (int i = 0; i < Res.Rows; i++)
                {
                    Res[i, j] = Images[i, j] - mean[i];
                }
            }
            return Res;
        }

        /// <summary>
        /// Calculates the covariance matrix of the dataset in the lowest dimension possible.
        /// </summary>
        /// <param name="Images"></param>
        /// <returns></returns>
        public IMatrix CalculateCovarianceMatrix(IMatrix Images)
        {
            IMatrix Covariance = Images.Rows > Images.Columns ? Images.Transpose().Multiply(Images) : Images.Multiply(Images.Transpose());
            return Covariance;
        }

        /// <summary>
        /// This method computes the projection to onto the new feature space
        /// </summary>
        /// <param name="ImageAsVector"></param>
        /// <returns> double[] array with length = newDimension</returns>
        public double[] ComputeProjection(double[] ImageAsVector)
        {
            double[] arr = new double[EigenTransformationMatrix.Columns];
            for (int j = 0; j < EigenTransformationMatrix.Columns; j++)
            {
                double proj = 0.0;
                for (int i = 0; i < EigenTransformationMatrix.Rows; i++)
                {
                    proj += ImageAsVector[i] * EigenTransformationMatrix[i, j];
                }
                arr[j] = proj;
            }
            return arr;
        }


        /// <summary>
        /// Computes the projection of all persons images into the new dimensonality.
        /// </summary>
        private void CalculateProjectedImages()
        {
            foreach (ATTFace Person in DataSet)
            {
                double[] meanAdjusted = SubstractArrays(Person.ImageVector, MeanFaceAsArray);
                double[] projected = ComputeProjection(meanAdjusted);
                Person.ImageVectorTransformed = projected;
            }
        }


        /// <summary>
        /// Projects and Image onto the new eigen space and gets reconstructed using the eigen faces.
        /// </summary>
        /// <param name="imageAsVector"></param>
        /// <returns></returns>
        public double[] ProjectAndReconstruct(double[] imageAsVector)
        {
            double[] coeficients = ComputeProjection(SubstractArrays(imageAsVector, MeanFaceAsArray));

            double[] reconstructed = new double[EigenTransformationMatrix.Rows]; //Getting back to the original space;
            for (int i = 0; i < EigenTransformationMatrix.Rows; i++)
            {
                double val = 0.0;
                for (int j = 0; j < EigenTransformationMatrix.Columns; j++)
                {
                    val += EigenTransformationMatrix[i, j] * coeficients[j];
                }
                reconstructed[i] = val;
            }
            return reconstructed;
        }


        /// <summary>
        /// Normal Euclidean distance.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private double EuclideanDistance(double[] v1, double[] v2)
        {
            double dist = 0.0;
            for (int i = 0; i < v1.Length; i++)
            {
                dist += (v1[i] - v2[i]) * (v1[i] - v2[i]);
            }
            return dist;
        }


        /// <summary>
        /// Calculates accuracy of the PCA Classifier
        /// </summary>
        /// <param name="testingPath"> Location of the Testing Files</param>
        /// <param name="decision"> Whether ClossestNeighbor or KNN</param>
        /// <param name="measureType"> Wether Euclidean or Projection</param>
        /// <param name="numberNeighbs"> How many neighbors to take into account for KNN</param>
        /// <returns></returns>
        public double Accuracy(String testingPath, DecisionType decision, ClossenessMeasure measureType, int numberNeighbs)
        {
            List<ATTFace> testingFaces = DataUnit.GetTestingData(testingPath);
            double accu = 0;
            foreach (ATTFace testFace in testingFaces)
            {
                int guessID = 0;
                List<ATTFace> bestFaces = new List<ATTFace>();
                FindBestMatch(testFace.ImageVector, decision, measureType, ref bestFaces, ref guessID, neighborNumber: numberNeighbs);
                if (guessID == testFace.personID) accu += 1;
            }
            return accu / testingFaces.Count;
        }


        /// <summary>
        /// After solving the GEVP, we have N eigenvectors forming the new basis. Retrieves the numEigenVectors (e.g. 20) eigenvectors corresponding
        /// to the highest eigenvalues.
        /// </summary>
        /// <param name="Decomposition"> Decompositon of GEVP</param>
        /// <param name="numEigenVectors"> Desire subset of most significant eigen vectors</param>
        /// <returns></returns>
        public IMatrix GetDesiredCountEigenvector(IEigenvalueDecomposition Decomposition, int numEigenVectors)
        {
            double[] eigenVector = new double[Decomposition.EigenvectorMatrix.Rows];
            int eigenvaluesCount = Decomposition.RealEigenvalues.Length;
            IMatrix Res = new Matrix(Decomposition.EigenvectorMatrix.Rows, numEigenVectors);
            int k = 0;
            for (int j = eigenvaluesCount - 1; j > eigenvaluesCount - numEigenVectors - 1; j--)
            {
                double sum = 0.0;
                for (int i = 0; i < eigenVector.Length; i++)
                {
                    eigenVector[i] = Decomposition.EigenvectorMatrix[i, j];
                    sum += eigenVector[i] * eigenVector[i];
                    Res[i, k] = Decomposition.EigenvectorMatrix[i, j];
                }
                k++;
                EigenVectorList.Add(eigenVector);
            }
            return Res;
        }

        /// <summary>
        /// A dot product between two arrays.
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public double DotProduct(double[] arr1, double[] arr2)
        {
            if (arr1.Length != arr2.Length) { throw new ArgumentException("Arrays must be same length"); }
            double res = 0.0;
            for (int i = 0; i < arr1.Length; i++)
            {
                res += arr1[i] * arr2[i];
            }
            return res;

        }


        /// <summary>
        /// Populates the Dataset. It also sets parameters WIDTH, HEIGHT of the ATTFaces for reference in the PCA.
        /// </summary>
        /// <param name="pathName"> Path of the Training Dataset.</param>
        private void LoadDataset(String pathName)
        {
            this.DataSet = DataUnit.GetTrainingData(pathName);
            if (DataSet.Count >= 1)
            {
                WIDTH = DataSet[0].WIDTH;
                HEIGHT = DataSet[0].HEIGHT;
            }
        }


        /// <summary>
        /// Populates the ImageMatrix to be used when training the PCA
        /// </summary>
        private void LoadImageMatrix()
        {
            if (!DataAvailable) throw new ArgumentException("Data was not loaded correctly");
            this.ImagesAsMatrix = new Matrix(WIDTH * HEIGHT, this.DataSet.Count);
            for (int j = 0; j < ImagesAsMatrix.Columns; j++)
            {
                double[] columnVector = DataSet[j].ImageVector;
                for (int i = 0; i < ImagesAsMatrix.Rows; i++)
                {
                    ImagesAsMatrix[i, j] = columnVector[i];
                }
            }
        }
    }
}
