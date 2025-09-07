namespace Backend.Services
{
	public class FeedInitializer(IFeedService feedService, ILogger<FeedInitializer> logger) : IHostedService
	{
		public async Task StartAsync(CancellationToken cancellationToken)
		{
			try
			{
				// Warm up the feed cache
				await feedService.GetMovieSummariesAsync();
				logger.LogInformation("Feed init complete.");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Failed to init feed.");
			}
		}

		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
	}
}
