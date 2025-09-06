using System.Text.Json.Serialization;

namespace Backend.Models
{
	public class Movie
	{
		[JsonPropertyName("body")]
		public string Body { get; set; } = string.Empty;

		[JsonPropertyName("cardImages")]
		public List<Image> CardImages { get; set; } = new();

		[JsonPropertyName("cast")]
		public List<Person> Cast { get; set; } = new();

		[JsonPropertyName("cert")]
		public string Cert { get; set; } = string.Empty;

		[JsonPropertyName("class")]
		public string Class { get; set; } = string.Empty;

		[JsonPropertyName("directors")]
		public List<Person> Directors { get; set; } = new();

		[JsonPropertyName("duration")]
		public int Duration { get; set; }

		[JsonPropertyName("genres")]
		public List<string> Genres { get; set; } = new();

		[JsonPropertyName("headline")]
		public string Headline { get; set; } = string.Empty;

		[JsonPropertyName("id")]
		public string Id { get; set; } = string.Empty;

		[JsonPropertyName("keyArtImages")]
		public List<Image> KeyArtImages { get; set; } = new();

		[JsonPropertyName("lastUpdated")]
		public string LastUpdated { get; set; } = string.Empty;

		[JsonPropertyName("quote")]
		public string Quote { get; set; } = string.Empty;

		[JsonPropertyName("rating")]
		public int Rating { get; set; }

		[JsonPropertyName("reviewAuthor")]
		public string ReviewAuthor { get; set; } = string.Empty;

		[JsonPropertyName("skyGoId")]
		public string SkyGoId { get; set; } = string.Empty;

		[JsonPropertyName("skyGoUrl")]
		public string SkyGoUrl { get; set; } = string.Empty;

		[JsonPropertyName("sum")]
		public string Sum { get; set; } = string.Empty;

		[JsonPropertyName("synopsis")]
		public string Synopsis { get; set; } = string.Empty;

		[JsonPropertyName("url")]
		public string Url { get; set; } = string.Empty;

		[JsonPropertyName("videos")]
		public List<Video> Videos { get; set; } = new();

		[JsonPropertyName("viewingWindow")]
		public ViewingWindow? ViewingWindow { get; set; }

		[JsonPropertyName("year")]
		public string Year { get; set; } = string.Empty;
	}
}