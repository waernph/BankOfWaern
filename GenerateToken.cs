using Bank_of_Waern.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank_of_Waern
{
    public class GenerateToken
    {
        private readonly IConfiguration _config;

        public GenerateToken(IConfiguration config)
        {
            _config = config;
        }


        public async Task<string> GetToken(string role, string emailAddress)
        {
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim(ClaimTypes.Email, emailAddress!));
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
}
