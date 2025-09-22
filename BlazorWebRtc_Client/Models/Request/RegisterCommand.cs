using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlazorWebRtc_Client.Models.Request
{
    public class RegisterCommand
    {
        [Required(ErrorMessage = "Username is Required")]
        public string UserName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "ConfirmPassword is Required")]
        public string ConfirmPassword { get; set; } = string.Empty;       
        [Required(ErrorMessage = "ProfilePicture is Required")]
        public IFormFile? ProfilePicture { get; set; }
    }
}
