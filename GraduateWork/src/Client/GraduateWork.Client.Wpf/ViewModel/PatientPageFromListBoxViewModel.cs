using GraduateWork.Client.Wpf.Managers;
using GraduateWork.Client.Wpf.View;
using GraduateWork.Common.CommonProject.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace GraduateWork.Client.Wpf.ViewModel
{
    public class PatientPageFromListBoxViewModel : BindableBase
    {
        private DateTime _dateofbirth;
        private string _email;
        private string _date;
        private string _firstname;
        private string _lastname;
        private string _phonenumber;
        private bool _isTextBoxEnable;
        private bool _isAddButtonEnable;
        private string PatientIdToWrite { get; set; }
        private string UserId { get; set; }
        public DelegateCommand SaveButtonCommand { get; }
        public DelegateCommand WriteMessage { get; set; }
        private readonly ConnectionManager _connectionManager;

        public bool IsAddButtonEnable
        {
            get { return _isAddButtonEnable; }
            set { SetProperty(ref _isAddButtonEnable, value); }
        }

        public bool IsTextBoxEnable
        {
            get { return _isTextBoxEnable; }
            set
            {
                SetProperty(ref _isTextBoxEnable, value);
            }
        }
        
        public DateTime DateOfBirthPatient
        {
            get { return this._dateofbirth; }
            set
            {
                SetProperty(ref _dateofbirth, value);
            }
        }

        public string Date
        {
            get { return this._date; }
            set { SetProperty(ref _date, value); }
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
        

        public PatientPageFromListBoxViewModel(UsersInfoModel patientModel, string userId)
        {
            _connectionManager = new ConnectionManager();
            FirstName = patientModel.FirstName;
            LastName = patientModel.LastName;
            DateOfBirthPatient = patientModel.DateOfBirth.UtcDateTime;
            PhoneNumber = patientModel.PhoneNumber;
            Email = patientModel.Email;
            Date = patientModel.Date;
            UserId = userId;
            PatientIdToWrite = patientModel.Id;
            IsTextBoxEnable = true;
            IsAddButtonEnable = true;
            WriteMessage = new DelegateCommand(WriteMessageInChat);
            SaveButtonCommand = new DelegateCommand(AddToHistoryOfTheDisease);
        }

        private async void WriteMessageInChat()
        {
            var saveChats = await _connectionManager.SavedChats(UserId);
            var getUserToWrite = await _connectionManager.GetUserInfoAsync(new UsersInfoModel { Id = PatientIdToWrite });

            if (saveChats.SavedPrivateChats == null)
                saveChats.SavedPrivateChats = new List<PrivateChat>();

            saveChats.SavedPrivateChats.Add(
                new PrivateChat()
                {
                    Id = Guid.NewGuid(),
                    Name = getUserToWrite.FullName,
                    Messages = new ObservableCollection<ChatMessage>(),
                    UserIdToWrite = Guid.Parse(PatientIdToWrite),
                    OwnUserId = Guid.Parse(UserId)
                });

            var chatPage = new ChatPage(saveChats, getUserToWrite);
            chatPage.Show();
        }   

        private async void AddToHistoryOfTheDisease()  
        {
            IsAddButtonEnable = false;
            
            var result = await _connectionManager.UpdateUser(new UsersInfoModel
            {
                Id = PatientIdToWrite,
                Date = Date
            });

            if (result.IsSuccessStatusCode)
            {
                JsonConvert.DeserializeObject<UsersInfoModel>(await result.Content.ReadAsStringAsync());
                MessageBox.Show("Dates are successfully added...", "Congratulations!", MessageBoxButton.OK,
                      MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Dates aren`t successfully added... Try again", "Bad Request!", MessageBoxButton.OK,
                     MessageBoxImage.Information);
                return;
            }

            IsAddButtonEnable = true;
        }
    }
}


