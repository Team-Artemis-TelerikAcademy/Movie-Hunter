using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieHunter.Api.Models
{
    public class CommentsBindingModel
    {
        [Required]
        public string Text {get; set;}
    }
}