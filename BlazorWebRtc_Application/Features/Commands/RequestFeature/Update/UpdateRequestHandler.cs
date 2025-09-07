using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Application.Features.Commands.RequestFeature.Update
{
    public class UpdateRequestHandler : IRequestHandler<UpdateRequestCommand, Request>
    {
        private readonly AppDbContext _context;
        public UpdateRequestHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Request> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            var requestObj = await _context.Requests.FirstOrDefaultAsync(x=>x.Id == request.RequestId);
            requestObj.Status = request.status;
            if (await _context.SaveChangesAsync() > 0)
            {
                return requestObj;
            }
            return null;
        }
    }
}
