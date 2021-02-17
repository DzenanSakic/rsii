using System.Collections.Generic;

namespace AMA.Common.Contracts
{
    public class CategoryUsageReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfAnswers { get; set; }
        public IList<SubCategoryUsageReport> SubCategoryUsageReport { get; set; }
    }

    public class SubCategoryUsageReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfAnswers { get; set; }
        public int CategoryId { get; set; }
    }
}
