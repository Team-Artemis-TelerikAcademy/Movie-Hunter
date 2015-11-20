using MovieHunter.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieHunter.Models;

namespace MovieHunter.Services
{
    public class CommentsService : ICommentsService
    {
        private IRepository<Comment> commentsRepository;

        public CommentsService(IRepository<Comment> commentsRepo)
        {
            this.commentsRepository = commentsRepo;
        }

        public void Add(User user, int id, string text)
        {
            var newComment = new Comment()
            {
                MovieId = id,
                Text = text,
                DateAdded = DateTime.Now,
                UserId = user.Id
            };

            this.commentsRepository.Add(newComment);
            this.commentsRepository.SaveChanges();
        }

        public IQueryable<Comment> GetAllCommentsByMovie(int id)
        {
            return this.commentsRepository.All().Where(c => c.MovieId == id);
        }
    }
}
