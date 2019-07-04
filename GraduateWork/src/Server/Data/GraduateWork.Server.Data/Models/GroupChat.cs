using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduateWork.Server.Data.Models
{
    public class GroupChat
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid[] UserIds { get; set; }

        public UserInfo UserInfo { get; set; }

    }
}
