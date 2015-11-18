namespace MovieHunter.Messenger
{
    using System;

    public class MessageViewModel
    {
        public string Content { get; set; }

        public DateTime TimeSent { get; set; }

        public string Author { get; set; }
        
        public string Recepient { get; set; }

        public bool Seen { get; set; }
    }
}