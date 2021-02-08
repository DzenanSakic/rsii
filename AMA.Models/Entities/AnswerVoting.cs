using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Models.Entities
{
    public class AnswerVoting
    {
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }
        public int AnswerId { get; set; }
        public bool Rating { get; set; }
    }
}
