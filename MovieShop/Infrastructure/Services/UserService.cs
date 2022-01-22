using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApplicationCore.Models.FavoriteResponseModel;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IPurchaseRepository _purchaseRepository;
        private IUserRepository _userRepository;
        private IMovieRepository _movieRepository;
        public UserService(IPurchaseRepository purchaseRepository, IUserRepository userRepository, IMovieRepository movieRepository)
        {
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var movie = await _movieRepository.GetById(favoriteRequest.MovieId);
            var newFavorite = new Favorite
            {
                Id = favoriteRequest.Id,
                UserId = favoriteRequest.UserId,
                MovieId = favoriteRequest.MovieId,
                Movie = movie
            };
            
            await _userRepository.AddFavorite(newFavorite);
        }
        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var movie = await _movieRepository.GetById(reviewRequest.MovieId);
            var newReview = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText,
                Movie = movie
            };
            await _userRepository.AddReview(newReview);
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            var movies = await _movieRepository.GetById(movieId);
            var reviewedMovie = movies.Reviews.Where(x => x.UserId == userId);
            

        }
        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            var purchases = await _movieRepository.GetById(movieId);
            throw new NotImplementedException();
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            var favs = await _userRepository.GetFavoritesOfUser(id);

            var favoriteresponse = new FavoriteResponseModel();
            favoriteresponse.UserId = id;
            var cards = new List<FavoriteMovieResponseModel>();
            foreach (var fav in favs)
            {
                cards.Add(new FavoriteMovieResponseModel { Id = fav.Movie.Id, Title = fav.Movie.Title, PosterUrl = fav.Movie.PosterUrl, ReleaseDate = fav.Movie.ReleaseDate.Value });
            }
            favoriteresponse.FavoriteMovies = cards;
            return favoriteresponse;
        }
        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            var mypurchases = await _purchaseRepository.GetByUserId(id);
            var purchaseresponse = new PurchaseResponseModel();
            purchaseresponse.UserId = id;
            var cards = new List<MovieCardResponseModel>();
            foreach (var thepurchase in mypurchases)
            {             
                cards.Add(new MovieCardResponseModel {Id = thepurchase.Movie.Id, Title = thepurchase.Movie.Title, PosterUrl = thepurchase.Movie.PosterUrl, ReleaseDate = thepurchase.Movie.ReleaseDate.Value });
            }
            purchaseresponse.PurchasedMovies = cards;
        

            purchaseresponse.TotalMoviesPurchased = purchaseresponse.PurchasedMovies.Count;
            return purchaseresponse;
        }

        public async Task<UserReviewResponseModel> GetAllReviewsByUser(int id)
        {
            await _userRepository.GetReviewsOfUser(id);
            var myUser = await _userRepository.GetById(id);
            /*var reviewlist = myUser.Reviews.ToList();*/
            var reviewlist = myUser.Reviews;
            var MovieReviewsList = new List <MovieReviewResponseModel>();
            foreach (var review in reviewlist)
            {
                MovieReviewsList.Add(new MovieReviewResponseModel { UserId = review.UserId, MovieId = review.MovieId, ReviewText = review.ReviewText, Rating = review.Rating , Name= review.Movie.Title});
            }
            return new UserReviewResponseModel
            {
                UserId=id,
                MovieReviews= MovieReviewsList
            };
        }

        public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
        {
            var purchases = await _purchaseRepository.GetAll();
            foreach (var purchase in purchases)
            {
                if (purchase.UserId == userId && purchase.MovieId == movieId)
                {
                    return new PurchaseDetailsResponseModel
                    {
                        Id = purchase.Id,
                        UserId = purchase.UserId,
                        PurchaseNumber = purchase.PurchaseNumber,
                        TotalPrice = purchase.TotalPrice,
                        PurchaseDateTime = purchase.PurchaseDateTime,
                        MovieId = purchase.MovieId,
                        Title = purchase.Movie.Title,
                        PosterUrl = purchase.Movie.PosterUrl,
                        ReleaseDate = purchase.Movie.ReleaseDate.GetValueOrDefault(),
                    };
                }
            }
            throw new Exception("error");


        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var y = await _purchaseRepository.GetByUserId(userId);
            
            var purchases = await _purchaseRepository.GetByUserId(userId);
            foreach (var purchase in purchases)
            {
                if (purchase.MovieId == purchaseRequest.MovieId)
                {
                    return true;
                }
            }
            return false;

        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var movieprice = await _userRepository.GetPriceDetails(purchaseRequest.MovieId);
            var newPurchase = new Purchase
            {
                MovieId = purchaseRequest.MovieId,
                UserId = userId,
                PurchaseNumber = Guid.NewGuid(),
                TotalPrice = movieprice,
                PurchaseDateTime = DateTime.Now
            };
            var dbCreatedPurchase = await _purchaseRepository.Add(newPurchase);
            if (dbCreatedPurchase.Id > 1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

       public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            /*            var movies = await _userRepository.Delete
                        var favoritedMovie = movies.Favorite.Where(x => x.UserId == favoriteRequest.UserId);
                        favoritedMovie.ToList().RemoveAt(0);*/
            throw new NotImplementedException();
            
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var movies = await _movieRepository.GetById(reviewRequest.MovieId);
            var favoritedMovie = movies.Favorite.Where(x => x.UserId == reviewRequest.UserId);
            var x = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText,
            };
            favoritedMovie.ToList().RemoveAt(0);
        }
    }
}

