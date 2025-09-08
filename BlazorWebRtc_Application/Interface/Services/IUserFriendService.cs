using BlazorWebRtc_Application.Features.Commands.UserFriendFeature;
using BlazorWebRtc_Application.Features.Queries.UserFriend;
using BlazorWebRtc_Application.Models;

namespace BlazorWebRtc_Application.Interface.Services
{
    public interface IUserFriendService
    {
        Task<BaseResponseModel> AddFriendship(UserFriendCommand command);
        Task<BaseResponseModel> DeleteFriendship(DeleteFriendshipCommand command);
        Task<BaseResponseModel> GetFriendshipList(UserFriendListQuery query);
    }
}
