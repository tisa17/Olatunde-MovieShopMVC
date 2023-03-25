using System;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IMovieService
	{
	  Task<	List<MovieCardModel>> GetTopGrossingMovies();

		// get movie details
	   Task< MovieDetailsModel>	GetMovieDetails(int id);

		Task< PagedResultSetModel<MovieCardModel> > GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1);

		Task<bool> AddMovie(MovieCardModel movie);
	}
}

