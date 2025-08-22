using BlazorWebRtc_Application.Features.Commands.RequestFeature;
using BlazorWebRtc_Application.Features.Queries.RequestFeature;
using BlazorWebRtc_Application.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // POST: api/Request
        // JSON body gönderilecek (application/json)
        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody] RequestCommand command)
        {
            var result = await _requestService.SendRequest(command);
            return Ok(result);
        }

        // GET: api/Request?UserId=xxxx
        // Query string üzerinden çalışacak
        [HttpGet]
        public async Task<IActionResult> GetFriendRequests([FromQuery] RequestsQuery query)
        {
            var result = await _requestService.GetRequests(query);
            return Ok(result);
        }
    }
}
