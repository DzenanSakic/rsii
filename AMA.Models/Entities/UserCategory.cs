using System.Runtime.Serialization;

namespace AMA.Models.Entities
{
    [DataContract]
    public class UserCategory
    {
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public Category Category { get; set; }
        [DataMember]
        public int CategoryId { get; set; }
    }
}
