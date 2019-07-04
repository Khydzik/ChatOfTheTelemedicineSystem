using System.Threading.Tasks;
using GraduateWork.Common.CommonProject;
using GraduateWork.Server.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GraduateWork.Server.AspNetCore.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationAsync([FromBody]RegistrationUserModel input)
        {
            await _userService.CreateUserAsync(input);
            return Ok();
        }
    }
}