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

    public class UsersController : ApiController
    {
        private IRepository<User> users;

        public UsersController()
        {
            this.users = new EfRepository<User>(new MovieDbContext());
        }

        [HttpPost]
        public IHttpActionResult Register(string username, string pass = "", string passConfirmation = "")
        {
            // TODO: validation and fix this abomination

            if (pass == passConfirmation)
            {
                var newUser = new User()
                {
                    IsAdmin = false,
                    Username = username
                };

                this.users.Add(newUser);
                this.users.SaveChanges();
                return this.Created(this.Url.ToString(), username);
            }

            return this.BadRequest("password do not match");
        }
    }
}
