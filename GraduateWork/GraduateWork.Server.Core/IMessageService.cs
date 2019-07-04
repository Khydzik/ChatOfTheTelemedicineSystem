using GraduateWork.Common.CommonProject.Models;
using System.Threading.Tasks;

namespace GraduateWork.Server.Core
{
    public interface IMessageService
    {
        Task AddMessageAsync(ChatMessage chatMessage);
    }
}
