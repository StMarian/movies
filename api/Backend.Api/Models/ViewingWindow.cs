using System.Text.Json.Serialization;

namespace Backend.Models
{
	public class ViewingWindow
	{
		[JsonPropertyName("startDate")]
		public string StartDate { get; set; } = string.Empty;

		[JsonPropertyName("wayToWatch")]
		public string WayToWatch { get; set; } = string.Empty;

		[JsonPropertyName("endDate")]
		public string EndDate { get; set; } = string.Empty;
	}
}