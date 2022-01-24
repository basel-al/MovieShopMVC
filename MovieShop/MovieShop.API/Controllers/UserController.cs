﻿using ApplicationCore.Contracts.Services;
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
        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> BuyForUser(PurchaseRequestModel model)
        {
            var x = await _userService.PurchaseMovie(model, model.UserId);
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
        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> ReviewByUser(ReviewRequestModel model)
        {

            await _userService.AddMovieReview(model);
            return Ok();
        }
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> Purchases(int userId)
        {
            var MyPurchases = await _userService.GetAllPurchasesForUser(userId);
            if(MyPurchases == null)
            {
                return NotFound();
            }
            return Ok(MyPurchases);
        }
        [HttpGet]
        [Route("{favorites}")]
        public async Task<IActionResult> Favorites(int userId)
        {
            var MyFavorites = await _userService.GetAllFavoritesForUser(userId);
            if (MyFavorites == null)
            {
                return NotFound();
            }
            return Ok(MyFavorites);
        }

        [HttpGet]
        [Route("reviews")]
        public async Task<IActionResult> Reviews(int userId)
        {
            var MyReviews = await _userService.GetAllReviewsByUser(userId);
            if (MyReviews == null)
            {
                return NotFound();
            }
            return Ok(MyReviews);
        }
/*        [HttpGet]
        [Route("PurchaseDetails")]
        public async Task<IActionResult> PurchaseDetails(int userId, int movieId)
        {

            var details = await _userService.GetPurchasesDetails(userId, movieId);
            return Ok(details);
        }*/

        [HttpPost]
        [Route("DeleteReview")]
        public async Task<IActionResult> RemoveReview(int userId, int movieId)
        {
            await _userService.DeleteMovieReview(userId, movieId);
            return Ok();
        }
    }
}
