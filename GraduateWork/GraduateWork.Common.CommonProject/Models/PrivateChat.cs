using System;
using System.Collections.ObjectModel;

namespace GraduateWork.Common.CommonProject.Models
{
    public class PrivateChat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserIdToWrite { get; set; }
        public Guid OwnUserId { get; set; }
        public ObservableCollection<ChatMessage> Messages { get; set; }
    }
}
