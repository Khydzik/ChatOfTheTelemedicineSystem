using System;
using System.Collections.Generic;
using System.Text;

namespace GraduateWork.Server.Data.Models
{
    public class PrivateMessage
    {
        public Guid Id { get; set; }
        public Guid PrivatChatId { get; set; }
        public Type Type { get; set; }
        public string Content { get; set; }
        public Guid FileId { get; set; }

        public PrivateChat PrivateChat { get; set; }
        public File File { get; set; }
    }
}
