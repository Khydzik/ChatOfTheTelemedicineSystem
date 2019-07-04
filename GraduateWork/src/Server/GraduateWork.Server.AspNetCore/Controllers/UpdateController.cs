using System.Threading.Tasks;
using GraduateWork.Common.CommonProject.Models;
using GraduateWork.Server.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduateWork.Server.AspNetCore.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiVersion("1")]
    public class UpdateController : ControllerBase
    {
        private readonly IUserService _userService;

        public UpdateController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]UsersInfoModel input)
        {
            await _userService.UpdateDateOfHistory(input.Id, input.Date);
            return Ok();
        }
    }
}