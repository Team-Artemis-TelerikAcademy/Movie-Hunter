namespace MovieHunter.Messenger
{
    using System;

    public class Message
    {
        public string Sender { get; set; }

        public string Content { get; set; }

        public DateTime Sent { get; set; }

        public bool Seen { get; set; }
    }
}