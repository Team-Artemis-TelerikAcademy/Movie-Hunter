using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieHunter.Api.Models;
using MovieHunter.Data;
using MovieHunter.Models;
using MovieHunter.Services;
using MovieHunter.Services.Contracts;
using System.Web.Http.Cors;

namespace MovieHunter.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TrailersController : ApiController
    {
        private ITrailersService service;
        private const int PageSize = 10;

        public TrailersController(ITrailersService service)
        {
            // var dbContext = new MovieDbContext();
            this.service = service;// new TrailersService(new EfRepository<Trailer>(dbContext));
        }

        public IHttpActionResult GetAll()
        {
            return this.GetAll(1);
        }

        public IHttpActionResult GetAll(int page)
        {
            return this.Ok(this.service.GetAllTrailers().Skip((page - 1)*PageSize).Take(PageSize).Select(TrailerViewModel.FromTrailer));
        }

        public IHttpActionResult GetById(int id)
        {
            //var result = this.service.GetById(id);
            var result = this.service.GetAllTrailers().FirstOrDefault(tr => tr.Id == id);
            return this.Ok(TrailerViewModel.FromTrailer.Compile().Invoke(result));
        }
    }
}
