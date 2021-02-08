using AMA.Common.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace AMA.Models.DTOS
{
    public class EditUserProfileRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
