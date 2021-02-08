using System.ComponentModel.DataAnnotations;

namespace AMA.Models.DTOS
{
    public class InsertAnswerRequest
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public int QuestionId { get; set; }
    }
}
