using BlazorWebRtc_Application.DTO.UserFriend;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Queries.UserFriend
{
    public class UserFriendListQuery : IRequestHandler<UserFriendListQuery, List<UserFriendDTO>>
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private Guid userId;
        public UserFriendListQuery(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<List<UserFriendDTO>> Handle(UserFriendListQuery request, CancellationToken cancellationToken)
        {
            userId = Guid.Parse (_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var friends = await _context.UserFriends.Include(x => x.ReceiverUser).Include(x => x.Requester).Where(x=>x.RequesterId == userId || x.ReceiverUserId == userId).ToListAsync();

            List<UserFriendDTO> userFriendList = new();
            foreach (var friend in friends)
            {
                UserFriendDTO dto = new();

                if (userId == friend.RequesterId)
                {
                    dto.UserId = friend.ReceiverUserId;
                    dto.UserName = friend.ReceiverUser.UserName;                   
                    dto.ProfilePicture = friend.ReceiverUser.ProfilePicture;
                    dto.Email = friend.ReceiverUser.Email;
                }

                if (userId == friend.ReceiverUserId)
                {
                    dto.UserId = friend.RequesterId;
                    dto.UserName = friend.Requester.UserName;
                    dto.ProfilePicture = friend.Requester.ProfilePicture;
                    dto.Email = friend.Requester.Email;
                }
                userFriendList.Add(dto);
            }
            return userFriendList;
        }
    }
}
