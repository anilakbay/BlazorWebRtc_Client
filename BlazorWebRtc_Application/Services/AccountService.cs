using BlazorWebRtc_Application.Features.Commands.Account.Register;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMediator mediator;
        private readonly BaseResponseModel _responseModel;
        public AccountService(IMediator mediator, BaseResponseModel responseModel)
        {
            this.mediator = mediator;
            _responseModel = responseModel;
        }

        public async Task<BaseResponseModel> SignUp(RegisterCommand command)
        {
            var response = await mediator.Send(command);
            if (response != null)
            {
                _responseModel.IsSuccess = true;
                _responseModel.Message = "User registered successfully";
                _responseModel.Data = response;
                return _responseModel;
            }
            else
            {
                _responseModel.IsSuccess = false;
                _responseModel.Message = "User registration failed";
                return _responseModel;

            }
          

        }
    }


}
