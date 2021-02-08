using System;
using System.Collections.Generic;
using System.Text;

namespace AMA.Common.Contracts
{
    public class SubCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryResponse Category { get; set; }
    }
}
