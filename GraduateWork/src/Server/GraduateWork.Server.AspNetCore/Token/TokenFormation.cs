using GraduateWork.Common.CommonProject;
using GraduateWork.Server.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GraduateWork.Server.AspNetCore.Token
{
    public class TokenFormation : ITokenFormation
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly AuthOptions _authOptions;

        public TokenFormation(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions.Value;
            _tokenHandler = new JwtSecurityTokenHandler();
        }
        public string GetToken(AuthorizationResponse user)
        {
            var claims = new List<Claim>
            {
                new Claim("user_id", user.Id),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Authorization", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(issuer: _authOptions.ISSUER, audience: _authOptions.AUDIENCE, notBefore: now, claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_authOptions.LIFETIME)), signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

            return _tokenHandler.WriteToken(jwt);
        }
    }
}
