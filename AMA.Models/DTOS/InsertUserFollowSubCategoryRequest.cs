using System.ComponentModel.DataAnnotations;

namespace AMA.Models.DTOS
{
    public class InsertUserFollowSubCategoryRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
    }
}
