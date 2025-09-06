namespace Backend.Models.DTO
{
	public class MovieSummaryDto
	{
		public string Id { get; set; } = string.Empty;
		public string Headline { get; set; } = string.Empty;
		public string Year { get; set; } = string.Empty;
		public int Rating { get; set; }

		public ImageDto Image { get; set; } = new();
	}
}
