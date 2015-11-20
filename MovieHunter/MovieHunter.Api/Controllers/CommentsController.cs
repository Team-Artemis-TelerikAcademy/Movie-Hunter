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

namespace MovieHunter.Api.Controllers
{
    public class CommentsController : ApiController
    {
        private IMoviesService movieService;
        private ICommentsService service;
        private IUsersService usersService;

        //public CommentsController()
        //{
        //    var db = new MovieDbContext();
        //    this.service = new CommentsService(new EfRepository<Comment>(db));
        //    this.movieService = new MoviesService(new EfRepository<Movie>(db));
        //    this.usersService = new UsersService(new EfRepository<User>(db));
        //}

        public CommentsController(ICommentsService commentsService, IMoviesService movies, IUsersService users)
        {
            this.service = commentsService;
            this.movieService = movies;
            this.usersService = users;
        }

        [Route("api/movies/{id}/comments")]
        public IHttpActionResult GetAllComments(int id)
        {
            var movie = this.movieService.GetById(id);
            return this.Ok(this.service.GetAllCommentsByMovie(movie.Id).Select(CommentViewModel.FromComment));
        }

        [Route("api/movies/{id}/comments")]
        [Authorize]
        public IHttpActionResult PostComment(CommentsBindingModel comment, int id)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }
            var username = this.User.Identity.Name;
            var user = this.usersService.GetByName(username);
            var movie = this.movieService.GetById(id);

            this.service.Add(user, movie.Id, comment.Text);

            return this.Created("api/movies/{id}"+ movie.Id,MovieDetailViewModel.FromMovie.Compile().Invoke(movie));
        }

       
    }
}
