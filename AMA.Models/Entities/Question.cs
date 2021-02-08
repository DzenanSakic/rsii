using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Models.Entities
{
    public class Question
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
