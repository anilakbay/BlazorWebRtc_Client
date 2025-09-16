using BlazorWebRtc_Client.Models.Request;

namespace BlazorWebRtc_Client.Services.Abstract
{

    public interface IAccountService
    {
        Task SignUp(RegisterCommand command);
    }
}
