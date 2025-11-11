using BlazorWebRtc_Client.Models.Response;
using BlazorWebRtc_Client.Services.Abstract;
using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Client.Services.Concrete
{
    public class UploadService : IUploadService
    {
        private readonly HttpClient _httpClient;
        public UploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ResponseModel> UploadFileAsync(IFormFile file, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
