namespace AMA.Models.DTOS
{
    public class EditQuestionRequest
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int? SubCategoryId { get; set; }
    }
}
