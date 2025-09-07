using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
	[ApiController]
	[Route("movies")]
	public class MoviesController(FeedService feedService) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var feed = await feedService.GetMovieSummariesAsync();

			return Ok(feed);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var movie = await feedService.GetMovieDetailAsync(id);
			if (movie == null)
			{
				return NotFound();
			}

			return Ok(movie);
		}
	}
}
