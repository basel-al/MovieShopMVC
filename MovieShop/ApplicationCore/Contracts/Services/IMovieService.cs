using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTop30GrossingMovies();
        Task<List<MovieCardResponseModel>> GetTop30RatedMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<List<MovieCardResponseModel>> GetMoviesForGenre(int genreId);
        Task<List<MovieReviewResponseModel>> GetAllReviewsForMovie(int id);
    }
}
