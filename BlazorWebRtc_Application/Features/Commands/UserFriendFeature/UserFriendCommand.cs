
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.UserFriendFeature
{
    public class UserFriendCommand : IRequest<bool>
    {
        public Guid RequesterId { get; set; }      
        public Guid ReceiverUserId { get; set; }
    }
}
