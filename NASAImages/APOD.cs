using NASAImages.Results;

namespace NASAImages
{
	public partial class APOD : Form
	{
		readonly APIConnection connection;
		readonly string APIKey = "IGaMqQV6XKQIBQFN5jtdI2Jcbt8WyacP0whXpjvO";
		public APODResult? result;
		public DateTime currentDate
        {
			get
            {
				return _currentDate;
            }set
            {
				_currentDate = value;
				DatePicker.Value = value;
            }
        }
		DateTime _currentDate = DateTime.Today;
		bool isRunning = false;
		int switches = 0;
		bool forward;
		public APOD()
		{
			InitializeComponent();
			DatePicker.MaxDate = DateTime.Today;
			DatePicker.Format = DateTimePickerFormat.Custom;
			DatePicker.CustomFormat = "dd-MM-yyyy";
			connection = new APIConnection("https://api.nasa.gov/");
			SetImageFromAPI();
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			currentDate += TimeSpan.FromDays(1);
			forward = true;
			SetImageFromAPI();
		}

		private void PreviousButton_Click(object sender, EventArgs e)
		{
			currentDate -= TimeSpan.FromDays(1);
			forward = false;
			SetImageFromAPI();
		}

		public void SetImageFromAPI()
		{
			if (isRunning) { return; }
			isRunning = true;

			Cursor.Current = Cursors.WaitCursor;
			result = connection.GetImageRequest("planetary/apod", APIKey, currentDate);

			if (result is null || string.IsNullOrEmpty(result.Url))
			{
				Cursor.Current = Cursors.Default;
				MessageBox.Show("Failed to retrieve the image.");
				isRunning = false;
				return;
			}

			if (currentDate >= DateTime.Today)
			{
				NextButton.Hide();
				currentDate = DateTime.Today;
			}
			else
			{
				NextButton.Show();
			}
			DatePicker.Value = currentDate;
			try
			{
				MainPicture.Image = result.GetImage();
			}
			catch (Exception)
			{
				MainPicture.Image = MainPicture.ErrorImage;
				DownloadButton.Hide();
				DownloadHDButton.Hide();
				isRunning = false;
				if (switches < 3)
                {
					switches++;
					if (!forward)
						currentDate -= TimeSpan.FromDays(1);
					else
						currentDate += TimeSpan.FromDays(1);
					SetImageFromAPI();
                }else
                {
					Cursor.Current = Cursors.Default;
				}
				return;
			}
			switches = 0;
			Cursor.Current = Cursors.Default;
			Width = MainPicture.Image.Width;
			Height = MainPicture.Image.Height + 25;
			Screen screen = Screen.FromControl(this);
			Height = Math.Min(Height, screen.WorkingArea.Height);
			Width = Math.Min(Width, screen.WorkingArea.Width);
			MainPicture.Refresh();
			DownloadButton.Show();
			DownloadHDButton.Show();
			isRunning = false;
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			if (result is null || result.Url is null)
			{
				MessageBox.Show("Can't download this picture.");
				return;
			}
			string fullSavePath = Path.Join(GetSavePath(), result.Title.Replace(' ', '-') + ".*");
			connection.Download(result.Url, fullSavePath);
			Cursor.Current = Cursors.Default;
			MessageBox.Show("Download Completed!");
		}
		private void DownloadHDButton_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			if (result is null || result.HDUrl is null)
			{
				MessageBox.Show("Can't download this picture.");
				return;
			}
			string fullSavePath = Path.Join(GetSavePath(), result.Title.Replace(' ', '-') + ".png");
			connection.Download(result.HDUrl, fullSavePath);
			Cursor.Current = Cursors.Default;
			MessageBox.Show("Download Completed!");
		}

        static string GetSavePath()
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.RootFolder = Environment.SpecialFolder.MyPictures;
			dialog.ShowNewFolderButton = true;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				return dialog.SelectedPath;
			}
			else
			{
				return "";
			}
		}

		private void DatePicker_ValueChanged(object sender, EventArgs e)
		{
			currentDate = DatePicker.Value;
			SetImageFromAPI();
		}
    }
}