using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mapack;

namespace FaceRecognitionPCA
{
    public partial class Form1 : Form
    {
        readonly int BITMAPDIM = 100;
        private Bitmap TestBitmap = null;
        private PCA Classifier;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculateEigenFaces_Click(object sender, EventArgs e)
        {
            String folderName = "C:\\Users\\chris\\Documents\\UNIVERSITY OF BRIDGEPORT\\2020SP Semester\\CPEG 585 - Computer Vision\\ATTFaceDataSet\\Training";
            Classifier = new PCA(folderName);
            Classifier.Train(30);
            Bitmap averageFace = DisplayHandeler.ArrayToBitmap(Classifier.MeanFaceAsArray);
            ShowImage(averageFace, picMeanFace);
            for(int i = 1; i < 5; i++)
            {
                double[] eigen = Classifier.GetEigenFace(i-1);
                Bitmap face = DisplayHandeler.ArrayToBitmap(eigen);
                ShowImage(face, GetEigenBox(i));
            }
        }

        private PictureBox GetEigenBox(int i)
        {
            switch(i)
            {
                case 1:
                    return picEigenFace1;
                case 2:
                    return picEigenFace2;
                case 3:
                    return picEigenFace3;
                case 4:
                    return picEigenFace4;
                default:
                    throw new ArgumentException("Not a valid integer for Picture boxes");
            }
        }

        private void btnTestImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Filter = "jpeg files (*.jpg)|*.jpg|(*.gif)|gif||";
            if(DialogResult.OK == Dialog.ShowDialog())
            {
                this.TestBitmap = DisplayHandeler.ResizeBitmap(new Bitmap(Dialog.FileName), BITMAPDIM, BITMAPDIM);
                ShowImage(this.TestBitmap, picTestImage);
                FileInfo fileInfo = new FileInfo(Dialog.FileName);
                lblTestImage.Text = fileInfo.Name;
                double[] testImageVector = DisplayHandeler.BitmapToArray(TestBitmap);

                //Showing mean adjusted
                Bitmap meanAdjusted = DisplayHandeler.ArrayToBitmap(Classifier.SubstractArrays(testImageVector, Classifier.MeanFaceAsArray));
                ShowImage(meanAdjusted, picMeanAdjusted);
                //Showing the reconstruction from eigen faces
                double[] projected = Classifier.ProjectAndReconstruct(testImageVector);
                Bitmap bmp_projected = DisplayHandeler.ArrayToBitmap(projected);
                ShowImage(bmp_projected, picReconstructed);

                //Trying to find best comparison
                ATTFace  bestMatch = Classifier.FindBestMatch(testImageVector);
                Bitmap face = DisplayHandeler.ArrayToBitmap(bestMatch.ImageVector);
                ShowImage(face, picBestMatch1);
            }

        }

        private void ShowImage(Bitmap bmp, PictureBox pictureBox)
        {
            Bitmap FittedBitmap = DisplayHandeler.ResizeBitmap(bmp, pictureBox.Width, pictureBox.Height);
            pictureBox.Image = FittedBitmap;
        }
    }
}
