using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
	[ApiController]
	[Route("images")]
	public class ImagesController(HashService hashService, ImageCacheService imageCacheService) : ControllerBase
	{
		[HttpGet("{hash}")]
		[ResponseCache(Duration = 86400)] // 24 hours client-side caching
		public async Task<IActionResult> GetImage(string hash)
		{
			var originalUrl = hashService.GetUrlFromHash(hash);
			if (originalUrl == null)
			{
				return NotFound("Image hash not found");
			}

			try
			{
				var bytes = await imageCacheService.GetOrDownloadAsync(hash, originalUrl);
				if (bytes == null || bytes.Length == 0)
				{
					return NotFound("Image could not be retrieved");
				}

				// Determine content type based on URL extension
				var contentType = GetContentTypeFromUrl(originalUrl);

				return File(bytes, contentType);
			}
			catch (HttpRequestException)
			{
				return NotFound("Image could not be retrieved");
			}
		}

		private static string GetContentTypeFromUrl(string url)
		{
			var extension = Path.GetExtension(url.Split('?')[0]).ToLowerInvariant();
			return extension switch
			{
				".jpg" or ".jpeg" => "image/jpeg",
				".png" => "image/png",
				".gif" => "image/gif",
				".webp" => "image/webp",
				".svg" => "image/svg+xml",
				_ => "image/jpeg" // Default fallback
			};
		}
	}
}