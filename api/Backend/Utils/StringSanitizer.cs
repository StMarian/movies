namespace Backend.Utils
{
	public static class StringSanitizer
	{
		private static readonly Dictionary<string, string> _replacements = new()
		{
			// Add more replacements as needed
			{ "\ufffd", "" },  // Replacement character
			{ "\u0000", "" }   // Null character
		};

		/// <summary>
		/// Sanitizes a string by removing or replacing problematic Unicode characters
		/// </summary>
		/// <param name="text">The text to sanitize</param>
		/// <returns>A sanitized version of the input text</returns>
		public static string SanitizeText(this string? text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}

			foreach (var kvp in _replacements)
			{
				text = text.Replace(kvp.Key, kvp.Value);
			}

			return text;
		}
	}
}