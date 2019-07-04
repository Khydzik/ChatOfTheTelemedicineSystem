using GraduateWork.Server.Common;
using System;

namespace GraduateWork.Server.Data.Models
{

    public class GroupMessageEntity
    {
        public Guid Id { get; set; }
        public Guid GroupChatId { get; set; }
        public MessageType Type { get; set; }
        public string Content { get; set; }
        public Guid? FileId { get; set; }

        public GroupChatEntity GroupChat { get; set; }
        public FileEntity File { get; set; }

    }
}
