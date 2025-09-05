using BlazorWebRtc_Domain;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.RequestFeature
{
    public class RequestCommand : IRequest<bool>
    {
        public Status Status { get; set; } = Status.pending;            
        public Guid ReceiverUserId { get; set; }
        public Guid SenderUserId { get; set; }
    }
}
