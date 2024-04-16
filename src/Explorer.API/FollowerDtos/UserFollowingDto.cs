namespace Explorer.API.FollowerDtos
{
    public class UserFollowingDto
    {
        public string userId { get; set; }
        public string username { get; set; }
        public string image { get; set; }
        public string followingUserId { get; set; }
        public string followingUsername { get; set; }
        public string followingImage { get; set; }
    }
}
