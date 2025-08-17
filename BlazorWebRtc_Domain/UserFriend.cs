using BlazorWebRtc_Domain.Common;

namespace BlazorWebRtc_Domain
{
    public class UserFriend: BaseEntity
    {
        public virtual List<User>Users { get; set; }
    }
}
