namespace FaceRecognitionPCA
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picMeanFace = new System.Windows.Forms.PictureBox();
            this.picEigenFace1 = new System.Windows.Forms.PictureBox();
            this.picEigenFace2 = new System.Windows.Forms.PictureBox();
            this.picEigenFace3 = new System.Windows.Forms.PictureBox();
            this.picEigenFace4 = new System.Windows.Forms.PictureBox();
            this.picTestImage = new System.Windows.Forms.PictureBox();
            this.picMeanAdjusted = new System.Windows.Forms.PictureBox();
            this.picReconstructed = new System.Windows.Forms.PictureBox();
            this.picBestMatch1 = new System.Windows.Forms.PictureBox();
            this.picBestMatch2 = new System.Windows.Forms.PictureBox();
            this.picBestMatch3 = new System.Windows.Forms.PictureBox();
            this.picBestMatch4 = new System.Windows.Forms.PictureBox();
            this.picBestMatch5 = new System.Windows.Forms.PictureBox();
            this.picBestMatch6 = new System.Windows.Forms.PictureBox();
            this.btnCalculateEigenFaces = new System.Windows.Forms.Button();
            this.btnTestImage = new System.Windows.Forms.Button();
            this.btnComputeAccuracy = new System.Windows.Forms.Button();
            this.lblScore1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTestImage = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picMeanFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEigenFace1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEigenFace2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEigenFace3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEigenFace4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTestImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMeanAdjusted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReconstructed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch6)).BeginInit();
            this.SuspendLayout();
            // 
            // picMeanFace
            // 
            this.picMeanFace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMeanFace.Location = new System.Drawing.Point(25, 178);
            this.picMeanFace.Name = "picMeanFace";
            this.picMeanFace.Size = new System.Drawing.Size(130, 147);
            this.picMeanFace.TabIndex = 0;
            this.picMeanFace.TabStop = false;
            // 
            // picEigenFace1
            // 
            this.picEigenFace1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEigenFace1.Location = new System.Drawing.Point(80, 378);
            this.picEigenFace1.Name = "picEigenFace1";
            this.picEigenFace1.Size = new System.Drawing.Size(162, 186);
            this.picEigenFace1.TabIndex = 1;
            this.picEigenFace1.TabStop = false;
            // 
            // picEigenFace2
            // 
            this.picEigenFace2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEigenFace2.Location = new System.Drawing.Point(281, 378);
            this.picEigenFace2.Name = "picEigenFace2";
            this.picEigenFace2.Size = new System.Drawing.Size(162, 186);
            this.picEigenFace2.TabIndex = 2;
            this.picEigenFace2.TabStop = false;
            // 
            // picEigenFace3
            // 
            this.picEigenFace3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEigenFace3.Location = new System.Drawing.Point(482, 378);
            this.picEigenFace3.Name = "picEigenFace3";
            this.picEigenFace3.Size = new System.Drawing.Size(162, 186);
            this.picEigenFace3.TabIndex = 3;
            this.picEigenFace3.TabStop = false;
            // 
            // picEigenFace4
            // 
            this.picEigenFace4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEigenFace4.Location = new System.Drawing.Point(683, 378);
            this.picEigenFace4.Name = "picEigenFace4";
            this.picEigenFace4.Size = new System.Drawing.Size(162, 186);
            this.picEigenFace4.TabIndex = 4;
            this.picEigenFace4.TabStop = false;
            // 
            // picTestImage
            // 
            this.picTestImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picTestImage.Location = new System.Drawing.Point(215, 26);
            this.picTestImage.Name = "picTestImage";
            this.picTestImage.Size = new System.Drawing.Size(154, 177);
            this.picTestImage.TabIndex = 5;
            this.picTestImage.TabStop = false;
            // 
            // picMeanAdjusted
            // 
            this.picMeanAdjusted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMeanAdjusted.Location = new System.Drawing.Point(394, 26);
            this.picMeanAdjusted.Name = "picMeanAdjusted";
            this.picMeanAdjusted.Size = new System.Drawing.Size(154, 177);
            this.picMeanAdjusted.TabIndex = 6;
            this.picMeanAdjusted.TabStop = false;
            // 
            // picReconstructed
            // 
            this.picReconstructed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picReconstructed.Location = new System.Drawing.Point(564, 26);
            this.picReconstructed.Name = "picReconstructed";
            this.picReconstructed.Size = new System.Drawing.Size(154, 177);
            this.picReconstructed.TabIndex = 7;
            this.picReconstructed.TabStop = false;
            // 
            // picBestMatch1
            // 
            this.picBestMatch1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBestMatch1.Location = new System.Drawing.Point(754, 26);
            this.picBestMatch1.Name = "picBestMatch1";
            this.picBestMatch1.Size = new System.Drawing.Size(82, 85);
            this.picBestMatch1.TabIndex = 8;
            this.picBestMatch1.TabStop = false;
            // 
            // picBestMatch2
            // 
            this.picBestMatch2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBestMatch2.Location = new System.Drawing.Point(858, 26);
            this.picBestMatch2.Name = "picBestMatch2";
            this.picBestMatch2.Size = new System.Drawing.Size(82, 85);
            this.picBestMatch2.TabIndex = 9;
            this.picBestMatch2.TabStop = false;
            // 
            // picBestMatch3
            // 
            this.picBestMatch3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBestMatch3.Location = new System.Drawing.Point(961, 26);
            this.picBestMatch3.Name = "picBestMatch3";
            this.picBestMatch3.Size = new System.Drawing.Size(82, 85);
            this.picBestMatch3.TabIndex = 10;
            this.picBestMatch3.TabStop = false;
            // 
            // picBestMatch4
            // 
            this.picBestMatch4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBestMatch4.Location = new System.Drawing.Point(754, 162);
            this.picBestMatch4.Name = "picBestMatch4";
            this.picBestMatch4.Size = new System.Drawing.Size(82, 85);
            this.picBestMatch4.TabIndex = 11;
            this.picBestMatch4.TabStop = false;
            // 
            // picBestMatch5
            // 
            this.picBestMatch5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBestMatch5.Location = new System.Drawing.Point(858, 163);
            this.picBestMatch5.Name = "picBestMatch5";
            this.picBestMatch5.Size = new System.Drawing.Size(82, 84);
            this.picBestMatch5.TabIndex = 12;
            this.picBestMatch5.TabStop = false;
            // 
            // picBestMatch6
            // 
            this.picBestMatch6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBestMatch6.Location = new System.Drawing.Point(961, 162);
            this.picBestMatch6.Name = "picBestMatch6";
            this.picBestMatch6.Size = new System.Drawing.Size(82, 85);
            this.picBestMatch6.TabIndex = 13;
            this.picBestMatch6.TabStop = false;
            // 
            // btnCalculateEigenFaces
            // 
            this.btnCalculateEigenFaces.Location = new System.Drawing.Point(25, 12);
            this.btnCalculateEigenFaces.Name = "btnCalculateEigenFaces";
            this.btnCalculateEigenFaces.Size = new System.Drawing.Size(130, 39);
            this.btnCalculateEigenFaces.TabIndex = 14;
            this.btnCalculateEigenFaces.Text = "Calculate EFs";
            this.btnCalculateEigenFaces.UseVisualStyleBackColor = true;
            this.btnCalculateEigenFaces.Click += new System.EventHandler(this.btnCalculateEigenFaces_Click);
            // 
            // btnTestImage
            // 
            this.btnTestImage.Location = new System.Drawing.Point(25, 57);
            this.btnTestImage.Name = "btnTestImage";
            this.btnTestImage.Size = new System.Drawing.Size(130, 36);
            this.btnTestImage.TabIndex = 15;
            this.btnTestImage.Text = "Test Image";
            this.btnTestImage.UseVisualStyleBackColor = true;
            this.btnTestImage.Click += new System.EventHandler(this.btnTestImage_Click);
            // 
            // btnComputeAccuracy
            // 
            this.btnComputeAccuracy.Location = new System.Drawing.Point(25, 106);
            this.btnComputeAccuracy.Name = "btnComputeAccuracy";
            this.btnComputeAccuracy.Size = new System.Drawing.Size(130, 54);
            this.btnComputeAccuracy.TabIndex = 16;
            this.btnComputeAccuracy.Text = "Compute Accuracy";
            this.btnComputeAccuracy.UseVisualStyleBackColor = true;
            // 
            // lblScore1
            // 
            this.lblScore1.AutoSize = true;
            this.lblScore1.Location = new System.Drawing.Point(756, 120);
            this.lblScore1.Name = "lblScore1";
            this.lblScore1.Size = new System.Drawing.Size(46, 17);
            this.lblScore1.TabIndex = 17;
            this.lblScore1.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Mean Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 567);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Eigen Vector 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(313, 567);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Eigen Vector 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(513, 567);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Eigen Vector 3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(713, 567);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "Eigen Vector 4";
            // 
            // lblTestImage
            // 
            this.lblTestImage.AutoSize = true;
            this.lblTestImage.Location = new System.Drawing.Point(253, 206);
            this.lblTestImage.Name = "lblTestImage";
            this.lblTestImage.Size = new System.Drawing.Size(78, 17);
            this.lblTestImage.TabIndex = 23;
            this.lblTestImage.Text = "Test Image";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(420, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "Mean Adjusted";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(587, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 34);
            this.label8.TabIndex = 25;
            this.label8.Text = "Reconstructed\r\nfrom Eigen Faces";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 610);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblTestImage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblScore1);
            this.Controls.Add(this.btnComputeAccuracy);
            this.Controls.Add(this.btnTestImage);
            this.Controls.Add(this.btnCalculateEigenFaces);
            this.Controls.Add(this.picBestMatch6);
            this.Controls.Add(this.picBestMatch5);
            this.Controls.Add(this.picBestMatch4);
            this.Controls.Add(this.picBestMatch3);
            this.Controls.Add(this.picBestMatch2);
            this.Controls.Add(this.picBestMatch1);
            this.Controls.Add(this.picReconstructed);
            this.Controls.Add(this.picMeanAdjusted);
            this.Controls.Add(this.picTestImage);
            this.Controls.Add(this.picEigenFace4);
            this.Controls.Add(this.picEigenFace3);
            this.Controls.Add(this.picEigenFace2);
            this.Controls.Add(this.picEigenFace1);
            this.Controls.Add(this.picMeanFace);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picMeanFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEigenFace1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEigenFace2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEigenFace3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEigenFace4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTestImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMeanAdjusted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReconstructed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBestMatch6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picMeanFace;
        private System.Windows.Forms.PictureBox picEigenFace1;
        private System.Windows.Forms.PictureBox picEigenFace2;
        private System.Windows.Forms.PictureBox picEigenFace3;
        private System.Windows.Forms.PictureBox picEigenFace4;
        private System.Windows.Forms.PictureBox picTestImage;
        private System.Windows.Forms.PictureBox picMeanAdjusted;
        private System.Windows.Forms.PictureBox picReconstructed;
        private System.Windows.Forms.PictureBox picBestMatch1;
        private System.Windows.Forms.PictureBox picBestMatch2;
        private System.Windows.Forms.PictureBox picBestMatch3;
        private System.Windows.Forms.PictureBox picBestMatch4;
        private System.Windows.Forms.PictureBox picBestMatch5;
        private System.Windows.Forms.PictureBox picBestMatch6;
        private System.Windows.Forms.Button btnCalculateEigenFaces;
        private System.Windows.Forms.Button btnTestImage;
        private System.Windows.Forms.Button btnComputeAccuracy;
        private System.Windows.Forms.Label lblScore1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTestImage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

