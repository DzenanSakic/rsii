using System.ComponentModel.DataAnnotations;

namespace AMA.Models.DTOS
{
    public class InsertSubCategoryRequest
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
