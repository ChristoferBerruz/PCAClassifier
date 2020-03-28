using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapack;
using System.Drawing;

namespace FaceRecognitionPCA
{
    class PCA
    {
        private IMatrix ImagesAsMatrix = null;
        private int validID = 1;
        readonly int BITMAPDIM = 100;
        private IMatrix EigenTransformationMatrix = null;
        private IMatrix EigenVectors = null;

        public double[] MeanFaceAsArray { get; private set; } = null;

        public List<ATTFace> DataSet { get; } = new List<ATTFace>();


        public PCA(String folderName)
        {
            this.LoadDataset(folderName); //Loading the List of ATT Faces
        }

        /// <summary>
        /// Trains the PCA Classifier with the data in the given folder
        /// </summary>
        /// <param name="NewDimensionality"></param>
        public void Train(int NewDimensionality)
        {
            LoadImageMatrix();
            if (ImagesAsMatrix == null) throw new Exception("Images were not loaded to Matrix correctly");
            IMatrix Adjusted = MeanAdjust(this.ImagesAsMatrix);
            IMatrix Covariance = CalculateCovarianceMatrix(Adjusted);
            IEigenvalueDecomposition Decomposition = Covariance.GetEigenvalueDecomposition(); //Eigenvalues are sorted, they are stack column-wise
            EigenVectors = GetDesiredCountEigenvector(Decomposition, NewDimensionality);
            EigenTransformationMatrix = ImagesAsMatrix.Multiply(EigenVectors);
            Console.WriteLine(EigenTransformationMatrix.Rows);
            Console.WriteLine(EigenTransformationMatrix.Columns);
            CalculateProjectedImages(); //Calculating the projection of all images in the new basis space
        }


