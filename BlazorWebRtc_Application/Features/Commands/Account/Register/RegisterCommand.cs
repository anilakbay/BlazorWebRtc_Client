using BlazorWebRtc_Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Application.Features.Commands.Account.Register
{
    public class RegisterCommand:IRequest<BaseResponseModel>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IFormFile? ProfilePicture { get; set; } 

    }
}
