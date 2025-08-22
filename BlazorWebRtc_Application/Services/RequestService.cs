using BlazorWebRtc_Application.Features.Commands.RequestFeature;
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
        public RequestService(IMediator mediator, BaseResponseModel responseModel)
        {
            _responseModel = responseModel;
            _mediator = mediator;
        }

        public async Task<BaseResponseModel> GetRequests(RequestsQuery query)
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
    }
}
