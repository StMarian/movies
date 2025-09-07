using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Backend.Services
{
	public interface IImageCacheService
	{
		ValueTask<byte[]?> GetOrDownloadAsync(string hash, string originalUrl);
	}

	public class ImageCacheService(ILogger<ImageCacheService> logger, HttpClient httpClient) : IImageCacheService
	{
		private readonly ConcurrentDictionary<string, byte[]?> _cache = new();
		private readonly ConcurrentDictionary<string, Lazy<Task<byte[]?>>> _currentDownloads = new();

		public async ValueTask<byte[]?> GetOrDownloadAsync(string hash, string originalUrl)
		{
			if (_cache.TryGetValue(hash, out var cached))
			{
				return cached;
			}

			var lazyTask = _currentDownloads.GetOrAdd(hash, _ =>
				new Lazy<Task<byte[]?>>(async () =>
				{
					try
					{
						logger.LogInformation("Downloading image for hash {Hash}", hash);

						var bytes = await httpClient.GetByteArrayAsync(originalUrl);

						_cache[hash] = bytes;
						return bytes;
					}
					catch
					{
						// Cache the failure to avoid repeated attempts
						_cache[hash] = null;
						return null;
					}
					finally
					{
						_currentDownloads.TryRemove(hash, out var _);
					}
				}));

			return await lazyTask.Value;
		}
	}
}
