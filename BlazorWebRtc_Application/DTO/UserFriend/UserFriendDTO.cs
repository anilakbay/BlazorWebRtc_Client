using BlazorWebRtc_Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebRtc_Application.DTO.UserFriend
{
    public class UserFriendDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
    }
}
