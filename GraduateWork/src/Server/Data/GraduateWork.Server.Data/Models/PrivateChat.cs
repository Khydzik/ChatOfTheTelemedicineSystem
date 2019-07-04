using System;

namespace GraduateWork.Server.Data.Models
{
    public class PrivateChat
    {
        public Guid ChatId { get; set; }
        public Guid FirstUserInfoId { get; set; }
        public Guid SecondUserInfoId { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
