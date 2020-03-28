using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionPCA
{
    class DisplayHandeler
    {
        public static double[] BitmapToArray(Bitmap bmp)
        {
            double[] arr = new double[bmp.Height * bmp.Width];
            int k = 0;
            for(int i = 0; i < bmp.Height; i++)
            {
                for(int j = 0; j < bmp.Width; j++)
                {
                    arr[k] = Convert.ToInt32(bmp.GetPixel(i, j).R);
                    k++;
                }
            }
            return arr;
        }

        /// <summary>
        /// This method assumes Bitmap to be symetric
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static Bitmap ArrayToBitmap(double[] arr)
        {
            double max = arr.Max();
            double min = arr.Min();
            int n = (int)Math.Ceiling(Math.Sqrt(arr.Length));
            Bitmap bmp = new Bitmap(n, n);
            int k = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    int valueOfPixel = (int)(255 * (arr[k] - min) / (max - min));
                    bmp.SetPixel(i, j, Color.FromArgb(valueOfPixel, valueOfPixel, valueOfPixel));
                    k++;
                }
            }
            return bmp;
            
        }

        public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using(Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }
            return result;
        }
    }
}
