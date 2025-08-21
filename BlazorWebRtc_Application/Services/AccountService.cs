using BlazorWebRtc_Application.Features.Commands.Account.Login;
using BlazorWebRtc_Application.Features.Commands.Account.Register;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Services
{
    public class AccountService : IAccountService
    {
        // AccountService sınıfının içinde kullanılan alanlar

        private readonly IMediator mediator;

        // Mediator, uygulamada komutları ve sorguları ilgili handler'lara göndermek için kullanılır.
        // readonly olduğu için sadece constructor içinde atanabilir, sonrasında değiştirilemez.

        private readonly BaseResponseModel _responseModel;

        // Metodlardan döndürülecek response'u tutar. 
        // İşlem başarılı mı, veri var mı gibi bilgileri burada saklarız.


        // Constructor: AccountService sınıfı oluşturulurken çağrılır
        public AccountService(IMediator mediator, BaseResponseModel responseModel)
        {
            this.mediator = mediator;
            // Parametre olarak gelen mediator nesnesini sınıf içinde kullanılacak alana atıyoruz

            _responseModel = responseModel;
            // Parametre olarak gelen responseModel nesnesini sınıf içinde kullanılacak alana atıyoruz
        }


        public async Task<BaseResponseModel> SignIn(LoginCommand command)
        {
            // 1. Kullanıcının giriş bilgilerini mediator aracılığıyla gönderiyoruz
            //    response bir Tuple döndürüyor: Item1 = işlem başarılı mı, Item2 = token
            var response = await mediator.Send(command);

            // 2. Eğer giriş başarılıysa (Item1 true ise)
            if (response.Item1)
            {
                _responseModel.IsSuccess = true;       // İşlem başarılı
                _responseModel.Data = response.Item2;  // Token'ı response'a ekle
                return _responseModel;                 // Başarılı response'u dön
            }

            // 3. Eğer giriş başarısızsa
            _responseModel.IsSuccess = false;         // İşlem başarısız
            return _responseModel;                    // Başarısız response'u dön
        }


        public async Task<BaseResponseModel> SignUp(RegisterCommand command)
        {
            // Kullanıcının kayıt bilgilerini mediator aracılığıyla gönderiyoruz
            var response = await mediator.Send(command);

            // Eğer mediator geçerli bir cevap dönerse (response null değilse), kayıt başarılı demektir
            if (response != null)
            {
                _responseModel.IsSuccess = true;    // İşlem başarılı
                return _responseModel;              // Başarılı response'u dön
            }

            // Eğer mediator null dönerse, kayıt işlemi başarısız
            _responseModel.IsSuccess = false;       // İşlem başarısız
            return _responseModel;                  // Başarısız response'u dön
        }

    }


}
