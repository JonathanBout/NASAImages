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
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(APOD));
			MainPicture = new PictureBox();
			NextButton = new Button();
			PreviousButton = new Button();
			DownloadButton = new Button();
			DatePicker = new DateTimePicker();
			DownloadHDButton = new Button();
			((System.ComponentModel.ISupportInitialize)MainPicture).BeginInit();
			SuspendLayout();
			// 
			// MainPicture
			// 
			resources.ApplyResources(MainPicture, "MainPicture");
			MainPicture.Name = "MainPicture";
			MainPicture.TabStop = false;
			// 
			// NextButton
			// 
			resources.ApplyResources(NextButton, "NextButton");
			NextButton.Name = "NextButton";
			NextButton.UseVisualStyleBackColor = true;
			NextButton.Click += NextButton_Click;
			// 
			// PreviousButton
			// 
			resources.ApplyResources(PreviousButton, "PreviousButton");
			PreviousButton.Name = "PreviousButton";
			PreviousButton.UseVisualStyleBackColor = true;
			PreviousButton.Click += PreviousButton_Click;
			// 
			// DownloadButton
			// 
			resources.ApplyResources(DownloadButton, "DownloadButton");
			DownloadButton.Name = "DownloadButton";
			DownloadButton.UseVisualStyleBackColor = true;
			DownloadButton.Click += DownloadButton_Click;
			// 
			// DatePicker
			// 
			resources.ApplyResources(DatePicker, "DatePicker");
			DatePicker.MaxDate = new DateTime(2022, 3, 31, 0, 0, 0, 0);
			DatePicker.MinDate = new DateTime(1995, 6, 20, 0, 0, 0, 0);
			DatePicker.Name = "DatePicker";
			DatePicker.Value = new DateTime(2022, 3, 31, 0, 0, 0, 0);
			DatePicker.ValueChanged += DatePicker_ValueChanged;
			// 
			// DownloadHDButton
			// 
			resources.ApplyResources(DownloadHDButton, "DownloadHDButton");
			DownloadHDButton.Name = "DownloadHDButton";
			DownloadHDButton.UseVisualStyleBackColor = true;
			DownloadHDButton.Click += DownloadHDButton_Click;
			// 
			// APOD
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(DownloadHDButton);
			Controls.Add(DatePicker);
			Controls.Add(DownloadButton);
			Controls.Add(PreviousButton);
			Controls.Add(NextButton);
			Controls.Add(MainPicture);
			DoubleBuffered = true;
			Name = "APOD";
			((System.ComponentModel.ISupportInitialize)MainPicture).EndInit();
			ResumeLayout(false);
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