using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AMA.Models.Entities
{
    [DataContract]
    public class UserFollow
    {
        [DataMember]
        public User UserFollowing { get; set; }
        [DataMember]
        public int UserFollowingId { get; set; }
        [DataMember]
        public User FollowedUser { get; set; }
        [DataMember]
        public int FollowedUserId { get; set; }
    }
}
