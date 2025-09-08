using BlazorWebRtc_Application.Features.Commands.RequestFeature;
using BlazorWebRtc_Application.Features.Commands.RequestFeature.Update;
using BlazorWebRtc_Application.Features.Commands.UserFriendFeature;
using BlazorWebRtc_Application.Features.Queries.RequestFeature;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IMediator _mediator;
        private readonly BaseResponseModel _responseModel;
        private readonly IUserFriendService _userFriendService;
        public RequestService(IMediator mediator, IUserFriendService userFriendService, BaseResponseModel responseModel)
        {
            _userFriendService = userFriendService;
            _responseModel = responseModel;
            _mediator = mediator;
        }

        public async Task<BaseResponseModel> GetRequestList(RequestsQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
            {

                _responseModel.isSuccess = false;
                return _responseModel;
            }

            _responseModel.isSuccess = true;
            _responseModel.Data = result;
            return _responseModel;
        }

        public async Task<BaseResponseModel> SendRequest(RequestCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                _responseModel.isSuccess = true;
                return _responseModel;
            }

            _responseModel.isSuccess = false;
            return _responseModel;
        }

        public async Task<BaseResponseModel> UpdateRequest(UpdateRequestCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {

                _responseModel.isSuccess = false;
                return _responseModel;
            }
            if (command.Status == BlazorWebRtc_Domain.Status.accept)
            {
                UserFriendCommand userFriendCommand = new();
                userFriendCommand.RequesterId = result.SenderUserId;
                userFriendCommand.ReceiverUserId = result.ReceiverUserId;

                var response = await _userFriendService.AddFriendship(userFriendCommand);
                if (response.isSuccess)
                {
                    _responseModel.isSuccess = true;
                    return _responseModel;
                }
            }

            _responseModel.isSuccess = false;
            return _responseModel;

        }
    }
}