using BlazorWebRtc_Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebRtc_Domain
{
    public class Message:BaseEntity
    {
        public string MessageContent { get; set; }
        [ForeignKey(nameof(MessageRoom))]
        public Guid MessageRoomId { get; set; }
        public MessageRoom MessageRoom { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
