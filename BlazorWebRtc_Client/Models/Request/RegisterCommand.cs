using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlazorWebRtc_Client.Models.Request
{
    public class RegisterCommand
    {
        [Required(ErrorMessage = "Username is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Username is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string ConfirmPassword { get; set; }       
        public IFormFile? ProfilePicture { get; set; }
        [Required(ErrorMessage = "ProfilePicture is Required")]
    }
}
