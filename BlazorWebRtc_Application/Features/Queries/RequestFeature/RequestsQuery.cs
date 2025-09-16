using BlazorWebRtc_Application.DTO.Request;
using BlazorWebRtc_Domain;
using MediatR;

namespace BlazorWebRtc_Application.Features.Queries.RequestFeature
{
    public class RequestsQuery: IRequest<List<GetRequestDTO>?>
    {
        public Guid UserId { get; set; }      
    }
}
