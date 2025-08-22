using BlazorWebRtc_Application.Features.Commands.Account.Login;
using BlazorWebRtc_Application.Features.Commands.Account.Register;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Services
{
    /// <summary>
    /// Kullanıcı hesap işlemlerini (giriş/kayıt) yöneten servis sınıfı.
    /// IMediator aracılığıyla komutları ilgili handler'lara iletir.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Servis metodlarından dönecek standart yanıt modeli
        /// </summary>
        public AccountService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Kullanıcı giriş işlemi.
        /// </summary>
        /// <param name="command">LoginCommand ile kullanıcı bilgileri</param>
        /// <returns>BaseResponseModel, giriş sonucu ve token bilgisi</returns>
        public async Task<BaseResponseModel> SignIn(LoginCommand command)
        {
            // Giriş isteğini handler’a gönderiyoruz (Tuple<bool, string> dönüyor)
            var response = await _mediator.Send(command);

            var result = new BaseResponseModel();

            if (response.Item1) // Giriş başarılıysa
            {
                result.IsSuccess = true;
                result.Data = response.Item2; // Token bilgisi
            }
            else // Giriş başarısızsa
            {
                result.IsSuccess = false;
            }

            return result;
        }

        /// <summary>
        /// Kullanıcı kayıt işlemi.
        /// </summary>
        /// <param name="command">RegisterCommand ile kayıt bilgileri</param>
        /// <returns>BaseResponseModel, kayıt işleminin sonucu</returns>
        public async Task<BaseResponseModel> SignUp(RegisterCommand command)
        {
            // Kayıt isteğini handler’a gönderiyoruz
            var response = await _mediator.Send(command);

            var result = new BaseResponseModel
            {
                IsSuccess = response != null // null değilse kayıt başarılı
            };

            return result;
        }
    }
}
