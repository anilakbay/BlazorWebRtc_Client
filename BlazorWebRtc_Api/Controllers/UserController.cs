using BlazorWebRtc_Application.Features.Commands.Account.Login;
using BlazorWebRtc_Application.Features.Commands.Account.Register;
using BlazorWebRtc_Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Kullanıcı kayıt (JSON body ile)
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _accountService.SignUp(command);
            return Ok(result);
        }

        // Kullanıcı giriş (JSON body ile)
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _accountService.SignIn(command);
            return Ok(result);
        }
    }
}
