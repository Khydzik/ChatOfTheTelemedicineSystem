using System;
using System.Collections.Generic;

namespace GraduateWork.Common.CommonProject.Models
{
    public class UsersInfoModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Date { get; set; }
        public List<InfoUserModel> PatientsList { get; set; }
        public List<InfoUserModel> DortorsList { get; set; }
    }
}
