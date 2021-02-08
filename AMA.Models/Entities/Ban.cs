using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AMA.Models.Entities
{
    [DataContract]
    public class Ban
    {
        [Key]
        public int Id { get; set; }
        [DataMember]
        public DateTime DateTimeFrom { get; set; }
        [DataMember]
        public DateTime DateTimeTo { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
