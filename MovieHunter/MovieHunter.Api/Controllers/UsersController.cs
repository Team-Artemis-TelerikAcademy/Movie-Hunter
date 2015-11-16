namespace MovieHunter.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using MovieHunter.Data;
    using MovieHunter.Models;
    using MovieHunter.Services;
    using MovieHunter.Services.Contracts;
    using System.Linq;
    using Models;

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
