using System;
using System.Windows;
using System.Windows.Controls;
using GraduateWork.Client.Wpf.FileAuthorize;
using GraduateWork.Client.Wpf.Managers;
using GraduateWork.Client.Wpf.Validation;
using GraduateWork.Client.Wpf.View;
using GraduateWork.Common.CommonProject;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm; 

namespace GraduateWork.Client.Wpf.ViewModel
{
    class AuthorizationViewModel : BindableBase
    {        
        private ConnectionManager _connectionManager;
        private OperationFile _operationFile;
        private string _login;
        private bool _isEnableLoginButton;
        private string _errorLabel;
        private Validator _validator;

        public DelegateCommand<Window> RegistrationCommandButton { get; set; }
        public DelegateCommand<Object> AutorizationCommandButton { get; set; }

        public AuthorizationViewModel()
        {
            RegistrationCommandButton = new DelegateCommand<Window>(RegistrationButton);
            AutorizationCommandButton = new DelegateCommand<Object>(AutorizationButton);
            _connectionManager = ConnectionManager.GetInstance();
            _operationFile = OperationFile.GetInstance();
            _validator = new Validator(); 
            IsEnableLoginButton = true;
        }

        public string Login
        {
            get { return this._login; }
            set { SetProperty(ref _login, value); }
        }

        public string ErrorLabel
        {
            get { return this._errorLabel; }
            set { SetProperty(ref _errorLabel, value); }
        }

        public bool IsEnableLoginButton
        {
            get { return _isEnableLoginButton; }
            set { SetProperty(ref _isEnableLoginButton, value); }
        }

        private async void AutorizationButton(Object obj)
        {
            IsEnableLoginButton = false;

            var passwordBox = obj as PasswordBox;
            var _password = passwordBox.Password;

            if(Login == null)
            {
                ErrorLabel = "Enter login...";
                IsEnableLoginButton = true;
                return;
            }
            
            if (!_validator.PasswordValidator(_password))
            {
                ErrorLabel = "Enter password... Passwords must be at least 8 characters and contain at 3 " +
                    "of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character " +
                    "(e.g. !@#$%^&*)...";
                IsEnableLoginButton = true;
                _password = "";
                return;
            }
            
            var userLogin = new UserLoginModel
            {
                Login = Login,
                Password = _password
            };

            var responce = await _connectionManager.Login(userLogin);
            
            if(responce.IsSuccessStatusCode)
            {
                var result = await responce.Content.ReadAsStringAsync();
                var authorizationResponce = JsonConvert.DeserializeObject<AuthorizationResponse>(result);
                await _operationFile.WriteTokenToFileAsync(authorizationResponce.Token);
                var connectionID = await _connectionManager.GetConnectionId();

                var infoPersonalPage = await _connectionManager.GetUserInfoAsync(new Common.CommonProject.Models.UsersInfoModel
                {
                    Id = authorizationResponce.Id
                });

                var countMessage = await _connectionManager.CountMessages(infoPersonalPage.Id);

                if (authorizationResponce.Role == Role.Doctor.ToString())
                {                  
                    var personalDoctorPage = new PersonalPageDoctor(infoPersonalPage, countMessage, connectionID);
                    personalDoctorPage.Show();
                }
                else
                {
                    var personalPatientPage = new PersonalPagePatient(infoPersonalPage, countMessage, connectionID);                    
                    personalPatientPage.Show();                    
                }
            }
            else
            {
                MessageBox.Show("Не вірний логін або пароль", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                Login = "";
                _password = "";
                IsEnableLoginButton = true;
            }
        }

        private void RegistrationButton(Window obj)
        {
            var register = new RegistrationView();
            register.Show();
            obj.Close();
        }

    }
}
