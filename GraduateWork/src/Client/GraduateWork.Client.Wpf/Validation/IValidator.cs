using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduateWork.Client.Wpf.Validation
{
    interface IValidator
    {
        bool ValidatorEmail(string email);
        bool ConfirmPassword(string confirmPassword, string password);
        bool PasswordValidator(string password);
    }
}
