using System;
using System.Threading.Tasks;
using GraduateWork.Common.CommonProject;
using GraduateWork.Server.AspNetCore.Token;
using GraduateWork.Server.Core;
using Microsoft.AspNetCore.Mvc;

namespace GraduateWork.Server.AspNetCore.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenFormation _tokenFormation;

        public AuthorizationController(IUserService userService, ITokenFormation tokenFormation)
        {
            _tokenFormation = tokenFormation;
            _userService = userService;
        }

        [HttpPost]
        public async Task<AuthorizationResponse> AuthorizationAsync([FromBody] UserLoginModel input)
        {
           var authorizationResponse = await _userService.GetUserAsync(input.Login, input.Password);

            if (authorizationResponse == null)
            {
                throw new ArgumentException("Invalid username or password");
            }

            var jwt = _tokenFormation.GetToken(authorizationResponse);
            
            return new AuthorizationResponse
            {
                Id = authorizationResponse.Id,
                UserName = authorizationResponse.UserName,
                Role = authorizationResponse.Role,
                Token = jwt
            };
        }
    }
}