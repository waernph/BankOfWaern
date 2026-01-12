using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank_of_Waern.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IConfiguration _config;

        public CustomerService(ICustomerRepo customerRepo, IConfiguration config)
        {
            _customerRepo = customerRepo;
            _config = config;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customerRepo.GetAllCustomers();
        }
        public async Task<Customer> Login(string birthday, string email, string password)
        {
            var user = await _customerRepo.Login(birthday, email);
            if (user.Password == null)
            {
                var tempPassword = await _customerRepo.GeneratePassword(user);
                throw new Exception($"It looks like you don't have a password. We sent a SMS temporary password... {tempPassword}");
                
            }
            else if (user == null)
            {
                throw new Exception("Invalid login credentials.");
            }
            else if (user.Password != password)
            {
                throw new Exception("Invalid login credentials.");
            }
            else
                return user;
        }
        public async Task<string> GenerateToken(Customer user)
        {
            {
                var userRole = "";
                if (user.IsAdmin == 1)
                    userRole = "Admin";
                else
                    userRole = "User";
                var birthdayString = user.Birthday.ToString().Trim(new char['-']);
                //var userId = user.UserId;
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                claims.Add(new Claim(ClaimTypes.DateOfBirth, birthdayString));
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
