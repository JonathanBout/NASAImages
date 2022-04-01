namespace NASAImages
{
    partial class APOD
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APOD));
            this.MainPicture = new System.Windows.Forms.PictureBox();
            this.NextButton = new System.Windows.Forms.Button();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            this.DownloadHDButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MainPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPicture
            // 
            resources.ApplyResources(this.MainPicture, "MainPicture");
            this.MainPicture.Name = "MainPicture";
            this.MainPicture.TabStop = false;
            // 
            // NextButton
            // 
            resources.ApplyResources(this.NextButton, "NextButton");
            this.NextButton.Name = "NextButton";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // PreviousButton
            // 
            resources.ApplyResources(this.PreviousButton, "PreviousButton");
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.UseVisualStyleBackColor = true;
            this.PreviousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // DownloadButton
            // 
            resources.ApplyResources(this.DownloadButton, "DownloadButton");
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // DatePicker
            // 
            resources.ApplyResources(this.DatePicker, "DatePicker");
            this.DatePicker.MaxDate = new System.DateTime(2022, 3, 31, 0, 0, 0, 0);
            this.DatePicker.MinDate = new System.DateTime(1995, 6, 20, 0, 0, 0, 0);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Value = new System.DateTime(2022, 3, 31, 0, 0, 0, 0);
            this.DatePicker.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // DownloadHDButton
            // 
            resources.ApplyResources(this.DownloadHDButton, "DownloadHDButton");
            this.DownloadHDButton.Name = "DownloadHDButton";
            this.DownloadHDButton.UseVisualStyleBackColor = true;
            this.DownloadHDButton.Click += new System.EventHandler(this.DownloadHDButton_Click);
            // 
            // APOD
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DownloadHDButton);
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.PreviousButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.MainPicture);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "APOD";
            ((System.ComponentModel.ISupportInitialize)(this.MainPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox MainPicture;
        private Button NextButton;
        private Button PreviousButton;
        private Button DownloadButton;
        private DateTimePicker DatePicker;
        private Button DownloadHDButton;
    }
}