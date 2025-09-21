using BlazorWebRtc_Application.Features.Commands.Upload;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Services
{
    public class UploadService : IUploadService
    {
        private readonly IMediator mediator;
        private readonly BaseResponseModel responseModel;
        public UploadService(IMediator mediator, BaseResponseModel responseModel)
        {
            this.responseModel = responseModel;
            this.mediator = mediator;
        }

        public async Task<BaseResponseModel> UploadFile(UploadCommand command)
        {
            var result = await mediator.Send(command);
            if (result)
            {
                responseModel.IsSuccess = true;
                return responseModel;
            }
            responseModel.IsSuccess = false;
            return responseModel;
        }
    }
}
