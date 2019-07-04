using GraduateWork.Common.CommonProject.Models;
using GraduateWork.Server.AspNetCore.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace GraduateWork.Server.AspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class ConnectedController:ControllerBase
    {
        private static ChatHub _chatHub;
        private readonly IHubContext<ChatHub> _hubContext;

        public ConnectedController(IHubContext<ChatHub> hubContext) 
        {
            _chatHub = ChatHub.GetInstance();
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<Connected> GetConnectionAsync()
        {
            var connectionId = HttpContext.Connection.Id;
            var userId = HttpContext.User.Claims.FirstOrDefault(r => r.Type == "user_id");

            return await _chatHub.OnConnectedAsync(connectionId, userId.Value);
        }
    }
}
 