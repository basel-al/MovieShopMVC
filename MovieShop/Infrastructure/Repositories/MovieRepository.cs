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
            var newmovie = await _dbContext.Movies.Where(x => x.Id == movie.Id).SingleOrDefaultAsync();
            if (movie.Title != null) newmovie.Title = movie.Title;
            if (movie.Overview != null) newmovie.Overview = movie.Overview;
            if (movie.Tagline != null) newmovie.Tagline = movie.Tagline;
            if (movie.Revenue!= null) newmovie.Revenue= movie.Revenue;
            if (movie.Budget != null) newmovie.Budget = movie.Budget;
            if (movie.ImdbUrl != null) newmovie.ImdbUrl = movie.ImdbUrl;
            if (movie.TmdbUrl != null) newmovie.TmdbUrl = newmovie.TmdbUrl;
            if (movie.PosterUrl != null) newmovie.PosterUrl = newmovie.PosterUrl;
            if (movie.BackdropUrl!= null) newmovie.BackdropUrl = newmovie.BackdropUrl;
            if (movie.OriginalLanguage != null) newmovie.OriginalLanguage = newmovie.OriginalLanguage;
            if (movie.RunTime != null) newmovie.RunTime = newmovie.RunTime;
            newmovie.UpdatedDate = DateTime.Now;


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
