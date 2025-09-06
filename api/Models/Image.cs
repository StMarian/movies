using System.Text.Json.Serialization;

namespace Backend.Models
{
	public class Image
	{
		[JsonPropertyName("url")]
		public string Url { get; set; } = string.Empty;

		[JsonPropertyName("h")]
		public int Height { get; set; }

		[JsonPropertyName("w")]
		public int Width { get; set; }
	}
}