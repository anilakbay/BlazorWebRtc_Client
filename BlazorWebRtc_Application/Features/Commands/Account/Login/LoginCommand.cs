using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.Account.Login
{
    public class LoginCommand : IRequest<(bool, string)>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
