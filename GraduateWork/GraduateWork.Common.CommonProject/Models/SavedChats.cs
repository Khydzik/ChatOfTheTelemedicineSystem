using System;
using System.Collections.Generic;
using System.Text;

namespace GraduateWork.Common.CommonProject.Models
{
    public class SavedChats
    {
        public Guid UserId { get; set; }
        public List<PrivateChat> SavedPrivateChats { get; set; }
    }
}
