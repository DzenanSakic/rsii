using System.ComponentModel.DataAnnotations;

namespace AMA.Models.Entities
{
    public class Country
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
