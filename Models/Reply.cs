using System;
using System.Collections.Generic;

namespace myWebApp.Models
{
    public class Reply
    {
        public int ID { get; set; }
        public int TopicID { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime? MessageDate { get; set; }

        public Reply()
        {
            MessageDate = new DateTime();
        }

        public Topic Topic { get; set; }
    }
}
