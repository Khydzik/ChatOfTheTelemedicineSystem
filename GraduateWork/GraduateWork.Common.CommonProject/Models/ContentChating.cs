using System;
using System.Collections.Generic;
using System.Text;

namespace GraduateWork.Common.CommonProject.Models
{
    public class ContentChating
    {
        public Guid UserId { get; set; }
        public Guid UserIdToWrite { get; set; }
        public string ContentMessages { get; set; }
    }
}
