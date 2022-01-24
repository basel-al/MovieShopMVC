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
        [HttpGet]
        public async Task<IActionResult> Purchases()
        {

            var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var MyPurchases = await _userService.GetAllPurchasesForUser(userId);
            return View(MyPurchases);
        }
        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var myFavorites = await _userService.GetAllFavoritesForUser(userId);
            return View(myFavorites);
        }
        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            var userId = Convert.ToInt32(HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var myReviews = await _userService.GetAllReviewsByUser(userId);
            return View(myReviews);
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> BuyForUser(PurchaseRequestModel model)
        {
            var x = await _userService.PurchaseMovie(model, model.UserId);
            return LocalRedirect("~/");
        }
        [HttpPost]
        public async Task<IActionResult> ReviewByUser(ReviewRequestModel model)
        {

            await _userService.AddMovieReview(model);
            return LocalRedirect("~/");
        }
        [HttpPost]
        public async Task<IActionResult> FavoriteByUser(FavoriteRequestModel model)
        {

            await _userService.AddFavorite(model);
            return LocalRedirect("~/");
        }
        [HttpGet]
        public async Task<bool> CheckPurchase(PurchaseRequestModel model)
        {
            var b = await _userService.IsMoviePurchased(model, model.UserId);
            return b;
        }
    }
    
}
