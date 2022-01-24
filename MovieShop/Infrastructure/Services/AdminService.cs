using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AdminService
    {
        private IMovieRepository _movieRepository;
        public AdminService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task AddMovie(MovieCreateRequest request)
        {
            var movie = new Movie
            {
                Title = request.Title,
                Overview = request.Overview,
                Tagline = request.Tagline,
                Budget = request.Budget,
                Revenue = request.Revenue,
                ImdbUrl=request.ImdbUrl,
                TmdbUrl=request.TmdbUrl,
                PosterUrl=request.PosterUrl,
                BackdropUrl=request.BackdropUrl,    
                OriginalLanguage=request.OriginalLanguage, 
                ReleaseDate=request.ReleaseDate,
                RunTime=request.RunTime,

            };
            await _movieRepository.AddMovie(movie);

        }
    }
}
