using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> AddingMovie(MovieCreateRequest request)
        {
            await _adminService.AddMovie(request);
            return Ok();
        }
        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> ChangingMovie(MovieCreateRequest request)
        {
            await _adminService.ChangeMovie(request);
            return Ok();
        }
        [HttpGet]
        [Route("top-purchased-movies")]
        public async Task<IActionResult> TopPurchasesRange(DateTime? x, DateTime? y)
        {
            if (x == null || y == null)
            {
                x = DateTime.Now.AddDays(-90);
                y = DateTime.Now;
            }
            var b = await _adminService.TopPurchasesedMovies(x.Value, y.Value);
            return Ok(b);
        }
/*        [HttpGet]
        [Route("top-purchased-movies")]
        public async Task<IActionResult> TopPurchases()
        {

            var b = await _adminService.TopPurchasesedMovies(DateTime.Now.AddDays(-90), DateTime.Now);
            return Ok(b);
        }*/
    }
}
    
