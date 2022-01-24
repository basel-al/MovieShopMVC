using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTop30RatedMovies();
            if (!movies.Any())
            {
                return NotFound();
            }
            return Ok(movies);
        }
        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop30GrossingMovies();
            if(!movies.Any())
            {
                return NotFound();
            }
            return Ok(movies);
        }
        [HttpGet]
        [Route("genre/{genreId}")]
        public async Task<IActionResult> GetMoviesOfGenre(int genreId)
        {
            var movies = await _movieService.GetMoviesForGenre(genreId);
            if (!movies.Any())
            {
                return NotFound();
            }
            return Ok(movies);
        }


    }
}
