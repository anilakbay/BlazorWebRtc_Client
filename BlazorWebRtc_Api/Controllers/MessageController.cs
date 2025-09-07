using BlazorWebRtc_Application.Features.Commands.MessageCommand.SendMessage;
using BlazorWebRtc_Application.Features.Queries.MessageQuery;
using BlazorWebRtc_Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMessage(SendMessageCommand command)
        {
            return Ok(await _messageService.SendMessage(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetMessageList(string userId)
        {
            GetMessagesQuery query = new GetMessagesQuery();
            query.MessageUserId = userId;
            return Ok(await _messageService.GetListMessage(query));
        }
    }
}