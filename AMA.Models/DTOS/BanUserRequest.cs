using System;

namespace AMA.Models.DTOS
{
    public class BanUserRequest
    {
        public int UserId { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string Reason { get; set; }
    }
}
