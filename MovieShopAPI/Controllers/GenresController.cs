using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService _genreService)
        {
            genreService = _genreService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await genreService.GetAllGenres();

            if (genres == null || !genres.Any())
            {
                return NotFound("Genres were not found");
            }

            return Ok(genres);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddGenre([FromBody] Genre genre)
        {
            var addedGenre = await genreService.AddGenre(genre);
            return Ok(addedGenre);
        }
    }
}
