using BlazorWebRtc_Domain;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.RequestFeature
{
    public class RequestFriendShipCommand : IRequest<bool>
    {
        public Status Status { get; set; } = Status.pending;
        public Guid ReceiverUserId { get; set; }
    }
}
