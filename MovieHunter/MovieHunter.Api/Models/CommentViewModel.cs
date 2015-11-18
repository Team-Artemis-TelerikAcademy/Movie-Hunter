using MovieHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieHunter.Api.Models
{
    public class CommentViewModel
    {
        public static Expression<Func<Comment, CommentViewModel>> FromComment
        {
            get
            {
                return comment => new CommentViewModel()
                {
                    Id = comment.Id,
                    UserName = comment.User.UserName,
                    Text = comment.Text,
                    MovieTitle = comment.Movie.Title,
                    DateAdded = comment.DateAdded
                };
            }
        }

        public DateTime DateAdded { get; private set; }
        public int Id { get; private set; }
        public string MovieTitle { get; private set; }
        public string Text { get; private set; }
        public string UserName { get; private set; }
    }
}
