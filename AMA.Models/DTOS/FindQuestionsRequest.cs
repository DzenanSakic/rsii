using AMA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMA.Models.DTOS
{
    public class FindQuestionsRequest
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
    }
}
