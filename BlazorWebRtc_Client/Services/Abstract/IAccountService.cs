using BlazorWebRtc_Client.Models.Request;
using BlazorWebRtc_Client.Models.Response;

namespace BlazorWebRtc_Client.Services.Abstract
{

    public interface IAccountService
    {
        Task<ResponseModel> SignUp(RegisterCommand command);
    }
}
