using BlazorWebRtc_Application.Hubs;
using BlazorWebRtc_Application.Interface.Services.Manager;
using BlazorWebRtc_Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BlazorWebRtc_Api.Controllers
{
    public class NotificationModel
    {
        public string CallerUserId { get; set; }
        public string AnswerUserId { get; set; }

    }
    public class UserResponseModel
    {
        public string image { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }

    }
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<UserHub> hubContext;
        private readonly IConnectionManager connectionManager;
        private readonly AppDbContext _context;
        public NotificationController(AppDbContext context, IHubContext<UserHub> hubContext, IConnectionManager connectionManager)
        {
            _context = context;
            this.connectionManager = connectionManager;
            this.hubContext = hubContext;
        }

        public object JsonConvert { get; private set; }

        [HttpPost]
        public async Task NotificationTrigger(NotificationModel notify)
        {

            var userId = connectionManager.GetConnectionByUserIdSingleObj(notify.AnswerUserId);
            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == Guid.Parse(notify.CallerUserId));
                UserResponseModel responseModel = new()
                {
                    image = result.ProfilePicture,
                    userName = result.UserName,
                    userId = result.Id.ToString(),
                };
                var serializeMessage = JsonConvert.SerializeObject(responseModel);
                await hubContext.Clients.Clients(userId).SendAsync("Notify", serializeMessage);
            }
        }


    }
}