using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int id)
        {
            var movieDetails = await _movieRepository.GetById(id);
            if (movieDetails == null)
            {
                return null;
            }
            var movie = new MovieDetailsModel
            {
                Id = movieDetails.Id,
                Tagline = movieDetails.Tagline,
                Title = movieDetails.Title,
                Overview = movieDetails.Overview,
                PosterUrl = movieDetails.PosterUrl,
                BackdropUrl = movieDetails.BackdropUrl,
                ImdbUrl = movieDetails.ImdbUrl,
                RunTime = movieDetails.RunTime,
                TmdbUrl = movieDetails.TmdbUrl,
                Revenue = movieDetails.Revenue,
                Budget = movieDetails.Budget,
                ReleaseDate = movieDetails.ReleaseDate
            };

            foreach (var genre in movieDetails.GenresOfMovie)
            {
                movie.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }

            foreach (var trailer in movieDetails.Trailers)
            {
                movie.Trailers.Add(new TrailerModel { Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl });
            }

            return movie;
        }

        public async Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId, pageSize, pageNumber);

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies.PagedData)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return new PagedResultSetModel<MovieCardModel>(pageNumber, movies.TotalRecords, pageSize, movieCards);
        }

        // method that return top movies to the caller
        // list of movies

        public async Task<List<MovieCardModel>> GetTopGrossingMovies()
        {
            var movies = await _movieRepository.Get30HighestGrossingMovies();

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return movieCards;
        }

        public async Task<bool> AddMovie(MovieCardModel movieCard)
        {
            var movie = new Movie();
            movie.Id = movieCard.Id;
            movie.Title = movieCard.Title;
            movie.PosterUrl = movieCard.PosterUrl;
            var addedMovie = await _movieRepository.Add(movie);
            if (addedMovie != null)
            {
                return true;
            }
            return false;
        }
    }
}

