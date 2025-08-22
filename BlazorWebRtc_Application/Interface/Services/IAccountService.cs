using BlazorWebRtc_Application.Features.Commands.Account.Login;
using BlazorWebRtc_Application.Features.Commands.Account.Register;
using BlazorWebRtc_Application.Models;

namespace BlazorWebRtc_Application.Interface.Services
{
    public interface IAccountService
    {
        Task<BaseResponseModel> SignIn(LoginCommand command); // Mevcut kullanıcı giriş işlemi, başarılı ise token döner
        Task<BaseResponseModel> SignUp(RegisterCommand command); // Yeni kullanıcı kaydı, başarılı ise true döner
    }
}
