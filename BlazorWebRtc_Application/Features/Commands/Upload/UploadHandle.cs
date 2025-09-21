using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Application.Features.Commands.Upload
{
    public class UploadHandle : IRequestHandler<UploadCommand, bool>
    {
        private readonly AppDbContext _context; 
        public UploadHandle(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UploadCommand request, CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            result.ProfilePicture = request.file;

            if (await _context.SaveChangesAsync() > 0)
            {              
                return true;
            }
            return false;
        }
    }
}
