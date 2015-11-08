using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieHunter.Data;
using MovieHunter.Models;

namespace MovieHunter.Api.Controllers
{
    public class TestController : ApiController
    {

        public IHttpActionResult GetAll()
        {
            var db = new MovieDbContext();
            var genre = new Genre();
            genre.Name = "Horror";
            db.Genres.Add(genre);
            db.SaveChanges();
            return this.Ok(db.Genres.Select(g => g.Name));
        }
    }
}
