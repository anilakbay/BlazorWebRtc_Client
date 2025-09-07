using BlazorWebRtc_Application.Features.Commands.RequestFeature;
using BlazorWebRtc_Application.Features.Commands.RequestFeature.Update;
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
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFriendRequests([FromRoute] string userId)
        {
            RequestsQuery query = new RequestsQuery();
            query.UserId = Guid.Parse(userId);
            return Ok(await _requestService.GetRequestList(query));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRequest(UpdateRequestCommand command)
        {
            return Ok(await _requestService.UpdateRequest(command));
        }
    }
}
