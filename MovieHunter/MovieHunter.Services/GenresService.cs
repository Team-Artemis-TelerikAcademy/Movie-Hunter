﻿using System.Linq;
using MovieHunter.Models;
using MovieHunter.Services.Contracts;

namespace MovieHunter.Services
{
    public class GenresService :IGenresService
    {
        private IRepository<Genre> genres;

        public GenresService(IRepository<Genre> genresRepo)
        {
            this.genres = genresRepo;
        }

        public IQueryable<Genre> GetAllGenres()
        {
            return genres.All();
        }

        public Genre GetGenreById(int id)
        {
            return this.genres.Find(id);
        }

        public Genre GetGenreByName(string name)
        {
            return this.genres.All().FirstOrDefault(g => g.Name.ToLower() == name.ToLower());
        }
    }
}