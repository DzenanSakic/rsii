using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AMA.Models.Entities
{
    [DataContract]
    public class UserRole
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
        [DataMember]
        public AMA.Common.Enumerations.UserRole Role { get; set; }
    }
}
