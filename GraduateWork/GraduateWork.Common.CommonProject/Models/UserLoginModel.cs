using System.ComponentModel.DataAnnotations;

namespace GraduateWork.Common.CommonProject
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Required", AllowEmptyStrings = false)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
