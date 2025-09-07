using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.Extensions.Options;

namespace Backend.Services
{
	public interface IFeedService
	{
		ValueTask<List<MovieSummaryDto>> GetMovieSummariesAsync();
		ValueTask<MovieDetailDto?> GetMovieDetailAsync(string id);
	}

	public class FeedService(HttpClient httpClient, IMapper mapper, IOptions<FeedSettings> settings) : IFeedService
	{
		private List<Movie>? _feedCache;
		private List<MovieSummaryDto>? _summaryCache;
		private Dictionary<string, MovieDetailDto>? _detailCache;

		private readonly SemaphoreSlim _feedSemaphore = new(1, 1);
		private readonly SemaphoreSlim _summarySemaphore = new(1, 1);
		private readonly SemaphoreSlim _detailSemaphore = new(1, 1);

		public async ValueTask<List<MovieSummaryDto>> GetMovieSummariesAsync()
		{
			if (_summaryCache != null)
			{
				return _summaryCache;
			}

			await _summarySemaphore.WaitAsync();
			try
			{
				if (_summaryCache != null)
				{
					return _summaryCache;
				}

				var movies = await GetFeedAsync();
				_summaryCache = [.. movies.Select(mapper.Map<MovieSummaryDto>)];

				return _summaryCache;
			}
			catch
			{
				_summaryCache = [];
				throw;
			}
			finally
			{
				_summarySemaphore.Release();
			}
		}

		public async ValueTask<MovieDetailDto?> GetMovieDetailAsync(string id)
		{
			if (_detailCache != null)
			{
				return _detailCache.TryGetValue(id, out var cachedDetail) ? cachedDetail : null;
			}

			await _detailSemaphore.WaitAsync();
			try
			{
				if (_detailCache != null)
				{
					return _detailCache.TryGetValue(id, out var cachedDetail) ? cachedDetail : null;
				}

				var movies = await GetFeedAsync();
				_detailCache = movies.ToDictionary(m => m.Id, mapper.Map<MovieDetailDto>);

				return _detailCache.TryGetValue(id, out var detail) ? detail : null;
			}
			finally
			{
				_detailSemaphore.Release();
			}
		}

		private async Task<List<Movie>> GetFeedAsync()
		{
			if (_feedCache != null)
			{
				return _feedCache;
			}

			await _feedSemaphore.WaitAsync();
			try
			{
				if (_feedCache != null)
				{
					return _feedCache;
				}

				var feedUrl = settings.Value.Url;
				if (string.IsNullOrEmpty(feedUrl))
				{
					throw new InvalidOperationException("Feed URL is not configured.");
				}

				var movies = await httpClient.GetFromJsonAsync<List<Movie>>(feedUrl) ?? [];

				_feedCache = movies;

				return _feedCache;
			}
			finally
			{
				_feedSemaphore.Release();
			}
		}
	}
}