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
    class ATTFace : IComparable
    {
        /// <summary>
        /// Full PathName to retrieve the Bitmap object later for display
        /// </summary>
        public String PathName { set; get; }

        /// <summary>
        /// Similarity score (Euclidean distance) required to evaluate similarity
        /// </summary>
        public double Similarity { set; get; }

        /// <summary>
        /// Unique identifier to be able to tell whether we used it or not
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// We only care about the array representation of the image
        /// </summary>
        public double[] ImageVector { set; get; }


        /// <summary>
        /// The image in the new base space
        /// </summary>
        public double[] ImageVectorTransformed { set; get; }


        /// <summary>
        /// Multiple ATTFaces may share the same PersonIdentifier
        /// </summary>
        public int PersonIdentifier { set; get; }


        public ATTFace()
        {

        }

        public ATTFace(String PathName, int ID, double[] ImageVector, int PersonIdentifier)
        {
            this.PathName = PathName;
            this.ID = ID;
            this.ImageVector = ImageVector;
            this.PersonIdentifier = PersonIdentifier;
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
                return this.CompareTo(OtherFace.Similarity);
            }
            else
            {
                throw new ArgumentException("Object is not an ATTFace");
            }
        }

        public int CompareTo(double otherSimilarity)
        {
            if(Similarity < otherSimilarity)
            {
                return -1;
            }

            if(Similarity == otherSimilarity)
            {
                return 0;
            }

            return 1;
        }
    }
}
