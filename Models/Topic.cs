using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace myWebApp.Models
{
    public class Topic
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("TopicID")]
        public ICollection<Reply> Replies { get; set; } = new List<Reply>();
    }
}
