using AMA.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMA.Models.DTOS
{
    public class InsertQuestionRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public List<int> SubCategories { get; set; }
    }
}
