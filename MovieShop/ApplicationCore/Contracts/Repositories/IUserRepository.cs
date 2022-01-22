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
        Task<List<Favorite>> GetFavoritesOfUser(int userId);
        Task DeleteFavorite(int movieId, int userId);
        Task<decimal> GetPriceDetails(int theid);
        Task<List<Review>> GetReviewsOfUser(int userId);
/*        Task AddReviewForUser(ReviewRequestModel model);*/
        Task AddReview(Review review);
        Task AddFavorite(Favorite favorite);
    }
}
