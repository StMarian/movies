namespace Backend.Models.DTO
{
	public class VideoDto
	{
		public string Title { get; set; } = string.Empty;
		public string Type { get; set; } = string.Empty;
		public string Url { get; set; } = string.Empty;
		public List<VideoAlternativeDto> Alternatives { get; set; } = new();
	}
}
