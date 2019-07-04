using System;
using System.Collections.Generic;
using System.Text;

namespace GraduateWork.Common.CommonProject.Models
{
    public class PatientPageFromListBoxModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Date { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
    }
}
