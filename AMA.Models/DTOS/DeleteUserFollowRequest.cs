using System.ComponentModel.DataAnnotations;

namespace AMA.Models.DTOS
{
    public class DeleteUserFollowRequest
    {
        [Required]
        public int UserFollowingId { get; set; }
        [Required]
        public int FollowedUserId { get; set; }
    }
}
