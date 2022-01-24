using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetAllGenres();

            //return json
            //newtonsoft.json
            //always return http status codes with data in API
            if (!genres.Any())
            {
                return NotFound();
            }
            return Ok(genres);
        }
    }
}
