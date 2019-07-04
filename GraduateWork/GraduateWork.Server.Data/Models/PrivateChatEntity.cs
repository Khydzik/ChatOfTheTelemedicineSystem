using System;

namespace GraduateWork.Server.Data.Models
{
    public class PrivateChatEntity
    {
        public Guid Id { get; set; }
        public Guid FirstUserInfoId { get; set; }
        public Guid SecondUserInfoId { get; set; }
        public bool IsActive { get; set; }

        public UserInfoEntity FirstUserInfo { get; set; }
        public UserInfoEntity SecondUserInfo { get; set; }
    }
}
