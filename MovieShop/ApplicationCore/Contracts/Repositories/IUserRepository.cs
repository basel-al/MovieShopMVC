using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUser(int id);
        Task<List<Favorite>> GetFavoritesOfUser(int userId);
        Task DeleteFavorite(int userId, int movieId);
        Task<decimal> GetPriceDetails(int theid);
        Task<List<Review>> GetReviewsOfUser(int userId);
/*        Task AddReviewForUser(ReviewRequestModel model);*/
        Task AddReview(Review review);
        Task AddFavorite(Favorite favorite);
        Task<Purchase> GetPurchaseByMovieUserId(int userId, int movieId);
        Task<Favorite> GetFavoriteByMovieUserId(int userId, int movieId);
        Task DeleteReview(int userId, int movieId);
        Task<Review> UpdateReview(int userId, int movieId, decimal rating, string reviewtext);
        Task<bool> VerifyEmail(string email);


    }
}
