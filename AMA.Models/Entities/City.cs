using System.ComponentModel.DataAnnotations;

namespace AMA.Models.Entities
{
    public class City
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
