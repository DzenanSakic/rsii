using AMA.Common.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AMA.Models.Entities
{
    [DataContract]
    public class User
    {
        [Key]
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }
        [DataMember]
        public Gender Gender { get; set; }
        [DataMember]
        public string Mail { get; set; }
        [DataMember]
        [ForeignKey("CityId")]
        public City City { get; set; }
        public int CityId { get; set; }
        [DataMember]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        [DataMember]
        public UserStatus Status { get; set; }
    }
}
