using GraduateWork.Common.CommonProject;

namespace GraduateWork.Server.AspNetCore.Token
{
    public interface ITokenFormation
    {
        string GetToken(AuthorizationResponse user);
    }
}
