using BlazorWebRtc_Application.Features.Commands.RequestFeature;
using BlazorWebRtc_Application.Features.Commands.RequestFeature.Update;
using BlazorWebRtc_Application.Features.Queries.RequestFeature;
using BlazorWebRtc_Application.Models;

namespace BlazorWebRtc_Application.Interface.Services
{
    public interface IRequestService
    {
        Task<BaseResponseModel> SendRequest(RequestCommand command);
        Task<BaseResponseModel> GetRequests(RequestsQuery query);
        Task<BaseResponseModel> UpdateRequest(RequestCommand command);
        Task<object?> GetRequestList(RequestsQuery query);
        Task<object?> UpdateRequest(UpdateRequestCommand command);
    }
}
