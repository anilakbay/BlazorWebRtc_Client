using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Application.Features.Queries.RequestFeature
{
    public class RequestsHandler : IRequestHandler<RequestsQuery, List<Request>>
    {
        private readonly AppDbContext _context;
        public RequestsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Request>> Handle(RequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _context.Requests.Where(x => x.ReceiverUserId == request.UserId).ToListAsync();

            if(requests.Any())
            {
                return requests;
            }
            return null;
        }
    }
}
