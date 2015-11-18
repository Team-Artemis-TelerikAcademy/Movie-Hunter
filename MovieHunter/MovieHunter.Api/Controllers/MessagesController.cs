namespace MovieHunter.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Models;
    using MovieHunter.Models;
    using Services.Contracts;

    public class MessagesController : ApiController
    {
        private IRepository<Message> messages;
        private IRepository<User> users;

        public MessagesController(IRepository<Message> messages, IRepository<User> users)
        {
            this.messages = messages;
            this.users = users;
        }

        [HttpGet]
        public IHttpActionResult GetByUsername(string username)
        {
            var msgs = this.messages.All()
                                    .Where(x => x.Author.UserName == username || x.Recipient.UserName == username)
                                    .ToList();

            return this.Ok(msgs);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody] MessageViewModel msg)
        {
            var participants = this.users.All()
                                         .Where(x => x.UserName == msg.Author || x.UserName == msg.Recepient)
                                         .ToList();

            var msgToAdd = new Message()
            {
                Content = msg.Content,
                AuthorId = participants.FirstOrDefault(x => x.UserName == msg.Author).Id,
                RecipientId = participants.FirstOrDefault(x => x.UserName == msg.Recepient).Id,
                TimeSent = msg.TimeSent
            };

            this.messages.Add(msgToAdd);
            this.messages.SaveChanges();

            return this.Created(this.Url.ToString(), msg);
        }
    }
}