using Backend.Models.DTO;
using Backend.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Backend.Tests.Services
{
	public class FeedInitializerTests
	{
		private readonly IFeedService _feedService = Substitute.For<IFeedService>();
		private readonly ILogger<FeedInitializer> _logger = Substitute.For<ILogger<FeedInitializer>>();

		private readonly FeedInitializer _initializer;

		public FeedInitializerTests()
		{
			_initializer = new FeedInitializer(_feedService, _logger);
		}

		[Fact]
		public async Task StartAsync_CallsFeedService()
		{
			var summaries = new List<MovieSummaryDto> { new() { Id = "1", Headline = "Movie 1" } };
			_feedService.GetMovieSummariesAsync().Returns(summaries);

			await _initializer.StartAsync(CancellationToken.None);

			await _feedService.Received(1).GetMovieSummariesAsync();
		}

		[Fact]
		public async Task StartAsync_HandlesExceptionWhenFeedServiceThrows()
		{
			_feedService.GetMovieSummariesAsync().ThrowsAsync(new InvalidOperationException("Feed failed"));

			await _initializer.StartAsync(CancellationToken.None);

			await _feedService.Received(1).GetMovieSummariesAsync();
		}
	}
}