using System.Text.Json.Serialization;

namespace Backend.Models
{
	public class VideoAlternative
	{
		[JsonPropertyName("quality")]
		public string Quality { get; set; } = string.Empty;

		[JsonPropertyName("url")]
		public string Url { get; set; } = string.Empty;
	}
}