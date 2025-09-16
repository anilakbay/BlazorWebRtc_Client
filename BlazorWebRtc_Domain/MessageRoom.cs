using BlazorWebRtc_Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebRtc_Domain
{
    public class MessageRoom:BaseEntity
    {
        public string MessageContent { get; set; } = string.Empty;
        [ForeignKey(nameof(SenderUser))]
        public Guid? SenderUserId { get; set; }
        public User? SenderUser { get; set; }
        [ForeignKey(nameof(ReceiverUser))]
        public Guid ReceiverUserId { get; set; }
        public User? ReceiverUser { get; set; }        
    }
}
