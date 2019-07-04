using System;
using System.Collections.Generic;

namespace GraduateWork.Server.Data.Models
{
    public class GroupChatEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }
        public List<UserInfoGroupChatEntity> UserInfoGroupChats { get; set; }
    }
}
