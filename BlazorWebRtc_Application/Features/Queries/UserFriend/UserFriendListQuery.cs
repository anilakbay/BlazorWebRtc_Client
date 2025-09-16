using BlazorWebRtc_Application.DTO.UserFriend;
using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Queries.UserFriend
{
    public class UserFriendListQuery : IRequest<List<UserFriendDTO>>
    {
    }

    public class UserFriendListQueryHandler : IRequestHandler<UserFriendListQuery, List<UserFriendDTO>>
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserFriendListQueryHandler(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }


        private Guid GetCurrentUserId()
        {
            var userIdClaim = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
        }

      

        private static List<UserFriendDTO> MapToUserFriendDTOs(List<BlazorWebRtc_Domain.UserFriend> friends, Guid currentUserId)
        {
            var userFriendList = new List<UserFriendDTO>();

            foreach (var friend in friends)
            {
                var dto = CreateUserFriendDTO(friend, currentUserId);
                if (dto != null)
                {
                    userFriendList.Add(dto);
                }
            }

            return userFriendList;
        }

        private static UserFriendDTO? CreateUserFriendDTO(BlazorWebRtc_Domain.UserFriend friend, Guid currentUserId)
        {
            if (currentUserId == friend.RequesterId)
            {
                return new UserFriendDTO
                {
                    UserId = friend.ReceiverUserId,
                    UserName = friend.ReceiverUser?.UserName ?? string.Empty,
                    ProfilePicture = friend.ReceiverUser?.ProfilePicture,
                    Email = friend.ReceiverUser?.Email ?? string.Empty
                };
            }

            if (currentUserId == friend.ReceiverUserId)
            {
                return new UserFriendDTO
                {
                    UserId = friend.RequesterId,
                    UserName = friend.Requester?.UserName ?? string.Empty,
                    ProfilePicture = friend.Requester?.ProfilePicture,
                    Email = friend.Requester?.Email ?? string.Empty
                };
            }

            return null;
        }

        public async Task<List<UserFriendDTO>> Handle(UserFriendListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();
            
            var friends = await _context.UserFriends
                .Include(uf => uf.Requester)
                .Include(uf => uf.ReceiverUser)
                .Where(uf => uf.RequesterId == currentUserId || uf.ReceiverUserId == currentUserId)
                .ToListAsync(cancellationToken);

            return MapToUserFriendDTOs(friends, currentUserId);
        }
    }
}