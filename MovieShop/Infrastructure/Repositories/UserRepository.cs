using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        private readonly MovieShopDbContext _dbContext;

        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
        public async Task<User> GetUser(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id==id);
            return user;
        }
        public async Task<List<Favorite>> GetFavoritesOfUser(int userId)
        {
            var favs = await _dbContext.Favorites.Where(x => x.UserId == userId).Include(m => m.Movie).ToListAsync();
            return favs;
        }
        public async Task DeleteFavorite(int userId, int movieId)
        {

            var fav = await _dbContext.Favorites.Where(u => u.UserId == userId && u.MovieId == movieId).SingleOrDefaultAsync();
            _dbContext.Favorites.Remove(fav);
            await _dbContext.SaveChangesAsync();

        }
        public async Task DeleteReview(int userId, int movieId)
        {

            var rev = await _dbContext.Reviews.Where(u => u.UserId == userId && u.MovieId == movieId).SingleOrDefaultAsync();
            _dbContext.Reviews.Remove(rev);
            await _dbContext.SaveChangesAsync();

        }
        public async Task<decimal> GetPriceDetails(int theid)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == theid);
            return movie.Price.GetValueOrDefault();

        }
        public async Task<List<Review>> GetReviewsOfUser(int userId)
        {
            var reviews = await _dbContext.Reviews.Where(x => x.UserId == userId).Include(x=>x.Movie).ToListAsync();
            return reviews;

        }
        public async Task AddReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            _dbContext.SaveChangesAsync();

        }
        public async Task AddFavorite(Favorite favorite)
        {
            _dbContext.Favorites.Add(favorite);
            _dbContext.SaveChangesAsync();

        }
        public async Task<Favorite> GetFavoriteByMovieUserId(int userId, int movieId)
        {
            var favorite = await _dbContext.Favorites.Where(u => u.UserId == userId && u.MovieId == movieId).FirstOrDefaultAsync();
            return favorite;
        }
        public async Task<Review> UpdateReview(int userId, int movieId, decimal rating, string reviewtext)
        {
            var review = await _dbContext.Reviews.Where(u => u.UserId == userId && u.MovieId == movieId).SingleOrDefaultAsync();
            if (review != null)
            {
                review.Rating = rating;
                review.ReviewText = reviewtext;
                await _dbContext.SaveChangesAsync();
                return review;
            }
            var newReview = new Review { UserId = userId, MovieId = movieId, Rating = rating, ReviewText = reviewtext };
            await _dbContext.Reviews.AddAsync(newReview);
            await _dbContext.SaveChangesAsync();
            return newReview;

        }
        public async Task<Purchase> GetPurchaseByMovieUserId(int userId, int movieId)
        {

            var purchaseDetail = await _dbContext.Purchases.Where(u => u.UserId == userId && u.MovieId == movieId).SingleOrDefaultAsync();
            return purchaseDetail;
        }
        public async Task<bool> VerifyEmail(string email)
        {

            var ver = await _dbContext.Users.Where(u => u.Email == email).SingleOrDefaultAsync();
            if(ver != null)
            {
                return true;
            }
            return false;
        }

    }
}
