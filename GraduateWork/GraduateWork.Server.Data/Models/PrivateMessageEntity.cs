using GraduateWork.Server.Common;
using System;

namespace GraduateWork.Server.Data.Models
{
    public class PrivateMessageEntity
    {
        public Guid Id { get; set; }
        public Guid PrivatChatId { get; set; }
        public MessageType Type { get; set; }
        public string Content { get; set; }
        public Guid? FileId { get; set; }

        public PrivateChatEntity PrivateChat { get; set; }
        public FileEntity File { get; set; }
    }
}
