using GraduateWork.Client.Wpf.ViewModel;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GraduateWork.Client.Wpf.Validation
{
    public class Validator : IValidator
    {
        private AuthorizationViewModel _authorizationViewModel {get;set;} 

        public bool PasswordValidator(string password)
        {
            try
            {
                string expression = "^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|" +
                    "(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$";

                if (password.Length == 0)
                {
                    return false;
                }

                if (Regex.IsMatch(password, expression))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool ConfirmPassword(string confirmPassword, string password)
        {
            if (confirmPassword.Length == 0)
            {
               _authorizationViewModel.ErrorLabel = "Enter reapet password...";
                return false;
            }

            if (password != confirmPassword)
            {               
                return false;
            }
            return true;
        }

        public bool ValidatorEmail(string email)
        {
            try
            {
                if (email.Length == 0)
                {
                    _authorizationViewModel.ErrorLabel = "Enter e-mail...";
                    return false;
                }

                var address = new MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
