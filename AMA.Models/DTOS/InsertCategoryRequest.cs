using System.ComponentModel.DataAnnotations;

namespace AMA.Models.DTOS
{
    public class InsertCategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
