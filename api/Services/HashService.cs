namespace Backend.Services
{
	public static class HashService
	{
		public static string ComputeHash(string input)
		{
			var dataBytes = System.Text.Encoding.UTF8.GetBytes(input);
			var hashBytes = System.Security.Cryptography.SHA1.HashData(dataBytes);

			return Convert.ToHexStringLower(hashBytes);
		}
	}
}
