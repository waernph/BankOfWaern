using Bank_of_Waern.Data.Entities;
using BrewHub.Core.Interfaces;
using System.Security.Claims;

namespace BrewHub.Core.Services
{
    public class JwtGetter : IJwtGetter
    {
        private readonly IHttpContextAccessor _jwt;

        public JwtGetter(IHttpContextAccessor httpContextAccessor)
        {
            _jwt = httpContextAccessor;
        }

        public async Task<string> GetLoggedInUserId()
        {
            return _jwt.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
        }
    }
}
