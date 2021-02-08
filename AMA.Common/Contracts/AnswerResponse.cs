using System;

namespace AMA.Common.Contracts
{
    public class AnswerResponse
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public bool Accepted { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeAnswered { get; set; }
        public UsersResponse User { get; set; }
        public int PositiveVotings { get; set; }
        public int NegativeVotings { get; set; }
        public bool IsAuthorOfQuestion { get; set; }
        public bool IsAuthorOfAnswer { get; set; }
        public bool MarkCorrectVisible { get 
            {
                return !Accepted;
            } }
        public bool MarkIncorrectVisible { get {
                return Accepted;
            } }
    }
}
