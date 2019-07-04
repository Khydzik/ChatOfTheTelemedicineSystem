using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduateWork.Server.Data.Models
{
    public class UserInfo_GroupChat
    {
        public Guid Id { get; set; }
        public Guid UserInfoId { get; set; }
        public Guid GroupChatId { get; set; }
        public bool IsAdmin { get; set; }

        public UserInfo UserInfo { get; set; }
        public GroupChat GroupChat { get; set; }
    }
}
