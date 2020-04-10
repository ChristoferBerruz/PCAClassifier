using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionPCA
{
    /// <summary>
    /// Provides loading data utilities for dealing with ATTFaces
    /// </summary>
    class DataUnit
    {
        static ATTFace face = new ATTFace();
        static readonly int WIDTH = face.WIDTH;
        static readonly int HEIGHT = face.HEIGHT;


        /// <summary>
        /// Read files and generates a training dataset
        /// </summary>
        /// <param name="trainingPath"> Directory Path of where Training data is located</param>
        /// <returns> </returns>
        public static List<ATTFace> GetTrainingData(String trainingPath)
        {
            List < ATTFace > Faces = new List<ATTFace>();
            DirectoryInfo di = new DirectoryInfo(trainingPath);
            FileInfo[] files = di.GetFiles("*.jpg");
            foreach (FileInfo fileInfo in files)
            {
                String fileName = fileInfo.Name;
                String[] parts = fileName.Split('_');
                int personID = int.Parse(parts[0].Substring(1, parts[0].Length - 1));
                Bitmap faceBitmap = new Bitmap(trainingPath + "\\" + fileName);
                double[] ImageAsArray = BitmapToArray(faceBitmap);
                ATTFace face = new ATTFace(fileName, personID, ImageAsArray);
                Faces.Add(face);
            }
            return Faces;
        }



        /// <summary>
        /// Generate TestingData
        /// </summary>
        /// <param name="testingPath"></param>
        /// <returns></returns>
        public static List<ATTFace> GetTestingData(String testingPath)
        {
            return GetTrainingData(testingPath);
        }


        /// <summary>
        /// Converts a Bitmap to an array. Flatteing happens row-wise.
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static double[] BitmapToArray(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            double[] arr = new double[width * height];
            int k = 0;
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    arr[k] = 1.0 * Convert.ToInt32(bmp.GetPixel(i, j).R);
                    k++;
                }
            }
            return arr;
        }


        /// <summary>
        /// Converts and array to a Bitmap. Because it deals with a PCA, it reconstructs the Bitmap making sure to retrieve
        /// original dimension of images being trained.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static Bitmap ArrayToBitmap(double[] arr)
        {
            Bitmap bmp = new Bitmap(WIDTH, HEIGHT);
            int k = 0;
            double max = arr.Max();
            double min = arr.Min();
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    int valueOfPixel = (int)(255 * (arr[k] - min) / (max - min));
                    bmp.SetPixel(i, j, Color.FromArgb(valueOfPixel, valueOfPixel, valueOfPixel));
                    k++;
                }
            }
            return bmp;
        }

        

        /// <summary>
        /// Gets an ATTFaces from a specified filePath
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static ATTFace GetATTFace(String filePath)
        {
            String[] parts = filePath.Split('\\');
            String fileName = parts[parts.Length - 1];
            String workingName = fileName.Substring(0, fileName.Length - 4);
            String[] personAndNumber = workingName.Split('_');
            int personId = int.Parse(personAndNumber[0].Substring(1, personAndNumber[0].Length - 1));
            double[] imageAsVector = BitmapToArray(new Bitmap(filePath));
            return new ATTFace(fileName, personId, imageAsVector);
        }
    }
}
