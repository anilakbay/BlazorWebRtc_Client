using BlazorWebRtc_Client.Models.Response;
using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Client.Services.Abstract
{
    public interface IUploadService
    {
        Task<ResponseModel> UploadFileAsync(IFormFile file, string userId);
    }
}
