using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Application.Features.Commands.Upload
{
    public class UploadCommand: IRequest<bool>
    {
        public IFormFile file { get; set; }
        public Guid UserId { get; set; } 
    }
}
