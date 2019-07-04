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
using System.Windows;

namespace GraduateWork.Client.Wpf.ViewModel
{
    public class PersonalPageDoctorViewModel : BindableBase
    {
        private DateTime _dateofbirth;
        private string _email;
        private string _firstname;
        private string _lastname;
        private string _phonenumber;
        private string UserId { get; set; }
        private string ConnectedID { get; set; }
        private string _dateHistory;
        private string _count;
        public DelegateCommand<InfoUserModel> SelectDoctorCommand { get; }
        public DelegateCommand<InfoUserModel> SelectPatientCommand { get; }
        public DelegateCommand ButtonMessageCommand { get; }
        private ObservableCollection<InfoUserModel> _patientsObservableCollection;
        private ObservableCollection<InfoUserModel> _doctorsObservableCollection;
        private readonly ConnectionManager _connectionManager;

        public string CountActiveMessage
        {
            get { return this._count; }
            set { SetProperty(ref _count, value); }
        }

        public ObservableCollection<InfoUserModel> DoctorsList
        {
            get { return _doctorsObservableCollection; }
            set { SetProperty(ref _doctorsObservableCollection, value); }
        }

        public ObservableCollection<InfoUserModel> PatientsList
        {
            get { return _patientsObservableCollection; }
            set { SetProperty(ref _patientsObservableCollection, value); }
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

        public PersonalPageDoctorViewModel(UsersInfoModel doctorInfoModel, CountMessageForPersonalPage countMessageForPersonalPage, Connected connected)
        {
            ConnectedID = connected.ConnectedID;
            _connectionManager = new ConnectionManager();
            UserId = doctorInfoModel.Id;
            FirstName = doctorInfoModel.FirstName;
            LastName = doctorInfoModel.LastName;
            DateOfBirthPatient = doctorInfoModel.DateOfBirth.UtcDateTime;
            PhoneNumber = doctorInfoModel.PhoneNumber;
            Email = doctorInfoModel.Email;
            SelectDoctorCommand = new DelegateCommand<InfoUserModel>(SelectDoctor);
            SelectPatientCommand = new DelegateCommand<InfoUserModel>(SelectPatient);
            DoctorsList = new ObservableCollection<InfoUserModel>(doctorInfoModel.DortorsList);
            PatientsList = new ObservableCollection<InfoUserModel>(doctorInfoModel.PatientsList);
            ButtonMessageCommand = new DelegateCommand(WriteMessageInChat);
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

        private async void SelectDoctor(InfoUserModel doctor)
        {
            try
            {
                var infoDoctorPersonalPage = await _connectionManager.GetUserInfoAsync(new UsersInfoModel
                {
                    Id = doctor.Id
                });

                var personalPage = new PageDoctorFromListBox(infoDoctorPersonalPage, UserId);
                personalPage.Show();
            }
            catch(Exception)
            { return; }
        }

        private async void SelectPatient(InfoUserModel patient)
        {
            try
            {
                var infoDoctorPersonalPage = await _connectionManager.GetUserInfoAsync(new UsersInfoModel
                {
                    Id = patient.Id
                });

                var personalPage = new PagePatientFromListBox(infoDoctorPersonalPage, UserId);
                personalPage.Show();
            }
            catch (Exception) {
                return;
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
            catch(Exception)
            { return; }
        }

        private void ConfigurationButtonButton(Window obj)
        {
            //var window = new Authorization();
            //window.Show();
            //obj.Close();
        }
    }
}

