using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GraduateWork.Server.Data.Models
{
    public enum Role
    {
        Doctor,
        Patient
    }

    public class UserInfo:IdentityUser
    {
        public override string Id { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public string Data { get; set; }
        public List<Guid> UserInfo_GroupChatId { get; set; }
    }
}
