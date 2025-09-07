using System.Collections.Concurrent;

namespace Backend.Services
{
	public interface IImageHashService
	{
		string ComputeAndStoreHash(string url);
		string? GetUrlFromHash(string hash);
	}

	public class ImageHashService : IImageHashService
	{
		private readonly ConcurrentDictionary<string, string> _hashToUrlMap = new();

		public string ComputeAndStoreHash(string url)
		{
			var dataBytes = System.Text.Encoding.UTF8.GetBytes(url);
			var hashBytes = System.Security.Cryptography.SHA1.HashData(dataBytes);

			var hash = Convert.ToHexStringLower(hashBytes);

			_hashToUrlMap[hash] = url;
			return hash;
		}

		public string? GetUrlFromHash(string hash) => _hashToUrlMap.TryGetValue(hash, out var url) ? url : null;
	}
}
