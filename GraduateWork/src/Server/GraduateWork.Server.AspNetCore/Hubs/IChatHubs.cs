using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduateWork.Server.AspNetCore.Hubs
{
    interface IChatHubs
    {
        void SendChatMessage(string who, string message);  
    }
}
