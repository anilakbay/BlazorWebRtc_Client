using BlazorWebRtc_Domain;
using BlazorWebRtc_Application.Features.Queries.RequestFeature;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BlazorWebRtc_Application.DTO.Request;

namespace BlazorWebRtc_Application.Features.Commands.RequestFeature
{
    public class RequestHandler : IRequestHandler<RequestsQuery, List<GetRequestDTO>>
    {
        private readonly AppDbContext _context;
        private readonly object requestDTO;

        public RequestHandler(AppDbContext context)
        {
            
            _context = context;
        }

        public async Task<GetRequestDTO> Handle(RequestsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Requests.Include(x=>x.SenderUser).Where(x=>x.ReceiverUserId == request.UserId).ToListAsync();

            List<GetRequestDTO> requestList = new();

            foreach (var item in requests)
            {
                GetRequestDTO getRequestDTO = new();
                requestDTO.ProfilePicture = item.SenderUser.ProfilePicture;
                requestDTO.UserName = item.SenderUser.UserName;
                requestDTO.Email = item.SenderUser.Email;
                requestList.Add(getRequestDTO);

            }

            if (result is not null)
            {
               return requests;
            }
            return null;
        }
       
    }
}
