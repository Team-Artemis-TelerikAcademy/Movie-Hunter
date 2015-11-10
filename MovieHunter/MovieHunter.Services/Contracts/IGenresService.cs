using System.Collections.Generic;
using System.Linq;
using MovieHunter.Models;

namespace MovieHunter.Services.Contracts
{
    public interface IGenresService
    {
        IQueryable<Genre> GetAllGenres();
        Genre GetGenreById(int id);
    }
}