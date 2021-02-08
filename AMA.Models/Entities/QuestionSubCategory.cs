using System.Runtime.Serialization;

namespace AMA.Models.Entities
{
    [DataContract]
    public class QuestionSubCategory
    {
        [DataMember]
        public Question Question { get; set; }
        [DataMember]
        public int QuestionId { get; set; }
        [DataMember]
        public SubCategory SubCategory { get; set; }
        [DataMember]
        public int SubCategoryId { get; set; }
    }
}
