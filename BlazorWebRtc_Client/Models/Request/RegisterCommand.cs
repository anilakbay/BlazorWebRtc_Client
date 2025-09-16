using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Client.Models.Request
{
    public class RegisterCommand
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public IFormFile? ProfilePicture { get; set; }
    }
}
