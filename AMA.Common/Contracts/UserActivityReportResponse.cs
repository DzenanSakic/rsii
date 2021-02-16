namespace AMA.Common.Contracts
{
    public class UserActivityReportResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfAnswers { get; set; }
    }
}
