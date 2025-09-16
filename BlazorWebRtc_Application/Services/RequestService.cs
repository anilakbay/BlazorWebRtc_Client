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
                return new BaseResponseModel
                {
                    isSuccess = false,
                    Message = "No requests found"
                };
            }

            return new BaseResponseModel
            {
                isSuccess = true,
                Data = result,
                Message = "Requests retrieved successfully"
            };
        }

        public async Task<BaseResponseModel> SendRequest(RequestCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return new BaseResponseModel
                {
                    isSuccess = true,
                    Message = "Request sent successfully"
                };
            }

            return new BaseResponseModel
            {
                isSuccess = false,
                Message = "Failed to send request"
            };
        }

        public async Task<BaseResponseModel> SendFriendshipRequest(RequestFriendShipCommand command)
        {
            var requestCommand = new RequestCommand
            {
                ReceiverUserId = command.ReceiverUserId,
                Status = command.Status
            };
            
            return await SendRequest(requestCommand);
        }

        public async Task<BaseResponseModel> UpdateRequest(UpdateRequestCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return new BaseResponseModel
                {
                    isSuccess = false,
                    Message = "Request not found"
                };
            }
            
            if (command.status == BlazorWebRtc_Domain.Status.accepted)
            {
                UserFriendCommand userFriendCommand = new();
                userFriendCommand.RequesterId = result.SenderUserId;
                userFriendCommand.ReceiverUserId = result.ReceiverUserId;

                var response = await _userFriendService.AddFriendship(userFriendCommand);
                if (response.isSuccess)
                {
                    return new BaseResponseModel
                    {
                        isSuccess = true,
                        Message = "Request accepted and friendship added"
                    };
                }
            }

            return new BaseResponseModel
            {
                isSuccess = true,
                Message = "Request updated successfully"
            };
        }

        public async Task<BaseResponseModel> GetRequests(RequestsQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return new BaseResponseModel
                {
                    isSuccess = false,
                    Message = "No requests found"
                };
            }

            return new BaseResponseModel
            {
                isSuccess = true,
                Data = result,
                Message = "Requests retrieved successfully"
            };
        }

    }
}