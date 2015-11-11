using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using MovieHunter.Api.Models;
using MovieHunter.Data;
using MovieHunter.Models;
using MovieHunter.Services;
using MovieHunter.Services.Contracts;

namespace MovieHunter.Api.Controllers
{
    public class ActorsController : ApiController
    {
        private IActorsService service;
        private int PageSize = 10;

        public ActorsController()
        {
            var db = new MovieDbContext();
            this.service = new ActorsService(new EfRepository<Actor>(db));
        }

        public IHttpActionResult GetAll()
        {
            return this.GetAll(1);
        }

        public IHttpActionResult GetAll(int page)
        {
            return this.Ok(this.service.GetAll().Skip((page - 1)*PageSize).Take(PageSize).Select(ActorViewModel.FromActor));
        }

        public IHttpActionResult GetById(int id)
        {
            return this.Ok(ActorViewModel.FromActor.Compile().Invoke(this.service.GetActorById(id)));
        }

        ////[Route("api/actors/{name}")]
        public IHttpActionResult GetByName(string name)
        {
            return this.Ok(this.service.GetActorByName(name).Select(ActorViewModel.FromActor));
        }
    }
}
