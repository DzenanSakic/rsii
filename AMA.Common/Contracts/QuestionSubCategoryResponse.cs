namespace AMA.Common.Contracts
{
    public class QuestionSubCategoryResponse
    {
        public QuestionResponse Question { get; set; }
        public int QuestionId { get; set; }
        public SubCategoryResponse SubCategory { get; set; }
        public int SubCategoryId { get; set; }
    }
}
