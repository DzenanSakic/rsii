using System.ComponentModel.DataAnnotations;

namespace AMA.Models.DTOS
{
    public class InsertUserFollowCategoryRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
