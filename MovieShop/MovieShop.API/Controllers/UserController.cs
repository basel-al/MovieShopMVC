﻿using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme) ]
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
            var user = await _userService.GetUser(Id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> BuyForUser(PurchaseRequestModel model, int userId)
        {
            var x = await _userService.PurchaseMovie(model, userId);
            return Ok(x);


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
        public async Task<IActionResult> CheckFavorite(int userId, int movieId)
        {
             var f = await _userService.FavoriteExists(userId, movieId);
             return Ok(f);
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
            if (details == null)
            {
                return NotFound();
            }
            return Ok(details);
        }
        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> VerifyPurchase(PurchaseRequestModel model, int userId)
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
