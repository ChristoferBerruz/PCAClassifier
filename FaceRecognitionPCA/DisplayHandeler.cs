using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionPCA
{
    /// <summary>
    /// Provides basic display methods for front end
    /// </summary>
    class DisplayHandeler
    {
        /// <summary>
        /// Resizes a Bitmap to fit a certain component.
        /// </summary>
        /// <param name="bmp"> Bitmap to be resized.</param>
        /// <param name="width"> Width of the component. </param>
        /// <param name="height"> Height of the component. </param>
        /// <returns></returns>
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
