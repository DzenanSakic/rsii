using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMA.Models.Entities
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [ForeignKey("FromUserId")]
        public User FromUser { get; set; }
        public int FromUserId { get; set; }
        [ForeignKey("ToUserId")]
        public User ToUser { get; set; }
        public int ToUserId { get; set; }
    }
}
