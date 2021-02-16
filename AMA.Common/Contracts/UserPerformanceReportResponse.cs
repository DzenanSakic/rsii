using System;
using System.Collections.Generic;
using System.Text;

namespace AMA.Common.Contracts
{
    public class UserPerformanceReportResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TotalAnswers { get; set; }
        public int TotalCorrectAnswers { get; set; }
        public decimal AnswerRate { get; set; }
    }
}
