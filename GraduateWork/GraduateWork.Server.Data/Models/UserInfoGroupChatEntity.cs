using System;

namespace GraduateWork.Server.Data.Models
{
    public class UserInfoGroupChatEntity
    {
        public Guid Id { get; set; }
        public Guid UserInfoId { get; set; }
        public Guid GroupChatId { get; set; }
        public bool IsAdmin { get; set; }

        public UserInfoEntity UserInfo { get; set; }
        public GroupChatEntity GroupChat { get; set; }
    }
}
