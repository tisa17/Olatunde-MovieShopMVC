using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genreRepository;

        public GenreService(IRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAll();
            var genresModel = genres.Select(g => new GenreModel { Id = g.Id, Name = g.Name });
            return genresModel;
        }

        public async Task<bool> AddGenre(Genre genre)
        {
            var addedGenre = await _genreRepository.Add(genre);
            if (addedGenre.Id > 0)
            {
                return true;
            }

            return false;
        }
    }
}
