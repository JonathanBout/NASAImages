using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NASAImages.Results;
using Newtonsoft.Json;

namespace NASAImages
{
	public class APIConnection : IDisposable
	{
		public string URL { get; init; }
		private readonly HttpClient client;
		public APIConnection(string url)
		{
			URL = url;
			client = new HttpClient()
			{
				BaseAddress = new Uri(URL)
			};
		}

		public Task<HttpResponseMessage> GetRequestAsync(string endpoint) => client.GetAsync(endpoint);

		public async Task<APODData?> GetImageDataAsync(string endpoint, string apiKey, DateTime? date)
		{
			string fullEndpoint = URL + endpoint + $"?api_key={apiKey}" + (date is not null ? $"&date={date?.ToString("yyyy-MM-dd")}" : "");
			using var response = await GetRequestAsync(fullEndpoint);
			return JsonConvert.DeserializeObject<APODData>(await response.Content.ReadAsStringAsync());
		}

		public async Task DownloadImageAsync(APODData data, bool hd, string targetPath)
		{
			var url = hd ? data.HDUrl : data.Url;
			using var response = await client.GetAsync(url);
			using var stream = await response.Content.ReadAsStreamAsync();
			using var image = Image.FromStream(stream);
			string extension = Path.GetExtension(targetPath);
			var format = extension.ToLower() switch
			{
				".jpeg" or ".jpg" => ImageFormat.Jpeg,
				_ => ImageFormat.Png,
			};
			image.Save(targetPath, format);
		}

		public void Dispose()
		{
			client.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
