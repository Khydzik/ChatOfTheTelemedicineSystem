using System;

namespace GraduateWork.Common.CommonProject.Models
{
    public class ChatMessage
    {
        public Guid ChatID { get; set; }
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
