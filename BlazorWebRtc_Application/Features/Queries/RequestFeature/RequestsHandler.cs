using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BlazorWebRtc_Application.DTO.Request;

namespace BlazorWebRtc_Application.Features.Queries.RequestFeature
{
    public class RequestsHandler : IRequestHandler<RequestsQuery, List<GetRequestDTO>>
    {
        private readonly AppDbContext _context;

        public RequestsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetRequestDTO>> Handle(RequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _context.Requests
                .Include(r => r.SenderUser)
                .Where(x => x.ReceiverUserId == request.UserId && x.Status == BlazorWebRtc_Domain.Status.pending)
                .ToListAsync();

            var requestList = new List<GetRequestDTO>();

            foreach (var item in requests)
            {
                var dto = new GetRequestDTO
                {
                    UserId = item.SenderUser.Id.ToString(),
                    Email = item.SenderUser.Email,
                    ProfilePicture = item.SenderUser.ProfilePicture
                };
                requestList.Add(dto);
            }

            if (requestList.Any())
            {
                return requestList;
            }
            return null;

        }
        
    }
}
