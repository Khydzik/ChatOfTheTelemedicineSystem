using GraduateWork.Client.Wpf.Managers;
using GraduateWork.Client.Wpf.View;
using GraduateWork.Common.CommonProject.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduateWork.Client.Wpf.ViewModel
{
    public class DoctorPageFromListBoxViewModel : BindableBase
    {
        private DateTime _dateofbirth;
        private string _email;
        private string _firstname;
        private string _lastname;
        private string _phonenumber;
        private string _doctorId;
        private string UserId { get; set; }
        private readonly ConnectionManager _connectionManager;
        public DelegateCommand WriteMessage { get; set; }

        public DateTime DateOfBirthDoctor
        {
            get { return this._dateofbirth; }
            set
            {
                SetProperty(ref _dateofbirth, value);
            }
        }

        public string DoctorId
        {
            get { return this._doctorId; }
            set { SetProperty(ref _doctorId, value); }
        }

        public string FirstName
        {
            get { return this._firstname; }
            set { SetProperty(ref _firstname, value); }
        }

        public string LastName
        {
            get { return this._lastname; }
            set { SetProperty(ref _lastname, value); }
        }

        public string PhoneNumber
        {
            get { return this._phonenumber; }
            set { SetProperty(ref _phonenumber, value); }
        }

        public string Email
        {
            get { return this._email; }
            set { SetProperty(ref _email, value); }
        }

        public DoctorPageFromListBoxViewModel(UsersInfoModel doctorModel, string userId)
        {
            _connectionManager = new ConnectionManager();
            FirstName = doctorModel.FirstName;
            LastName = doctorModel.LastName;
            DateOfBirthDoctor = doctorModel.DateOfBirth.UtcDateTime;
            PhoneNumber = doctorModel.PhoneNumber;
            Email = doctorModel.Email;
            DoctorId = doctorModel.Id;
            UserId = userId;
            WriteMessage = new DelegateCommand(WriteMessageInChat);
        }

        private async void WriteMessageInChat()
        {

            var saveChats = await _connectionManager.SavedChats(UserId);
            var getUserToWrite = await _connectionManager.GetUserInfoAsync(new UsersInfoModel { Id = DoctorId });

            if (saveChats.SavedPrivateChats == null)
                saveChats.SavedPrivateChats = new List<PrivateChat>();

            saveChats.SavedPrivateChats.Add(
                new PrivateChat()
                {
                    Id = Guid.NewGuid(),
                    Name = getUserToWrite.FullName,
                    Messages = new ObservableCollection<ChatMessage>(),
                    UserIdToWrite = Guid.Parse(DoctorId),
                    OwnUserId = Guid.Parse(UserId)
                });

            var chatPage = new ChatPage(saveChats, getUserToWrite); 
            chatPage.Show();
        }
    }
}
