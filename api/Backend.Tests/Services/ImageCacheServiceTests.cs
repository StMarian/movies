using Backend.Services;
using Backend.Tests.MockHelpers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Net;

namespace Backend.Tests.Services
{
	public partial class ImageCacheServiceTests
	{
		private readonly ILogger<ImageCacheService> _logger = Substitute.For<ILogger<ImageCacheService>>();

		[Fact]
		public async Task GetOrDownloadAsync_ReturnsImageBytesAndCaches()
		{
			var imageBytes = new byte[] { 1, 2, 3 };
			var httpClient = MockHttpMessageHandler.CreateMockHttpClient(HttpStatusCode.OK, imageBytes);
			var service = new ImageCacheService(_logger, httpClient);
			var hash = "hash1";
			var url = "https://example.com/image.jpg";

			var result1 = await service.GetOrDownloadAsync(hash, url);
			var result2 = await service.GetOrDownloadAsync(hash, url); // Should hit the cache

			Assert.Equal(imageBytes, result1);
			Assert.Equal(imageBytes, result2);
		}

		[Fact]
		public async Task GetOrDownloadAsync_FailedDownloadCachesNull()
		{
			var httpClient = MockHttpMessageHandler.CreateMockHttpClient(HttpStatusCode.NotFound, null);
			var service = new ImageCacheService(_logger, httpClient);
			var hash = "hash2";
			var url = "https://example.com/missing.jpg";

			var result1 = await service.GetOrDownloadAsync(hash, url);
			var result2 = await service.GetOrDownloadAsync(hash, url);

			Assert.Null(result1);
			Assert.Null(result2);
		}

		[Fact]
		public async Task GetOrDownloadAsync_DifferentHashesReturnDifferentResults()
		{
			var imageBytes1 = new byte[] { 1 };
			var imageBytes2 = new byte[] { 2 };
			var httpClient = MockHttpMessageHandler.CreateMockHttpClient([
				(HttpStatusCode.OK, imageBytes1),
				(HttpStatusCode.OK, imageBytes2),
			]);
			var service = new ImageCacheService(_logger, httpClient);

			var hash1 = "hashA";
			var hash2 = "hashB";
			var url1 = "https://example.com/a.jpg";
			var url2 = "https://example.com/b.jpg";

			var result1 = await service.GetOrDownloadAsync(hash1, url1);
			var result2 = await service.GetOrDownloadAsync(hash2, url2);

			Assert.NotEqual(result1, result2);
		}
	}
}
