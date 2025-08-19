using BlazorWebRtc_Application.Features.Commands.Account.Register;
using BlazorWebRtc_Application.Models;

namespace BlazorWebRtc_Application.Interface.Services
{
    public interface IAccountService
    {
        // IAccountService arayüzü, hesap işlemleri için servis sözleşmesini tanımlar.
        // SignUp metodu, dışarıdan RegisterCommand alır (kayıt bilgileri) 
        // ve işlem sonucunu standartlaştırılmış BaseResponseModel olarak döner.
        public interface IAccountService
        {
            Task<BaseResponseModel> SignUp(RegisterCommand command);
            
        }
    }
}
