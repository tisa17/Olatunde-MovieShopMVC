using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("top-grossing")]
        // Attribute Routing
        // MVC http://localhost/movies/GetTopGrossingMovies => Traditional/Convention based routing
        // http://localhost/api/movies/top-grossing
        public async Task<IActionResult> GetTopGrossingMovies()
        {
            // call my service
            var movies = await _movieService.GetTopGrossingMovies();
            // return the movies information in JSON Format
            // ASP.NET Core automatically serializes C# objects to JSOn objects
            // System.Text.Json .NET 3
            // older version of .NET to work with JSON Newtonsoft.JSON
            // return data(json format) along with it return the HTTP status code

            if (movies == null || !movies.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new {errorMessage = $"No Movie Found for id: {id}"});
            }

            return Ok(movie);
        }
}
}
