using BlazorWebRtc_Application.Features.Commands.MessageCommand.SendMessage;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMediator mediator;
        private readonly BaseResponseModel _responseModel;
        public MessageService(IMediator mediator, BaseResponseModel responseModel)
        {
            this.mediator = mediator;
            _responseModel = responseModel;
        }

        public async Task<BaseResponseModel> SendMessage(SendMessageCommand command)
        {
            var response = await mediator.Send(command);
            if (response)
            {
                _responseModel.IsSuccess = true;
                _responseModel.Data = response;
                return _responseModel;
            }
            _responseModel.IsSuccess = false;
            return _responseModel;
        }
    }
}
