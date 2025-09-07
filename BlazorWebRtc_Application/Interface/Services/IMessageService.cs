using BlazorWebRtc_Application.Features.Commands.MessageCommand.SendMessage;
using BlazorWebRtc_Application.Features.Queries.MessageQuery;
using BlazorWebRtc_Application.Models;

namespace BlazorWebRtc_Application.Interface.Services
{
    public interface IMessageService
    {
        Task<BaseResponseModel> SendMessage(SendMessageCommand command);
        Task<BaseResponseModel> GetListMessage(GetMessagesQuery query);
    }
}