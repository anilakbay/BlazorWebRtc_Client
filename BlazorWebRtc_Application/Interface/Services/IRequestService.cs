using BlazorWebRtc_Application.Features.Commands.RequestFeature;
using BlazorWebRtc_Application.Features.Commands.RequestFeature.Update;
using BlazorWebRtc_Application.Features.Queries.RequestFeature;
using BlazorWebRtc_Application.Models;

namespace BlazorWebRtc_Application.Interface.Services
{
    public interface IRequestService
    {
        Task<BaseResponseModel> SendRequest(RequestCommand command);
        Task<BaseResponseModel> SendFriendshipRequest(RequestFriendShipCommand command);
        Task<BaseResponseModel> GetRequestList(RequestsQuery query);
        Task<BaseResponseModel> UpdateRequest(UpdateRequestCommand command);
    }
}
