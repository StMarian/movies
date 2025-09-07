using System.Text.Json.Serialization;

namespace Backend.Models
{
	public class Person
	{
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;
	}
}