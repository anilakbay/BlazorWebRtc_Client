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

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterCommand command)
        {           
            return Ok(await _accountService.SignUp(command));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginCommand command)
        {
            return Ok(await _accountService.SignIn(command));
        }
    }
}
