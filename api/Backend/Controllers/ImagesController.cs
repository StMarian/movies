using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
	[ApiController]
	[Route("images")]
	public class ImagesController(
		IImageHashService hashService,
		IImageCacheService imageCacheService,
		IFeedService feedService) : ControllerBase
	{
		[HttpGet("{hash}")]
		[ResponseCache(Duration = 86400)]
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

				var contentType = GetContentTypeFromUrl(originalUrl);
				return File(bytes, contentType);
			}
			catch (HttpRequestException)
			{
				return NotFound("Image could not be retrieved");
			}
		}

		[HttpGet("for-movie/{movieId}")]
		[ResponseCache(Duration = 86400)]
		public async Task<IActionResult> GetFirstAvailableImageForMovie(string movieId)
		{
			var movie = await feedService.GetMovieDetailAsync(movieId);
			if (movie == null)
			{
				return NotFound("Movie not found");
			}

			foreach (var image in movie.CardImages)
			{
				var originalUrl = hashService.GetUrlFromHash(image.Hash);
				if (originalUrl == null)
				{
					return NotFound("Image hash not found");
				}

				var bytes = await imageCacheService.GetOrDownloadAsync(image.Hash, originalUrl);
				if (bytes != null && bytes.Length > 0)
				{
					var contentType = GetContentTypeFromUrl(originalUrl);
					return File(bytes, contentType);
				}
			}

			return NotFound("No available images could be retrieved");
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