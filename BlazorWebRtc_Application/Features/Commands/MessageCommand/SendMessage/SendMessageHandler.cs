using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Commands.MessageCommand.SendMessage
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, bool>
    {
        private readonly AppDbContext _context;
        private string UserId;
        private readonly IHttpContextAccessor _contextAccessor;
        public SendMessageHandler(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)? Value;
            MessageRoom message = new MessageRoom();
            message.SenderUserId = Guid.Parse(userId);
            message.MessageContent = request.MessageContent;
            message.ReceiverUserId = request.ReceiverUserId;            

            await _context.MessageRooms.AddAsync(message);
            if (await _context.SaveChangesAsync(cancellationToken) > 0)
            {
                return true;
            }
            return false;

        }
    }
}
