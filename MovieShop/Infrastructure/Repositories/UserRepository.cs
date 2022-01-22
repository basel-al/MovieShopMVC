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
            //CHECK THIS
            //var user = _dbContext.User.FirstOrDefault(x => x.Email == email);
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
        public async Task<List<Favorite>> GetFavoritesOfUser(int userId)
        {
            var favs = await _dbContext.Favorites.Where(x => x.UserId == userId).Include(m=>m.Movie).ToListAsync();
            return favs;
        }
        public async Task DeleteFavorite(int movieId, int userId)
        {
            _dbContext.Remove(_dbContext.Favorites.Where(x => x.UserId == userId).Where(x => x.MovieId == movieId));
            _dbContext.SaveChangesAsync();

        }
        public async Task DeleteReview(int movieId, int userId)
        {
            _dbContext.Remove(_dbContext.Reviews.Where(x => x.UserId == userId).Where(x => x.MovieId == movieId));
            _dbContext.SaveChangesAsync();

        }
        public async Task<decimal> GetPriceDetails(int theid)
        {
            //get movie price where movieid = id
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == theid);
            return movie.Price.GetValueOrDefault();

        }
        public async Task<List<Review>> GetReviewsOfUser(int userId)
        {
            var reviews = await _dbContext.Reviews.Where(x => x.UserId == userId).ToListAsync();
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
    }
}
