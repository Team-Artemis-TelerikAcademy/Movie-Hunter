<<<<<<< HEAD
﻿using MovieHunter.Api.Models;
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
=======
﻿using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using MovieHunter.Api.Models;
using MovieHunter.Services.Contracts;
>>>>>>> 78f6d2e53bd2111795470e7915644f7cdbb7c1b9

namespace MovieHunter.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ActorsController : ApiController
    {
        private const int PageSize = 10;
        private IActorsService service;

        //public ActorsController()
        //{
        //    var db = new MovieDbContext();
        //    this.service = new ActorsService(new EfRepository<Actor>(db));
        //}

        public ActorsController(IActorsService actorsService)
        {
            this.service = actorsService;
        }

        public ActorsController(IActorsService service)
        {
            this.service = service;
        }
        public IHttpActionResult GetAll()
        {
            return this.GetAll(1);
        }

       

        public IHttpActionResult GetAll(int page)
        {
            return this.Ok(this.service.GetAll().Skip((page - 1) * PageSize).Take(PageSize).Select(ActorViewModel.FromActor));
        }

        public IHttpActionResult GetById(int id)
        {
            return this.Ok(ActorViewModel.FromActor.Compile().Invoke(this.service.GetActorById(id)));
        }

        //api/actors?name=value
        public IHttpActionResult GetByName(string name)
        {
            return this.Ok(this.service.GetActorByName(name).Select(ActorViewModel.FromActor));
        }

    }
}
