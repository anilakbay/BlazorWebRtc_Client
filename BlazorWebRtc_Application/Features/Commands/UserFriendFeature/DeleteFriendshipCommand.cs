using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.UserFriendFeature
{
    public class DeleteFriendshipCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
