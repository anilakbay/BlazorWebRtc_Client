using BlazorWebRtc_Domain;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.RequestFeature.Update
{
    public class UpdateRequestCommand: IRequest<Request>
    {
        public Guid RequestId { get; set; }
        public Status status { get; set; }
    }
}
