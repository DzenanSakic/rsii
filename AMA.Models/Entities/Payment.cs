using AMA.Common.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Models.Entities
{
    public class Payment
    {
        [Key]
        public int ID { get; set; }
        public int PaypalId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public PaymentStatus Status { get; set; }
        [ForeignKey("FromUserId")]
        public User FromUser { get; set; }
        public int FromUserId { get; set; }
        [ForeignKey("ToUserId")]
        public User ToUser { get; set; }
        public int ToUserId { get; set; }

    }
}