        public IMatrix NormalizeByColumn(IMatrix M)
        {
            IMatrix R = new Matrix(M.Rows, M.Columns);
            double[] columnSum = new double[M.Columns];
            for(int j = 0; j < M.Columns; j++)
            {
                double sum = 0.0;
                for(int i = 0; i < M.Rows; i++)
                {
                    sum += M[i, j];
                }
                columnSum[j] = sum;
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

        public ATTFace FindBestMatch(double[] imageAsVector)
        {
            double[] meanAdjusted = SubstractArrays(imageAsVector, MeanFaceAsArray);
            double[] projectedImage = ComputeProjection(meanAdjusted);
            this.CalculateEuclideanDistances(projectedImage); //Up to this point all images have have computed a similarity score
            DataSet.Sort();
            return DataSet[0];
        }

        /// <summary>
        /// Returns one eigen vector that represents an eigen face.
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>

        public double[] GetEigenFace(int col)
        {
            return EigenTransformationMatrix.GetColumnAsArray(col);
        }

        public double[] SubstractArrays(double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length) throw new ArgumentException("Array length are not the same!");
            double[] res = new double[v1.Length];
            for(int i = 0; i<v1.Length; i++)
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
            IMatrix VectorMean = Images.Mean(axis:1);
            this.MeanFaceAsArray = VectorMean.Flatten(); //Storing the meanFaceVector
            IMatrix MeanAdjusted = Images.AddVector(VectorMean.Negate(), axis:1); //Substract from each column 
            return MeanAdjusted;
        }

        /// <summary>
        /// Calculates the covariance matrix of the dataset
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
            for(int j = 0; j < EigenTransformationMatrix.Columns; j++)
            {
                double proj = 0.0;
                for(int i = 0; i < EigenTransformationMatrix.Rows; i++)
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
            foreach(ATTFace Person in DataSet)
            {
                double[] projected = ComputeProjection(Person.ImageVector);
                Person.ImageVectorTransformed = projected;
            }
        }

        /// <summary>
        /// Calculates the Euclidean Distance of all images to a test image. All of this in the projected space.
        /// </summary>
        /// <param name="projectedTestImage"></param>
        private void CalculateEuclideanDistances(double[] projectedTestImage)
        {
            foreach(ATTFace Person in DataSet)
            {
                Person.Similarity = EuclideanDistance(Person.ImageVectorTransformed, projectedTestImage);
            }
        }

        public double[] ProjectAndReconstruct(double[] imageAsVector)
        {
            double[] coeficients = ComputeProjection(imageAsVector);
            double[] reconstructed = new double[EigenTransformationMatrix.Rows]; //Getting back to the original space;
            for(int i = 0; i < EigenTransformationMatrix.Rows; i++)
            {
                double val = 0.0;
                for(int j = 0; j < EigenTransformationMatrix.Columns; j++)
                {
                    val += EigenTransformationMatrix[i, j] * coeficients[j];
                }
                reconstructed[i] = val;
            }
            return reconstructed;
        }

        private double EuclideanDistance(double[] v1, double[] v2)
        {
            double dist = 0.0;
            for(int i = 0; i < v1.Length; i++)
            {
                dist += (v1[i] - v2[i])*(v1[i] - v2[i]);
            }
            return dist;
        }

        public IMatrix GetDesiredCountEigenvector(IEigenvalueDecomposition Decomposition, int NumEigenVectors)
        {
            int[] columnIndexes = new int[NumEigenVectors];
            int[] rowIndexes = new int[Decomposition.EigenvectorMatrix.Rows];
            int k = 0;
            int eigenvaluesCount = Decomposition.RealEigenvalues.Length;
            for (int i = eigenvaluesCount - NumEigenVectors; i < eigenvaluesCount; i++)
            {
                columnIndexes[k] = i;
                k++;
            }

            for(int i = 0; i < Decomposition.EigenvectorMatrix.Rows; i++)
            {
                rowIndexes[i] = i;
            }

            IMatrix DesiredVectors = Decomposition.EigenvectorMatrix.Submatrix(rowIndexes, columnIndexes);
            return ReorderColumns(DesiredVectors);
        }

        /// <summary>
        /// Flips all the columns of a given matrix
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        private IMatrix ReorderColumns(IMatrix M)
        {
            Matrix Res = new Matrix(M.Rows, M.Columns);
            for(int j = M.Columns - 1; j >= 0; j--)
            {
                for(int i = 0; i < M.Rows; i++)
                {
                    Res[i, M.Columns - j - 1] = M[i, j]; 
                }
            }
            return Res;
        }

        public double DotProduct(double[] arr1, double[] arr2)
        {
            if(arr1.Length != arr2.Length) { throw new ArgumentException("Arrays must be same length"); }
            double res = 0;
            for(int i = 0; i < arr1.Length; i++)
            {
                res += arr1[i] * arr2[i];
            }
            return res;

        }
        
        private void LoadDataset(String pathName)
        {
            DirectoryInfo di = new DirectoryInfo(pathName);
            FileInfo[] files = di.GetFiles("*.jpg");
            foreach (FileInfo fileInfo in files)
            {
                String fileName = fileInfo.Name;
                String[] parts = fileName.Split('_');
                int PersonIdentifier = int.Parse(parts[0].Substring(1, parts[0].Length - 1));
                Bitmap faceBitmap = new Bitmap(pathName +"\\"+ fileName);
                faceBitmap = DisplayHandeler.ResizeBitmap(faceBitmap, BITMAPDIM, BITMAPDIM);
                double[] ImageAsArray = DisplayHandeler.BitmapToArray(faceBitmap);
                ATTFace face = new ATTFace(fileName, this.validID, ImageAsArray, PersonIdentifier);
                DataSet.Add(face);
            }
        }

        private void LoadImageMatrix() 
        {
            this.ImagesAsMatrix = new Matrix(BITMAPDIM * BITMAPDIM, this.DataSet.Count);
            for(int j = 0; j < ImagesAsMatrix.Columns; j++)
            {
                double[] columnVector = DataSet[j].ImageVector;
                for(int i = 0; i < ImagesAsMatrix.Rows; i++)
                {
                    ImagesAsMatrix[i, j] = columnVector[i];
                }
            }
        }
    }
}
