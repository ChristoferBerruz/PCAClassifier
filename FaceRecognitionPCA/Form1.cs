using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            Classifier.Train(50);
            Bitmap averageFace = DataUnit.ArrayToBitmap(Classifier.MeanFaceAsArray);
            ShowImage(averageFace, picMeanFace);
            for(int i = 1; i < 6; i++)
            {
                double[] eigen = Classifier.GetEigenFace(i - 1);
                Bitmap face = DataUnit.ArrayToBitmap(eigen);
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
                case 5:
                    return picEigenFace5;
                default:
                    throw new ArgumentException("Not a valid integer for Picture boxes");
            }
        }

        private PictureBox GetBestMatchBox(int i)
        {
            switch(i)
            {
                case 1:
                    return picBestMatch1;
                case 2:
                    return picBestMatch2;
                case 3:
                    return picBestMatch3;
                case 4:
                    return picBestMatch4;
                case 5:
                    return picBestMatch5;
                case 6:
                    return picBestMatch6;
                default:
                    throw new ArgumentException("Not a valid integer for Best Match Boxes");
            }
        }

        private Label GetBestMatchLabel(int i)
        {
            switch(i)
            {
                case 1:
                    return lblScore1;
                case 2:
                    return lblScore2;
                case 3:
                    return lblScore3;
                case 4:
                    return lblScore4;
                case 5:
                    return lblScore5;
                case 6:
                    return lblScore6;
                default:
                    throw new ArgumentException("Invalid Label number!");
            }
        }

        private void ShowBestFaces(List<ATTFace> bestFaces)
        {
            CultureInfo ci = new CultureInfo("en-us");
            for (int i = 0; i < bestFaces.Count; i++)
            {
                Bitmap bmp = bestFaces[i].getImagesAsBmp();
                ShowImage(bmp, GetBestMatchBox(i + 1));
                Label lbl = GetBestMatchLabel(i + 1);
                lbl.Text = bestFaces[i].Closeness.ToString("E03", ci) + "\n" + bestFaces[i].FileName;
            }
        }

        private void btnTestImage_Click(object sender, EventArgs e)
        {
            if (Classifier == null || !Classifier.IsTrained)
            {
                MessageBox.Show("Classifier not trained. Please press Calculate EFs first!");
            }
            else
            {
                OpenFileDialog Dialog = new OpenFileDialog();
                Dialog.Filter = "jpeg files (*.jpg)|*.jpg|(*.gif)|gif||";
                if (DialogResult.OK == Dialog.ShowDialog())
                {
                    ATTFace face = DataUnit.GetATTFace(Dialog.FileName);
                    this.TestBitmap = face.getImagesAsBmp();
                    ShowImage(this.TestBitmap, picTestImage);
                    FileInfo fileInfo = new FileInfo(Dialog.FileName);
                    lblTestImage.Text = fileInfo.Name;
                    ///Showing mean adjusted
                    Bitmap meanAdjusted = DataUnit.ArrayToBitmap(Classifier.SubstractArrays(face.ImageVector, Classifier.MeanFaceAsArray));
                    ShowImage(meanAdjusted, picMeanAdjusted);
                    ////Showing the reconstruction from eigen faces
                    double[] projected = Classifier.ProjectAndReconstruct(face.ImageVector);
                    Bitmap bmp_projected = DataUnit.ArrayToBitmap(projected);
                    ShowImage(bmp_projected, picReconstructed);

                    ////Trying to find best matches
                    List<ATTFace> bestFaces = new List<ATTFace>();
                    int guessID = 0;
                    Classifier.FindBestMatch(face.ImageVector, DecisionType.ClossestNeighbor, ClossenessMeasure.Euclidean, ref bestFaces, ref guessID, onlyID: false);
                    ShowBestFaces(bestFaces);
                }
            }

        }

        private void ShowImage(Bitmap bmp, PictureBox pictureBox)
        {
            Bitmap FittedBitmap = DisplayHandeler.ResizeBitmap(bmp, pictureBox.Width, pictureBox.Height);
            pictureBox.Image = FittedBitmap;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnComputeAccuracy_Click(object sender, EventArgs e)
        {
            if (Classifier == null || !Classifier.IsTrained)
            {
                MessageBox.Show("Classifier is not trained! Please click Calculate EFs!");
            }
            else
            {
                String testingPath = "C:\\Users\\chris\\Documents\\UNIVERSITY OF BRIDGEPORT\\2020SP Semester\\CPEG 585 - Computer Vision\\ATTFaceDataSet\\Testing";
                double acc = Classifier.Accuracy(testingPath, DecisionType.ClossestNeighbor, ClossenessMeasure.Euclidean, 3);
                MessageBox.Show("Accuracy: " + (acc * 100) + "%");
            }
        }
    }
}
