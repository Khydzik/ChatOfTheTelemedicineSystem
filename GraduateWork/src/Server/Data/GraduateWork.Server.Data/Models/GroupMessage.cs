using System;
using System.Collections.Generic;
using System.Text;

namespace GraduateWork.Server.Data.Models
{
    public enum Type
    {
        Text,
        Photo,
        File
    }

    public class GroupMessage
    {
        public Guid Id { get; set; }
        public Guid GroupChatId { get; set; }
        public Type Type { get; set; }
        public string Content { get; set; }
        public Guid FileId { get; set; }

        public GroupChat GroupChat { get; set; }
        public File File { get; set; }

    }
}
