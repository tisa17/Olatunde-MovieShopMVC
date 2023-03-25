using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {
            // LINQ code to get top 30 grossing movies
            // select top 30 * from Movie order by Revenue
            // I/O bound operation
            // await
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public Task<IEnumerable<Movie>> Get30HighestRatedMovies()
        {
            throw new NotImplementedException();
        }

        public async override Task<Movie> GetById(int id)
        {
            // select * from Movie
            // join cast and MovieCast
            // join Trailer
            // Genre and MovieGenre
            // where id = id
            var movieDetails = await _dbContext.Movies
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
                .Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == id);

            // FirstOrDefault
            // First  => ex when thee are no matching records

            // SingleOrDefault 0 or 1 =>   ex => more than one matching record 
            // Single more than one matching record 

            return movieDetails;
        }

        public async Task< PagedResultSetModel<Movie> > GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            // get total count movies for the genre
            var totalMoviesForGenre = await _dbContext.MovieGenres.Where(g => g.GenreId == genreId).CountAsync();

            var movies = await _dbContext.MovieGenres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Movie)
                .OrderByDescending(m => m.Movie.Revenue)
                .Select(m => new Movie { Id = m.MovieId, PosterUrl = m.Movie.PosterUrl, Title = m.Movie.Title })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagedMovies = new PagedResultSetModel<Movie>(pageNumber, totalMoviesForGenre, pageSize, movies);
            return pagedMovies;
            
        }
    }
}

