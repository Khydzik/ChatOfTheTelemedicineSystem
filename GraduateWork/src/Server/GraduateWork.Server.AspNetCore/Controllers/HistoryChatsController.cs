using GraduateWork.Common.CommonProject.Models;
using GraduateWork.Server.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GraduateWork.Server.AspNetCore.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class HistoryChatsController:ControllerBase
    {
        private readonly IUserService _userService;

        public HistoryChatsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<SavedChats> HistoryChatsAsync([FromBody] SavedChats input)
        {
            return await _userService.GetHistoryChatsAsync(input.UserId);
        }
    }
}
