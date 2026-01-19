using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using BrewHub.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BrewHub.Core.Services
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IHttpContextAccessor _jwt;
        private readonly IConfiguration _config;

        public JwtHelper(IHttpContextAccessor jwt, IConfiguration config)
        {
            _jwt = jwt;
            _config = config;
        }



        public async Task<int> GetLoggedInCustomerId()
        {
            var customerId = int.Parse(_jwt.HttpContext.User.FindFirst(ClaimTypes.UserData)!.Value);
            return customerId;
        }

        public async Task<string> GetLoggedInEmail()
        {
            var email = _jwt.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
            return email;
        }

        public async Task<string> GetToken(string role, string? email, Customer? user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, role));
            claims.Add(new Claim(ClaimTypes.Email, email.ToString()));
            claims.Add(new Claim(ClaimTypes.UserData, user.CustomerId.ToString()!));
            var IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["ApiKey"]!));
            var signinCredentials = new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);


            var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5256",
                    audience: "http://localhost:5256",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials);


            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}

