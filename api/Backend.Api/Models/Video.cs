using System.Text.Json.Serialization;

namespace Backend.Models
{
	public class Video
	{
		[JsonPropertyName("title")]
		public string Title { get; set; } = string.Empty;

		[JsonPropertyName("alternatives")]
		public List<VideoAlternative> Alternatives { get; set; } = new();

		[JsonPropertyName("type")]
		public string Type { get; set; } = string.Empty;

		[JsonPropertyName("url")]
		public string Url { get; set; } = string.Empty;
	}
}