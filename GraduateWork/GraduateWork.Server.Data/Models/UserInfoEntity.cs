using GraduateWork.Common.CommonProject;
using GraduateWork.Server.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GraduateWork.Server.Data.Models
{
    public class UserInfoEntity:IdentityUser
    {        
        public DateTimeOffset DateOfBirth { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public string Data { get; set; }
        public List<UserInfoGroupChatEntity> UserInfo_GroupChatId { get; set; }
    }
}
