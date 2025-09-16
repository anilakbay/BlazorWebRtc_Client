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
        private readonly IHttpContextAccessor _contextAccessor;

        public SendMessageHandler(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return false; // Kullanıcı kimliği bulunamadı
            }

            MessageRoom message = new MessageRoom
            {
                SenderUserId = Guid.Parse(userId),
                MessageContent = request.MessageContent,
                ReceiverUserId = request.ReceiverUserId
            };

            await _context.MessageRooms.AddAsync(message);
            if (await _context.SaveChangesAsync(cancellationToken) > 0)
            {
                return true;
            }
            return false;
        }
    }
}