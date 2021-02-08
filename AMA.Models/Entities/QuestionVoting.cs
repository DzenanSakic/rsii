using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Models.Entities
{
    public class QuestionVoting
    {
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        [ForeignKey("AnswerId")]
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public bool Rating { get; set; }
    }
}
