using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Models.Entities
{
    public class Answer
    {
        [Key]
        public int ID { get; set; }
        public string Text { get; set; }
        public bool Accepted { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime TimeAnswered { get; set; }
    }
}
