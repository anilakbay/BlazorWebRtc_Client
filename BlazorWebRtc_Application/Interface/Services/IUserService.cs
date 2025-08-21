using BlazorWebRtc_Application.Models;

namespace BlazorWebRtc_Application.Interface.Services
{
    public interface IUserService
    {
        Task<BaseResponseModel> GetUserList();
    }
}
