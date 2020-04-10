using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapack;

namespace FaceRecognitionPCA
{
    /// <summary>
    /// Class to encapsulate properties of an ATTFace usefukl for PCA
    /// </summary>
    class ATTFace : IComparable, ICloneable
    {

        public readonly int WIDTH = 92;

        public readonly int HEIGHT = 112;
        /// <summary>
        /// FileName for display
        /// </summary>
        /// 
        public String FileName { set; get; }

        /// <summary>
        /// Similarity score (Euclidean distance) required to evaluate similarity
        /// </summary>
        public double Closeness { set; get; }

        /// <summary>
        /// Unique identifier of the person
        /// </summary>
        public int personID { set; get; }

        /// <summary>
        /// We only care about the array representation of the image
        /// </summary>
        public double[] ImageVector { set; get; }


        /// <summary>
        /// The image in the new base space
        /// </summary>
        public double[] ImageVectorTransformed { set; get; }


        public ATTFace()
        {

        }

        public ATTFace(String FileName, int personID, double[] ImageVector)
        {
            this.FileName = FileName;
            this.personID = personID;
            this.ImageVector = ImageVector;
        }


        public int CompareTo(Object obj)
        {
            if(obj == null)
            {
                return 1;
            }

            ATTFace OtherFace = obj as ATTFace;
            if(OtherFace != null)
            {
                return this.CompareTo(OtherFace.Closeness);
            }
            else
            {
                throw new ArgumentException("Object is not an ATTFace");
            }
        }

        public int CompareTo(double otherCloseness)
        {
            if(Closeness < otherCloseness)
            {
                return -1;
            }

            if(Closeness == otherCloseness)
            {
                return 0;
            }

            return 1;
        }


        /// <summary>
        /// Return original Bitmap from its image array.
        /// </summary>
        /// <returns></returns>
        public Bitmap getImagesAsBmp() 
        {
            Bitmap bmp = new Bitmap(WIDTH, HEIGHT);
            int k = 0; //To index the array
            for(int i = 0; i < WIDTH; i++)
            {
                for(int j = 0; j < HEIGHT; j++)
                {
                    int pix = (int)this.ImageVector[k];
                    bmp.SetPixel(i, j, Color.FromArgb(pix, pix, pix));
                    k++;
                }
            }
            return bmp;
        }

        public Object Clone()
        {
            ATTFace Copy = new ATTFace();
            Copy.Closeness = this.Closeness;
            Copy.ImageVector = this.ImageVector;
            Copy.ImageVectorTransformed = this.ImageVectorTransformed;
            Copy.personID = this.personID;
            Copy.FileName = this.FileName;
            return Copy;
        }
    }
}
