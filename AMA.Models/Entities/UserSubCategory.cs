using System.Runtime.Serialization;

namespace AMA.Models.Entities
{
    [DataContract]
    public class UserSubCategory
    {
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public SubCategory SubCategory { get; set; }
        [DataMember]
        public int SubCategoryId { get; set; }
    }
}
