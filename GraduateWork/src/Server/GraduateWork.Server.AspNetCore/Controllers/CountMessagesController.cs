using GraduateWork.Common.CommonProject.Models;
using GraduateWork.Server.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduateWork.Server.AspNetCore.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class CountMessagesController:ControllerBase
    {
        private readonly IUserService _userService;

        public CountMessagesController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<CountMessageForPersonalPage> CountMessagesAsync([FromBody] CountMessageForPersonalPage input)
        {
            return await _userService.CountMessages(input.UserId);
        }
    }
}
