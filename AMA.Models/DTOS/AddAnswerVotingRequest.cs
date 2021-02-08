namespace AMA.Models.DTOS
{
    public class AddAnswerVotingRequest
    {
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public bool Rating { get; set; }
    }
}
