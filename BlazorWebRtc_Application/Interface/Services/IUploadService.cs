using BlazorWebRtc_Application.Features.Commands.Upload;
using BlazorWebRtc_Application.Models;

namespace BlazorWebRtc_Application.Interface.Services
{
    public interface IUploadService
    {
        Task<BaseResponseModel> UploadFile (UploadCommand command);
    }
}
