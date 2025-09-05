using BlazorWebRtc_Application.Features.Commands.UserFriendFeature;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Services
{
    public class UserFriendService : IUserFriendService
    {
        private readonly IMediator mediator;
        
        public UserFriendService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<BaseResponseModel> AddFriendship(UserFriendCommand command)
        {
            var result = await mediator.Send(command);
            return new BaseResponseModel
            {
                IsSuccess = result,
                Message = result ? "Arkadaşlık isteği başarıyla gönderildi" : "Arkadaşlık isteği gönderilemedi"
            };
        }
    }
}
