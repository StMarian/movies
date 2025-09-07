using Backend.Services;

namespace Backend.Tests.Services
{
	public class ImageHashServiceTests
	{
		[Fact]
		public void ComputeAndStoreHash_StoresAndRetrievesUrl()
		{
			var service = new ImageHashService();
			var url = "https://example.com/image.jpg";

			var hash = service.ComputeAndStoreHash(url);
			var retrievedUrl = service.GetUrlFromHash(hash);

			Assert.Equal(url, retrievedUrl);
		}

		[Fact]
		public void GetUrlFromHash_ReturnsNullForUnknownHash()
		{
			var service = new ImageHashService();
			var unknownHash = "abcdef123456";

			var result = service.GetUrlFromHash(unknownHash);

			Assert.Null(result);
		}

		[Fact]
		public void ComputeAndStoreHash_DifferentUrlsProduceDifferentHashes()
		{
			var service = new ImageHashService();
			var url1 = "https://example.com/image1.jpg";
			var url2 = "https://example.com/image2.jpg";

			var hash1 = service.ComputeAndStoreHash(url1);
			var hash2 = service.ComputeAndStoreHash(url2);

			Assert.NotEqual(hash1, hash2);
		}

		[Fact]
		public void ComputeAndStoreHash_SameUrlProducesSameHash()
		{
			var service = new ImageHashService();
			var url = "https://example.com/image.jpg";

			var hash1 = service.ComputeAndStoreHash(url);
			var hash2 = service.ComputeAndStoreHash(url);

			Assert.Equal(hash1, hash2);
		}
	}
}
