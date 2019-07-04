using System;
using System.ComponentModel.DataAnnotations;

namespace GraduateWork.Common.CommonProject
{
    public class RegistrationUserModel
    {
        [Required(ErrorMessage = "Required", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "You can enter only letters and numbers")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required", AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required", AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Required")]
        public Role UserRole { get; set; }

        [EmailAddress(ErrorMessage ="Uncorrect address")]
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Required")]
        public DateTimeOffset DateOfBirth { get; set; }

        [Required(ErrorMessage = "Required", AllowEmptyStrings = false)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
