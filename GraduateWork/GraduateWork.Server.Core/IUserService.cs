using GraduateWork.Common.CommonProject;
using GraduateWork.Common.CommonProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraduateWork.Server.Core
{
    public interface IUserService
    {
        Task<AuthorizationResponse> GetUserAsync(string username, string password);
        Task CreateUserAsync(RegistrationUserModel input);
        Task<UsersInfoModel> GetUserByIdAsync(string id);
        Task UpdateDateOfHistory(string id, string date);
        Task<CountMessageForPersonalPage> CountMessages(Guid id);
        Task<SavedChats> GetHistoryChatsAsync(Guid UserId);
    }
}
