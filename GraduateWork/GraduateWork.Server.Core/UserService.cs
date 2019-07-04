using GraduateWork.Common.CommonProject;
using GraduateWork.Common.CommonProject.Models;
using GraduateWork.Server.Data;
using GraduateWork.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduateWork.Server.Core
{
    public class UserService:IUserService
    {
        public string PatientRole = "Patient";
        public string DoctorRole = "Doctor";
        private readonly IRepository<UserInfoEntity> _userRepository;
        private readonly IRepository<PrivateChatEntity> _chatRepository;

        public UserService(IRepository<UserInfoEntity> userRepository, IRepository<PrivateChatEntity> privateChatRepository)
        {
            _chatRepository = privateChatRepository;
            _userRepository = userRepository;
        }

        public async Task<AuthorizationResponse> GetUserAsync(string username, string password)
        {
            var user = await _userRepository.Query().FirstOrDefaultAsync(r => r.UserName == username && r.PasswordHash == password);

            if (user == null)
            {
                throw new ArgumentException("Invalid username or password");
            }

            var response = new AuthorizationResponse
            {
                UserName = user.UserName,
                Id = user.Id,
                Role = user.Role.ToString()
            };

            return response;
        }

        public async Task CreateUserAsync(RegistrationUserModel registrationUserModel)
        {
            var user = await _userRepository.GetAsync(u => u.UserName == registrationUserModel.UserName);

            if (user != null)
                throw new Exception("Such user exist!");
            
            var newUser = new UserInfoEntity
            {
                UserName = registrationUserModel.UserName,
                FirstName = registrationUserModel.FirstName,
                LastName = registrationUserModel.LastName,
                Email = registrationUserModel.Email,
                DateOfBirth = registrationUserModel.DateOfBirth,
                PhoneNumber = registrationUserModel.PhoneNumber,
                PasswordHash = registrationUserModel.Password,
                Role = registrationUserModel.UserRole
            }; 

            await _userRepository.InsertAsync(newUser);
        }

        public async Task<List<InfoUserModel>> GetAllByRole(string role, string userId)
        {
            List<InfoUserModel> userInfoEntities = new List<InfoUserModel>();
            var users = await _userRepository.Query().ToListAsync();
            foreach(var user in users)
            {
                if (user.Role.ToString() == role && user.Id != userId)
                {
                    userInfoEntities.Add(new InfoUserModel { Id = user.Id, Name = user.FirstName + " " + user.LastName });
                }
            }
            return userInfoEntities;
        }

        public async Task<UsersInfoModel> GetUserByIdAsync(string id)
        {
            List<InfoUserModel> allPatients = null;
            List<InfoUserModel> allDoctors = null;

            var user = await _userRepository.GetAsync(r => r.Id == id);

            if (user == null)
                throw new ArgumentException("Not Found...");

            if (user.Role.ToString() == PatientRole)
            {
                allDoctors = await GetAllByRole(DoctorRole, user.Id);
            }
            else
            {
                allDoctors = await GetAllByRole(DoctorRole, user.Id);
                allPatients = await GetAllByRole(PatientRole, user.Id);
            }

            return new UsersInfoModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Date = user.Data,
                PatientsList = allPatients,
                DortorsList = allDoctors,
                FullName = user.FirstName + " " + user.LastName
            };
        }

        public async Task UpdateDateOfHistory(string id, string date)
        {
            var user = await _userRepository.Query().FirstOrDefaultAsync(r => r.Id == id);

            if (user == null)
                throw new ArgumentException("Not Found");

            user.Data = date;

            await _userRepository.UpdateAsync(user); 
        }

        public async Task<CountMessageForPersonalPage> CountMessages(Guid id)
        {
            var messages = await _chatRepository.Query().ToListAsync();
            var count = (from t in messages where t.FirstUserInfoId == id  where t.IsActive == true orderby t select t).Count();
            return new CountMessageForPersonalPage { CountMessages = count };
        }

        public async Task<SavedChats> GetHistoryChatsAsync(Guid userId)
        {
            var firstUserChats = await _chatRepository.Query().Where(r => r.FirstUserInfoId == userId).ToListAsync();
            var secondUserChats = await _chatRepository.Query().Where(r => r.SecondUserInfoId == userId).ToListAsync();
          
            var chats = new SavedChats { UserId = userId };

            foreach(var value in firstUserChats)
            {
                chats.SavedPrivateChats.Add(new PrivateChat
                {
                    Id = value.Id,
                    Name = value.SecondUserInfo.LastName + " " + value.SecondUserInfo.FirstName,
                    OwnUserId = new Guid(value.FirstUserInfo.Id),
                    UserIdToWrite = new Guid(value.SecondUserInfo.Id)
                });
            }

            foreach (var value in secondUserChats)
            {
                chats.SavedPrivateChats.Add(new PrivateChat
                {
                    Id = value.Id,
                    Name = value.FirstUserInfo.LastName + " " + value.FirstUserInfo.FirstName,
                    UserIdToWrite = new Guid(value.FirstUserInfo.Id),
                    OwnUserId = new Guid(value.SecondUserInfo.Id)                    
                });
            }
            
            return chats;
        }

        
    }
}
