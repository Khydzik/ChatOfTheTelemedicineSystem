using GraduateWork.Server.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduateWork.Server.Data.Models
{
    public class ApplicationContext : IdentityDbContext<UserInfo>
    {
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<PrivateChat> PrivateChats { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<UserInfo_GroupChat> UserInfo_GroupChats { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
