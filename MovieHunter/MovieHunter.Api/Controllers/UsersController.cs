namespace MovieHunter.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using MovieHunter.Models;
    using Services.Contracts;

    public class UsersController : ApiController
    {
        private IRepository<User> users;

        public UsersController(IRepository<User> users)
        {
            this.users = users;
        }

        [HttpGet]
        public IHttpActionResult GetByName(string username)
        {
            var res = this.users.All().Where(x => x.UserName.Contains(username)).Select(x => x.UserName);
            return this.Ok(res);
        }
    }
}
