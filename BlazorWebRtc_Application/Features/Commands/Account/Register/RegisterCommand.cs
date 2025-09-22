using BlazorWebRtc_Domain;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.Account.Register
{
    public class RegisterCommand:IRequest<User>
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; } 

    }
}
