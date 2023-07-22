using NASAImages.Results;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace NASAImages
{
	public partial class APOD : Form
	{
		private readonly APIConnection connection;
		private readonly string APIKey = "IGaMqQV6XKQIBQFN5jtdI2Jcbt8WyacP0whXpjvO";
		private readonly SemaphoreSlim semaphore = new(1, 1);
		private DateTime? selectedDate;
		private APODData? currentImageData;
		public DateTime SelectedDate
		{
			get
			{
				return selectedDate ??= DateTime.Today;
			}
			set
			{
				selectedDate = value;
				if (value.Date == DateTime.Today)
				{
					NextButton.Enabled = false;
				} else
				{
					NextButton.Enabled = true;
				}
				DatePicker.Value = value;
			}
		}



		public APOD()
		{
			InitializeComponent();
			DatePicker.MaxDate = DateTime.Today;
			DatePicker.Format = DateTimePickerFormat.Custom;
			DatePicker.CustomFormat = "dd-MM-yyyy";
			connection = new APIConnection("https://api.nasa.gov/");
			SelectedDate = DateTime.Today;
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			_ = SetImageFromAPI();
		}

		private async void NextButton_Click(object sender, EventArgs e)
		{
			SelectedDate += TimeSpan.FromDays(1);
			await SetImageFromAPI();
		}
		private async void PreviousButton_Click(object sender, EventArgs e)
		{
			SelectedDate -= TimeSpan.FromDays(1);
			await SetImageFromAPI();
		}
		private async void DownloadButton_Click(object sender, EventArgs e) => await SaveImage(false);
		private async void DownloadHDButton_Click(object sender, EventArgs e) => await SaveImage(true);

		private async Task SaveImage(bool hd)
		{
			SetCursor(Cursors.WaitCursor);
			string? selectedUrl = hd ? currentImageData?.HDUrl : currentImageData?.Url;

			if (currentImageData is null || selectedUrl is null)
			{
				MessageBox.Show("Can't download this picture.");
				return;
			}

			var invalidChars = Path.GetInvalidFileNameChars();

			string fileName = new string(currentImageData.Title.Select(x => invalidChars.Contains(x) ? '-' : x).ToArray()).Replace(' ', '-');

			if (hd)
			{
				fileName += "-hd";
			}
			fileName += ".png";
			fileName = MultipleDashesRegex().Replace(fileName, "-");
			string fullSavePath = GetSavePath(fileName);
			if (!string.IsNullOrEmpty(fullSavePath))
			{
				await connection.DownloadImageAsync(currentImageData, hd, fullSavePath);
				Process.Start("explorer.exe", Directory.GetParent(fullSavePath)?.FullName ?? fullSavePath)
					.Dispose();
			}
			SetCursor(Cursors.Default);
		}

		private async void DatePicker_ValueChanged(object sender, EventArgs e)
		{
			SelectedDate = DatePicker.Value;
			await SetImageFromAPI();
		}

		public async Task SetImageFromAPI()
		{
			await semaphore.WaitAsync();
			SetCursor(Cursors.WaitCursor);
			try
			{
				currentImageData = await connection.GetImageDataAsync("planetary/apod", APIKey, SelectedDate);
				string? url = currentImageData?.Url;
				if (string.IsNullOrEmpty(url))
				{
					if (string.IsNullOrEmpty(currentImageData?.HDUrl) || currentImageData is null)
					{
						MessageBox.Show("Failed to retrieve the image.");
					} else
					{
						url = currentImageData.HDUrl;
					}
				}

				if (SelectedDate > DateTime.Today)
				{
					SelectedDate = DateTime.Today;
				}
				DatePicker.Value = SelectedDate;
				MainPicture.ImageLocation = url ?? "";
				Invoke(() =>
				{
					SetCursor(Cursors.Default);
					MainPicture.Refresh();
				});
			} finally
			{
				semaphore.Release();
			}
		}

		static string GetSavePath(string defaultFileName)
		{
			SaveFileDialog dialog = new()
			{
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
				AddExtension = true,
				DefaultExt = "png",
				Filter = "Portable Network Graphics (*.png)|*.png|Joint Photographic Experts Group (*.jpeg)|*.jpeg",
				FileName = defaultFileName
			};

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				return dialog.FileName;
			} else
			{
				return "";
			}
		}
		void SetCursor(Cursor cursor)
		{
			// make sure handle is created
			var h = Handle;
			h.ToString();
			Invoke(() =>
			{
				Cursor = cursor;
			});
		}

		[GeneratedRegex("-{2,}", RegexOptions.Compiled)]
		private static partial Regex MultipleDashesRegex();
	}
}
