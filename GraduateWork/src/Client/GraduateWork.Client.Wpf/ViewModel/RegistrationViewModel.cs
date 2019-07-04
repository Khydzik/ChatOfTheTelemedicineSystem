using GraduateWork.Client.Wpf.Managers;
using GraduateWork.Client.Wpf.Validation;
using GraduateWork.Client.Wpf.View;
using GraduateWork.Common.CommonProject;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GraduateWork.Client.Wpf.ViewModel
{
    public class RegistrationViewModel: BindableBase
    {
        public DelegateCommand<object> RegistrationButtonCommand { get; set; }
        public DelegateCommand<Window> BackCommandButton { get; set; }

        private string _username;
        private string _firstname;
        private string _lastname;
        private string _phonenumber;
        private Role _role;
        private DateTime _dataofbirth;
        private string _email;
        private Validator _validator;
        private readonly ConnectionManager _registrationManager;
        private string _errorLabel;
        private bool _isRegisterButtonEnable;
        private string _passwordBackGroundCollor;
        private string _confirmPasswordBackGroundCollor;

        public bool IsRegisterButtonEnable
        {
            get { return _isRegisterButtonEnable; }
            set { SetProperty(ref _isRegisterButtonEnable, value); }
        }

        public string PasswordBackGroundCollor
        {
            get { return _passwordBackGroundCollor; }
            set { SetProperty(ref _passwordBackGroundCollor, value); }
        }

        public string ConfirmPasswordBackGroundCollor
        {
            get { return _confirmPasswordBackGroundCollor; }
            set { SetProperty(ref _confirmPasswordBackGroundCollor, value); }
        }

        public string ErrorLabel
        {
            get { return _errorLabel; }
            set { SetProperty(ref _errorLabel, value); }
        }

        public DateTime DataOfBirth
        {
            get { return this._dataofbirth; }
            set { SetProperty(ref _dataofbirth, value);
            }
        }

        public Role Role
        {
            get { return this._role; }
            set { SetProperty(ref _role, value); }
        }
        public string UserName
        {
            get { return this._username; }
            set { SetProperty(ref _username, value); }
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

        public RegistrationViewModel()
        {
            RegistrationButtonCommand = new DelegateCommand<object>(RegistrationButton);
            BackCommandButton = new DelegateCommand<Window>(BackButton);
            _registrationManager = new ConnectionManager();
            _validator = new Validator();

            PasswordBackGroundCollor = "White";
            ConfirmPasswordBackGroundCollor = "White";
            IsRegisterButtonEnable = true;
        }

        private async void RegistrationButton(object obj)
        {
            var boxes = (object[])obj;
            var passwordBox = ((PasswordBox)boxes[0]);
            var passwordBoxConfirmation = (PasswordBox)boxes[1];                 

            if (!_validator.ValidatorEmail(Email))
            {
                ErrorLabel = " Enter e-mail... Incorrect e-mail...";
                Email = "";
                IsRegisterButtonEnable = true;
                return;
            }

            if (!_validator.PasswordValidator(passwordBox.Password))
            {
                ErrorLabel = "Enter password... Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)...";
                passwordBox.Password = "";
                passwordBoxConfirmation.Password = "";
                IsRegisterButtonEnable = true;
                return;
            }

            if (!_validator.ConfirmPassword(passwordBoxConfirmation.Password, passwordBox.Password))
            {
                ErrorLabel = "Incorrectly entered password...";
                PasswordBackGroundCollor = "Red";
                ConfirmPasswordBackGroundCollor = "Red";
                IsRegisterButtonEnable = true;
                return;
            }

            IsRegisterButtonEnable = false;

            var newUser = new RegistrationUserModel
            {
                UserName = UserName,
                Email = Email,
                Password = passwordBox.Password,
                PhoneNumber = PhoneNumber,
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = new DateTimeOffset(DataOfBirth),
                UserRole = Role
            };

           
            
            var result = await _registrationManager.RegisterUser(newUser);

            if(result.IsSuccessStatusCode)
            {
                MessageBox.Show("You are successfully registered...", "Congratulations!", MessageBoxButton.OK,
                       MessageBoxImage.Information);
                var loginPage = new Authorization();
                loginPage.Show();
            }
            else
            {                
                ErrorLabel = "The login or email address you have already entered is already in use.";
                Email = "";
                passwordBox.Password = "";
                IsRegisterButtonEnable = true;
                return;
            }

        }

        private void BackButton(Window obj)
        {
            var window = new Authorization();
            window.Show();
            obj.Close();
        }
    }
}
