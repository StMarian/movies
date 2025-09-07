namespace Backend.Models
{
	public class ErrorResponse
	{
		public required int Status { get; set; }
		public required string TraceId { get; set; }
	}
}