using System.ComponentModel.DataAnnotations;

namespace AMA.Models.DTOS
{
    public class InsertUserFollowRequest
    {
        [Required]
        public int FollowingUserId { get; set; }
        [Required]
        public int UserFolloingId { get; set; }
    }
}
