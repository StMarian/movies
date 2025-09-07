using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;
using Backend.Services;
using Backend.Tests.MockHelpers;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Net;

namespace Backend.Tests.Services
{
	public class FeedServiceTests
	{
		private readonly IOptions<FeedSettings> _options;
		private readonly IMapper _mapper = Substitute.For<IMapper>();

		private readonly FeedSettings _feedSettings = new() { Url = "https://api.example.com/movies" };

		public FeedServiceTests()
		{
			_options = Options.Create(_feedSettings);
		}

		[Fact]
		public async Task GetMovieSummariesAsync_ReturnsMovieSummaries()
		{
			var httpClient = CreateHttpClientWithMovies(
			[
				new() { Id = "1", Headline = "Movie 1" },
				new() { Id = "2", Headline = "Movie 2" },
			]);

			_mapper.Map<MovieSummaryDto>(Arg.Any<Movie>()).Returns(
				new MovieSummaryDto { Id = "1", Headline = "Movie 1" },
				new MovieSummaryDto { Id = "2", Headline = "Movie 2" }
			);

			var service = new FeedService(httpClient, _mapper, _options);

			var result = await service.GetMovieSummariesAsync();

			Assert.Equal(2, result.Count);
			Assert.Equal("Movie 1", result[0].Headline);
			Assert.Equal("Movie 2", result[1].Headline);
		}

		[Fact]
		public async Task GetMovieSummariesAsync_WithEmptyUrl_ThrowsInvalidOperationException()
		{
			var feedSettings = new FeedSettings { Url = "" };
			var settings = Substitute.For<IOptions<FeedSettings>>();
			var httpClient = new HttpClient();

			settings.Value.Returns(feedSettings);

			var service = new FeedService(httpClient, _mapper, settings);

			await Assert.ThrowsAsync<InvalidOperationException>(() => service.GetMovieSummariesAsync().AsTask());
		}

		[Fact]
		public async Task GetMovieSummariesAsync_CachesResults()
		{
			var httpClient = CreateHttpClientWithMovies([
				new() { Id = "1", Headline = "Movie 1" },
			]);

			_mapper.Map<MovieSummaryDto>(Arg.Any<Movie>()).Returns(new MovieSummaryDto { Id = "1", Headline = "Movie 1" });

			var service = new FeedService(httpClient, _mapper, _options);

			var result1 = await service.GetMovieSummariesAsync();
			var result2 = await service.GetMovieSummariesAsync();

			Assert.Same(result1, result2);
		}

		[Fact]
		public async Task GetMovieDetailAsync_WithValidId_ReturnsMovieDetail()
		{
			var httpClient = CreateHttpClientWithMovies([
				new() { Id = "1", Headline = "Movie 1" },
				new() { Id = "2", Headline = "Movie 2" },
			]);

			_mapper.Map<MovieDetailDto>(Arg.Any<Movie>()).Returns(new MovieDetailDto { Id = "1", Headline = "Movie 1" });

			var service = new FeedService(httpClient, _mapper, _options);

			var result = await service.GetMovieDetailAsync("1");

			Assert.NotNull(result);
			Assert.Equal("1", result.Id);
			Assert.Equal("Movie 1", result.Headline);
		}

		[Fact]
		public async Task GetMovieDetailAsync_WithInvalidId_ReturnsNull()
		{
			var httpClient = CreateHttpClientWithMovies([new() { Id = "1", Headline = "Movie 1" }]);

			var service = new FeedService(httpClient, _mapper, _options);

			var result = await service.GetMovieDetailAsync("999");

			Assert.Null(result);
		}

		private HttpClient CreateHttpClientWithMovies(List<Movie> movies) => MockHttpMessageHandler.CreateMockHttpClientWithJson(HttpStatusCode.OK, movies);
	}
}