using BlazorWebRtc_Application.DTO;
using BlazorWebRtc_Domain;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Queries.UserInfo
{
    public class UserListHandler : IRequestHandler<UserListQuery, List<UserDTOResponseModel>>
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserListHandler(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        // Kullanıcı listesi sorgusu
        public async Task<List<UserDTOResponseModel>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            // Tüm kullanıcıları getir
            List<User> users = await _context.Users.ToListAsync(cancellationToken);

            // Mevcut kullanıcı ID'si
            var currentUserId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<UserDTOResponseModel> userDTOs = new List<UserDTOResponseModel>();

            if (users != null)
            {
                foreach (var item in users)
                {
                    // Mevcut kullanıcıyı listeye ekleme
                    if (currentUserId != null && currentUserId != item.Id.ToString())
                    {
                        var dto = new UserDTOResponseModel
                        {
                            UserId = item.Id,
                            UserName = item.UserName,
                            Email = item.Email,
                            ProfilePicture = item.ProfilePicture
                        };
                        userDTOs.Add(dto);
                    }
                }
            }

            return userDTOs; // Boş olsa bile boş liste döner
        }
    }
}
