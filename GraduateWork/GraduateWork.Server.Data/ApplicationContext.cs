using GraduateWork.Server.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraduateWork.Server.Data
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {}

        public DbSet<UserInfoEntity> UserInfos { get; set; }
        public DbSet<PrivateChatEntity> PrivateChats { get; set; }
        public DbSet<PrivateMessageEntity> PrivateMessages { get; set; }
        public DbSet<GroupChatEntity> GroupChats { get; set; }
        public DbSet<GroupMessageEntity> GroupMessages { get; set; }
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<UserInfoGroupChatEntity> UserInfo_GroupChats { get; set; }


    }
}
