using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AMA.Models.Entities
{
    [DataContract]
    public class SubCategory
    {
        [Key]
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [ForeignKey("CategoryId")]
        [DataMember]
        public Category Category { get; set; }
        [DataMember]
        public int CategoryId { get; set; }
    }
}
