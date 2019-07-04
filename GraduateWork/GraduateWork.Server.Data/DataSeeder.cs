using GraduateWork.Common.CommonProject;
using GraduateWork.Server.Common;
using GraduateWork.Server.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace GraduateWork.Server.Data
{
    public class DataSeeder
    {
        private readonly UserManager<UserInfoEntity> _userManager;
        private readonly RoleManager<IdentityRole>  _rolesManager;
        public DataSeeder(UserManager<UserInfoEntity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _rolesManager = roleManager;
        }
        public async Task Seed()
        {
            var user = new UserInfoEntity
            {
                UserName = "Volodya1111",
                Email = "parta142536@i.ua",
                PhoneNumber = "0672530997",
                FirstName = "Volodya",
                LastName = "Khydzik",
                DateOfBirth = new DateTimeOffset(2008, 5, 1, 8, 6, 32, new TimeSpan(1, 0, 0)),
                Role = Role.Doctor,
                PasswordHash = "volodya1111"
            };
            
            var roles = Enum.GetValues(typeof(Role));

            foreach (var role in roles)
            {
                string roleStr = role.ToString();

                if(await _rolesManager.FindByNameAsync(roleStr) == null)
                {
                    await _rolesManager.CreateAsync(new IdentityRole(roleStr));
                }
            }

            if (await _userManager.FindByNameAsync(user.UserName).ConfigureAwait(false) == null)
            {
                var result = await _userManager.CreateAsync(user);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Role.Doctor.ToString());
                }
            }
        }
    }
}
