using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Commands.RequestFeature
{
    public class RequestHandler : IRequestHandler<RequestCommand, bool>
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private string? userId;
        public RequestHandler(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<bool> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.ReceiverUserId);




            if (result is not null)
            {
                userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var requests = await _context.Requests.Where(x => x.ReceiverUserId == request.ReceiverUserId).ToListAsync();

                foreach (var item in requests)
                {
                    if (item.SenderUserId == Guid.Parse(userId) || item.ReceiverUserId == Guid.Parse(userId))
                    {
                        return false;
                    }
                }


                Request requestObj = new Request();
                requestObj.ReceiverUserId = request.ReceiverUserId;
                requestObj.SenderUserId = Guid.Parse(userId);
                requestObj.Status = requestObj.Status;
                await _context.Requests.AddAsync(requestObj);
                if (await _context.SaveChangesAsync(cancellationToken) > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}