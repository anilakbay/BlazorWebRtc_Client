using BlazorWebRtc_Application.DTO;
using BlazorWebRtc_Application.Features.Queries.UserInfo;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;
        private readonly BaseResponseModel _responseModel;
        public UserService(IMediator mediator, BaseResponseModel responseModel)
        {
            _responseModel = responseModel;
            _mediator = mediator;
        }

        public async Task<List<UserDTOResponseModel>> GetUserList()
        {
            UserListQuery query = new UserListQuery();
            var response = await _mediator.Send(query);
            return response ?? new List<UserDTOResponseModel>();
        }
    }
}
