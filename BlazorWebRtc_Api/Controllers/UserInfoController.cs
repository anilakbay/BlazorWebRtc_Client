using BlazorWebRtc_Application.Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserInfoController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("UserList")]
        public async Task<IActionResult> GetUserList()
        {
            return Ok(await _userService.GetUserList());
        }
    }
}
