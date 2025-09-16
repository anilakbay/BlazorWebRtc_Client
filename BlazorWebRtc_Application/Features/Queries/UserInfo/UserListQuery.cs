using BlazorWebRtc_Application.DTO;
using MediatR;

namespace BlazorWebRtc_Application.Features.Queries.UserInfo
{
    public class UserListQuery: IRequest<List<UserDTOResponseModel>>
    {
    }
}
