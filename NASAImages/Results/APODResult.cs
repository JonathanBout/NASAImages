using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NASAImages.Results
{
	public class APODResult
	{
		public string Copyright		{ get; set; } = "";
		public string Date			{ get; set; } = "";
		public string Explanation	{ get; set; } = "";
		public string HDUrl			{ get; set; } = "";
		public string Title			{ get; set; } = "";
		public string Url			{ get; set; } = "";

		public Image GetImage()
		{
			Uri uri = new(Url);
			HttpClient downloadClient = new();
			var result = downloadClient.GetAsync(uri).GetAwaiter().GetResult();
			Image image = Image.FromStream(result.Content.ReadAsStream());
			return image;
		}
	}
}