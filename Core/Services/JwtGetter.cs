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

        public async Task<int> GetLoggedInUserId()
        {
            return int.Parse(_jwt.HttpContext.User.FindFirst(ClaimTypes.UserData).Value);
        }
    }
}
