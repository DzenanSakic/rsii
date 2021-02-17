namespace AMA.Common.Contracts
{
    public class UserFollowResponse
    {
        public UsersResponse UserFollowing { get; set; }
        public int UserFollowingId { get; set; }
        public UsersResponse FollowedUser { get; set; }
        public int FollowedUserId { get; set; }
    }
}
