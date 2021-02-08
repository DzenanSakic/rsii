using System;
using System.Collections.Generic;
using System.Text;

namespace AMA.Common.Contracts
{
    public class QuestionResponse
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public UsersResponse User { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
