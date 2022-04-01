using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NASAImages.Results;
//using RestSharp;
using Newtonsoft.Json;

namespace NASAImages
{
    public class APIConnection
    {
        public string URL { get; init; }
        readonly HttpClient client;
        public APIConnection(string url)
        {
            URL = url;
            client = new HttpClient()
            {
                BaseAddress = new Uri(URL)
            };
        }

        public HttpResponseMessage GetRequest(string endpoint)
        {
            return client.GetAsync(endpoint).GetAwaiter().GetResult();
        }

        public APODResult? GetImageRequest(string endpoint, string apiKey, DateTime? date)
        {
            string fullEndpoint = URL + endpoint + $"?api_key={apiKey}" + (date is not null ? $"&date={date?.ToString("yyyy-MM-dd")}" : "");
            HttpResponseMessage response = GetRequest(fullEndpoint);
            HttpContent content = response.Content;
            using (StreamReader reader = new StreamReader(content.ReadAsStream()))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<APODResult>(json);
            }
        }

        public Image GetPicture(string endpoint, string apiKey, DateTime? date)
        {
            string fullEndpoint = URL + endpoint + $"?api_key={apiKey}" + (date is not null ? $"&date={date?.ToString("yyyy-MM-dd")}" : "");
            HttpResponseMessage response = GetRequest(fullEndpoint);
            HttpContent content = response.Content;
            return Image.FromStream(content.ReadAsStream());
        }

        public void Download(string url, string targetPath)
        {
            Uri uri = new Uri(url);
            HttpClient downloadClient = new HttpClient();
            var image = downloadClient.GetAsync(uri).GetAwaiter().GetResult();
            Image img = Image.FromStream(image.Content.ReadAsStream());
            img.Save(targetPath, ImageFormat.Png);
        }
    }
}
