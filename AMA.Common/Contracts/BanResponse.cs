using System;
using System.Collections.Generic;
using System.Text;

namespace AMA.Common.Contracts
{
    public class BanResponse
    {
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public string Reason { get; set; }
    }
}
