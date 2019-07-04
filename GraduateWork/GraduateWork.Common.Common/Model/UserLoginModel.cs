using System.ComponentModel.DataAnnotations;

namespace GraduateWork.Common.Common.Models
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
