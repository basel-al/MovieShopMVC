using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<List<Movie>> Get30HighestGrossingMovies();
        Task<List<Movie>> Get30HighestRatedMovies();
        Task<decimal> GetMovieRating(int id);
        Task AddMovie(Movie movie);
        Task<List<Movie>> GetMoviesOfGenre(int genreId);
        Task<List<Review>> GetReviewsOfMovie(int movieId);
    }
}
