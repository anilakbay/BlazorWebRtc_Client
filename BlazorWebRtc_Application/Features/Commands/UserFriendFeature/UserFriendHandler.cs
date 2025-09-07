using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using BlazorWebRtc_Application.Features.Commands.UserFriendFeature;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.UserFriend
{
    public class UserFriendHandler : IRequestHandler<UserFriendCommand, bool>
    {
        private readonly AppDbContext _context;
        
        public UserFriendHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UserFriendCommand request, CancellationToken cancellationToken)
        {
            BlazorWebRtc_Domain.UserFriend friendship = new BlazorWebRtc_Domain.UserFriend();

            friendship.RequesterId = request.RequesterId;
            friendship.ReceiverUserId = request.ReceiverUserId;

            await _context.UserFriends.AddAsync(friendship, cancellationToken);
            if (await _context.SaveChangesAsync(cancellationToken) > 0)
            {
                return true;               
            }
            return false;
        }
    }
}
