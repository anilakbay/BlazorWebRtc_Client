using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Application.Features.Commands.RequestFeature
{
    public class RequestHandler : IRequestHandler<RequestCommand, bool>
    {
        private readonly AppDbContext _context;

        public RequestHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            var requestEntity = new Request
            {
                Status = request.Status,
                ReceiverUserId = request.ReceiverUserId,
                SenderUserId = request.SenderUserId
            };

            await _context.Requests.AddAsync(requestEntity, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            
            return result > 0;
        }
    }
}
