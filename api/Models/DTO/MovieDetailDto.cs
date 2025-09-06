namespace Backend.Models.DTO
{
	public class MovieDetailDto
	{
		public string Id { get; set; } = string.Empty;
		public string Headline { get; set; } = string.Empty;
		public string Year { get; set; } = string.Empty;
		public int Rating { get; set; }
		public string Synopsis { get; set; } = string.Empty;
		public string Body { get; set; } = string.Empty;
		public string Cert { get; set; } = string.Empty;
		public string Quote { get; set; } = string.Empty;
		public string ReviewAuthor { get; set; } = string.Empty;
		public string SkyGoId { get; set; } = string.Empty;
		public string SkyGoUrl { get; set; } = string.Empty;
		public string Sum { get; set; } = string.Empty;

		public List<ImageDto> CardImages { get; set; } = [];
		public List<ImageDto> KeyArtImages { get; set; } = [];
		public List<PersonDto> Cast { get; set; } = [];
		public List<PersonDto> Directors { get; set; } = [];
		public List<VideoDto> Videos { get; set; } = [];
		public List<string> Genres { get; set; } = [];

		public ViewingWindowDto? ViewingWindow { get; set; }
	}
}
