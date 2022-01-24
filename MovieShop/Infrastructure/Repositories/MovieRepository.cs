using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        private readonly MovieShopDbContext _dbContext;

        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Movie>> Get30HighestGrossingMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(x => x.Revenue).Take(30).ToListAsync();
            return movies;
        }
        public async Task<List<Movie>> Get30HighestRatedMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(x => this.GetMovieRating(x.Id)).Take(30).ToListAsync();
            return movies;

        }
        public override async Task<Movie> GetById(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.Trailers)
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
                .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
                .SingleOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public async Task<decimal> GetMovieRating(int id)
        {
            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty().AverageAsync(r => r == null ? 0 : r.Rating);
            return movieRating;
        }
        public async Task AddMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChangesAsync();
        }
        public async Task ChangeMovie(Movie movie)
        {
            /*            {
                            var review = await _dbContext.Reviews.Where(u => u.UserId == req. && u.MovieId == movieId).SingleOrDefaultAsync();
                            if (review != null)
                            {
                                review.Rating = rating;
                                review.ReviewText = reviewtext;
                                await _dbContext.SaveChangesAsync();
                                return review;
                            }


                        }*/
            throw new NotImplementedException();

        }
        public async Task<List<Movie>> GetMoviesOfGenre(int genreId)
        {
            var movies = await _dbContext.MovieGenres.Include(x => x.Movie).Where(y => y.GenreId == genreId).Select(z => z.Movie).ToListAsync();
            return movies;

        }
        public async Task<List<Review>> GetReviewsOfMovie(int movieId)
        {
            var reviews = await _dbContext.Reviews.Include(x => x.Movie).Where(x => x.MovieId == movieId).ToListAsync();
            return reviews;

        }

    }
}
