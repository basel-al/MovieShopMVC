using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetUserDetails(int Id)
        {
            var x = await _userService.GetUser(Id);
            return Ok(x);
        }
        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> BuyForUser(PurchaseRequestModel model, int userId)
        {
            var x = await _userService.PurchaseMovie(model, userId);
            return Ok();
        }
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> FavoriteByUser(FavoriteRequestModel model)
        {

            await _userService.AddFavorite(model);
            return Ok();
        }
        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> RemoveFavorite(FavoriteRequestModel model)
        {
            await _userService.RemoveFavorite(model);
            return Ok();
        }
        [HttpGet]
        [Route("check-movie-favorite/")]
        public async Task<IActionResult> CheckFavorite(FavoriteRequestModel model)
        {
            var f = await _userService.FavoriteExists(model.UserId, model.MovieId);
            return Ok();
        }
        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> ReviewByUser(ReviewRequestModel model)
        {

            await _userService.AddMovieReview(model);
            return Ok();
        }
        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> ModifyReview(ReviewRequestModel model)
        {

            await _userService.UpdateMovieReview(model);
            return Ok();
        }
        [HttpDelete]
        [Route("delete-review")]
        public async Task<IActionResult> RemoveReview(int userId, int movieId)
        {
            await _userService.DeleteMovieReview(userId, movieId);
            return Ok();
        }
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> Purchases(int id)
        {
            var MyPurchases = await _userService.GetAllPurchasesForUser(id);
            if(MyPurchases == null)
            {
                return NotFound();
            }
            return Ok(MyPurchases);
        }
        [HttpGet]
        [Route("purchase-details/{movieId:int}")]
        public async Task<IActionResult> PurchaseDetails(int userId, int movieId)
        {

            var details = await _userService.GetPurchasesDetails(userId, movieId);
            return Ok(details);
        }
        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> VerifyPurchase(int movieId, int userId, PurchaseRequestModel model)
        {
            var details = await _userService.IsMoviePurchased(model, userId);
            return Ok(details);
        }
        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> Favorites(int id)
        {
            var MyFavorites = await _userService.GetAllFavoritesForUser(id);
            if (MyFavorites == null)
            {
                return NotFound();
            }
            return Ok(MyFavorites);
        }

        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> Reviews(int id)
        {
            var MyReviews = await _userService.GetAllReviewsByUser(id);
            if (MyReviews == null)
            {
                return NotFound();
            }
            return Ok(MyReviews);
        }
    
    }
}
