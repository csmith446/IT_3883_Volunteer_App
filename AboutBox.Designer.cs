namespace IT_3883_Volunteer_App
{
    partial class AboutBox
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
            this.AboutPictureBox = new System.Windows.Forms.PictureBox();
            this.DevelopersLabel = new System.Windows.Forms.Label();
            this.VersionNumberLabel = new System.Windows.Forms.Label();
            this.CopyrightInformationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AboutPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.AboutPictureBox.BackgroundImage = global::IT_3883_Volunteer_App.Properties.Resources.AboutImage;
            this.AboutPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.AboutPictureBox.Location = new System.Drawing.Point(30, 20);
            this.AboutPictureBox.Name = "pictureBox1";
            this.AboutPictureBox.Size = new System.Drawing.Size(72, 72);
            this.AboutPictureBox.TabIndex = 0;
            this.AboutPictureBox.TabStop = false;
            // 
            // lblDevelopers
            // 
            this.DevelopersLabel.AutoSize = true;
            this.DevelopersLabel.Location = new System.Drawing.Point(118, 33);
            this.DevelopersLabel.Name = "lblDevelopers";
            this.DevelopersLabel.Size = new System.Drawing.Size(65, 13);
            this.DevelopersLabel.TabIndex = 1;
            this.DevelopersLabel.Text = "[developers]";
            // 
            // lblVersionNumber
            // 
            this.VersionNumberLabel.AutoSize = true;
            this.VersionNumberLabel.Location = new System.Drawing.Point(118, 48);
            this.VersionNumberLabel.Name = "lblVersionNumber";
            this.VersionNumberLabel.Size = new System.Drawing.Size(89, 13);
            this.VersionNumberLabel.TabIndex = 2;
            this.VersionNumberLabel.Text = "[version] Release";
            // 
            // lblCopyrightInformation
            // 
            this.CopyrightInformationLabel.AutoSize = true;
            this.CopyrightInformationLabel.Location = new System.Drawing.Point(118, 63);
            this.CopyrightInformationLabel.Name = "lblCopyrightInformation";
            this.CopyrightInformationLabel.Size = new System.Drawing.Size(56, 13);
            this.CopyrightInformationLabel.TabIndex = 3;
            this.CopyrightInformationLabel.Text = "[copyright]";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 112);
            this.Controls.Add(this.CopyrightInformationLabel);
            this.Controls.Add(this.VersionNumberLabel);
            this.Controls.Add(this.DevelopersLabel);
            this.Controls.Add(this.AboutPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AboutPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AboutPictureBox;
        private System.Windows.Forms.Label DevelopersLabel;
        private System.Windows.Forms.Label VersionNumberLabel;
        private System.Windows.Forms.Label CopyrightInformationLabel;


    }
}