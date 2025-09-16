using BlazorWebRtc_Domain;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebRtc_Application.Features.Commands.MessageCommand.SendMessage
{
    public class SendMessageCommand: IRequest<bool>
    {
        public string MessageContent { get; set; } = string.Empty;                             
        public Guid ReceiverUserId { get; set; }
    }
}
