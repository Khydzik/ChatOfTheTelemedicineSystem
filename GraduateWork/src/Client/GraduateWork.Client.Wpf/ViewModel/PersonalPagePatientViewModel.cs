using GraduateWork.Client.Wpf.Managers;
using GraduateWork.Client.Wpf.View;
using GraduateWork.Common.CommonProject.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace GraduateWork.Client.Wpf.ViewModel
{
    public class PersonalPagePatientViewModel : BindableBase
    {
        public DelegateCommand ButtonMessageCommand { get; set; }
        private DateTime _dateofbirth;
        private string _count;
        private string _email;
        private string _firstname;
        private string UserId { get; set; }
        private string ConnectedID { get; set; }
        private string _lastname;
        private string _phonenumber;
        private readonly ConnectionManager _connectionManager;
        public DelegateCommand<InfoUserModel> SelectDoctorCommand { get; set; }
        private bool _isTextBoxEnable;
        private string _dateHistory;
        private ObservableCollection<InfoUserModel> _observableCollection;

        public bool IsTextBoxEnable
        {
            get { return _isTextBoxEnable; }
            set { SetProperty(ref _isTextBoxEnable, value); }
        }

        public ObservableCollection<InfoUserModel> DoctorsList
        {
            get { return _observableCollection; }
            set { SetProperty(ref _observableCollection, value); }
        }

        public string DateHistory
        {
            get { return _dateHistory; }
            set { SetProperty(ref _dateHistory, value); }
        }

        public DateTime DateOfBirthPatient
        {
            get { return this._dateofbirth; }
            set
            {
                SetProperty(ref _dateofbirth, value);
            }
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

        public string CountActiveMessage
        {
            get { return this._count; }
            set { SetProperty(ref _count, value); }
        }

        public PersonalPagePatientViewModel(UsersInfoModel patientInfoModel, CountMessageForPersonalPage countMessageForPersonalPage, Connected connected)
        {
            ConnectedID = connected.ConnectedID;
            _connectionManager = new ConnectionManager();
            FirstName = patientInfoModel.FirstName;
            LastName = patientInfoModel.LastName;
            DateOfBirthPatient = patientInfoModel.DateOfBirth.UtcDateTime;
            PhoneNumber = patientInfoModel.PhoneNumber;
            Email = patientInfoModel.Email;
            IsTextBoxEnable = false;
            DateHistory = patientInfoModel.Date;
            DoctorsList = new ObservableCollection<InfoUserModel>(patientInfoModel.DortorsList);
            SelectDoctorCommand = new DelegateCommand<InfoUserModel>(SelectDoctor);
            ButtonMessageCommand = new DelegateCommand(WriteMessageInChat);
            UserId = patientInfoModel.Id;
            ActiveMessage(countMessageForPersonalPage.CountMessages);
        }

        private void ActiveMessage(int activeMessageNumber)
        {
            var activeNumber = Convert.ToString(activeMessageNumber);

            if (activeNumber == "0")
                CountActiveMessage = "";
            else
            {
                CountActiveMessage = "+" + activeNumber;
            }
        }

        private async void WriteMessageInChat()
        {
            try
            {
                var saveChats = await _connectionManager.SavedChats(UserId);
                var chatPage = new ChatPage(saveChats, null);
                chatPage.Show();
            }
            catch (Exception)
            { return; }
        }

        private async void SelectDoctor(InfoUserModel doctor)
        {
            var infoDoctorPersonalPage = await _connectionManager.GetUserInfoAsync(new UsersInfoModel
            {
                Id = doctor.Id
            });

            var personalPage = new PageDoctorFromListBox(infoDoctorPersonalPage, UserId);
            personalPage.Show();
        }      
    }
}
