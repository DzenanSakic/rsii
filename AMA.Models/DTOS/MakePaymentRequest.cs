using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AMA.Models.DTOS
{
    public class MakePaymentRequest
    {
        [Required]
        public int ToUserId { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
