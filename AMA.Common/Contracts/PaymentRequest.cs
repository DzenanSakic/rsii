using System;
using System.Collections.Generic;
using System.Text;

namespace AMA.Common.Contracts
{
    public class PaymentRequest
    {
        public string Token { get; set; }
        public string Description { get; set; }
        public long Amount { get; set; }
        public int ToUserId { get; set; }
    }
}
