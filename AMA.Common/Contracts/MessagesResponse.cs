using System;
using System.Collections.Generic;
using System.Text;

namespace AMA.Common.Contracts
{
    public class MessagesResponse
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public UsersResponse FromUser { get; set; }
        public int FromUserId { get; set; }
        public UsersResponse ToUser { get; set; }
        public int ToUserId { get; set; }
        public bool AlignRight { get; set; }
        public bool AlignLeft { get; set; }
    }
}
