namespace MovieHunter.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class MessageViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime TimeSent { get; set; }

        [Required]
        public string Author { get; set; }
        
        [Required]
        public string Recepient { get; set; }
    }
}