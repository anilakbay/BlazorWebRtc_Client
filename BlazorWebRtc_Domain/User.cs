using BlazorWebRtc_Domain.Common;

namespace BlazorWebRtc_Domain
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public virtual List<UserFriend> Friends { get; set; } = new List<UserFriend>();
    }
}
