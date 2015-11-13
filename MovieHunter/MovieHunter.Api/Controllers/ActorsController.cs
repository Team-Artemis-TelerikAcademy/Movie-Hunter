using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using MovieHunter.Api.Models;
using MovieHunter.Services.Contracts;

namespace MovieHunter.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ActorsController : ApiController
    {
        private IActorsService service;
        private int PageSize = 10;

        //public ActorsController()
        //{
        //    var db = new MovieDbContext();
        //    this.service = new ActorsService(new EfRepository<Actor>(db));
        //}

        public ActorsController(IActorsService actorsService)
        {
            this.service = actorsService;
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
