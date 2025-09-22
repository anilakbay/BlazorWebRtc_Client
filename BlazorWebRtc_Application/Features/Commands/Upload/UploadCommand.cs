using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Application.Features.Commands.Upload
{
    public class UploadCommand: IRequest<bool>
    {
        public string FileUrl { get; set; } = string.Empty;
        public Guid UserId { get; set; } 
    }
}
