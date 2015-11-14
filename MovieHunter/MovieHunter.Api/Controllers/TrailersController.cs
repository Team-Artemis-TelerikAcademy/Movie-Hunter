using MovieHunter.Api.Models;
using MovieHunter.Data;
using MovieHunter.Models;
using MovieHunter.Services;
using MovieHunter.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MovieHunter.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*" )]
    public class TrailersController : ApiController
    {
        private const int PageSize = 10;
        private ITrailersService service;

        public TrailersController()
        {
            var dbContext = new MovieDbContext();
            this.service = new TrailersService(new EfRepository<Trailer>(dbContext));
        }

        public TrailersController(ITrailersService service)
        {
            this.service = service;
        }

        public IHttpActionResult GetAll()
        {
            return this.GetAll(1);
        }

        public IHttpActionResult GetAll(int page)
        {
            return this.Ok(this.service.GetAllTrailers().Skip((page - 1) * PageSize).Take(PageSize).Select(TrailerViewModel.FromTrailer));
        }

        public IHttpActionResult GetById(int id)
        {
            //var result = this.service.GetById(id);
            var result = this.service.GetAllTrailers().FirstOrDefault(tr => tr.Id == id);
            return this.Ok(TrailerViewModel.FromTrailer.Compile().Invoke(result));
        }
    }
}
