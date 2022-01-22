using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Purchases()
        {

            var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var x = _userService.GetAllPurchasesForUser(userId);
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var x = _userService.GetAllFavoritesForUser(userId);
            return View();
        }
        public Task<IActionResult> Profile()
        {
            return null;

        }
        public Task<IActionResult> EditProfile()
        {
            return null;

        }
        [HttpPost]
        public async Task<IActionResult> BuyForUser(int UserId, int MovieId)
        {
            var request = new PurchaseRequestModel
            {
                UserId = UserId,
                MovieId = MovieId,
            };
            var x = await _userService.PurchaseMovie(request, UserId);
            return LocalRedirect("~/");
        }
        [HttpPost]
        public async Task<IActionResult> ReviewByUser(ReviewRequestModel model)
        {

            await _userService.AddMovieReview(model);
            return LocalRedirect("~/");
        }
    }
    
}
