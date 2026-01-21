using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IConfiguration _config;
        private readonly IJwtHelper _jwtHelper;
        private readonly IPasswordService _passwordServiece;

        public CustomerService(ICustomerRepo customerRepo, IConfiguration config, 
            IJwtHelper jwtHelper, IPasswordService passwordServiece)
        {
            _customerRepo = customerRepo;
            _config = config;
            _jwtHelper = jwtHelper;
            _passwordServiece = passwordServiece;
        }

        public async Task<Customer> Login(string birthday, string email, string password)
        {
            var user = await _customerRepo.Login(birthday, email);
            if (user.Password == null)
            {
                var tempPassword = await _customerRepo.GeneratePassword(user);
                throw new Exception($"It looks like you don't have a password. We sent an email with a temporary password... {tempPassword}");
            }
            else if (user == null)
            {
                throw new Exception("Invalid login credentials.");
            }
            else if (!BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password))
            {
                throw new Exception("Invalid login credentials.");
            }
            else
                return user;
        }
        

        public async Task<Customer> CreateCustomer(string firstName, string lastName, string gender, string street,
            string city, string zip, string country, string countryCode, string birthday, string emailAdress,
            string phoneCountryCode, string phoneNumber)
        {
            if (await _customerRepo.CheckIfCustomerExists(emailAdress, birthday))
            {
                throw new Exception("Customer with the same email and birthday already exists.");
            }
            else
            {
                var password = Guid.NewGuid().ToString().Substring(0, 16);
                var newCustomer = await _customerRepo.CreateCustomer(firstName, lastName, gender, street,
                 city, zip, country, countryCode, birthday, emailAdress,
                 phoneCountryCode, phoneNumber, password);
                return newCustomer;
            }
        }

        public async Task ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var customerId = await _jwtHelper.GetLoggedInCustomerId();
            var customer =  await _customerRepo.FindCustomer(customerId);

            if (newPassword != confirmPassword)
            {
                throw new Exception("New password and confirm password do not match.");
            }
            else if (!await _passwordServiece.VerifyPassword(oldPassword, customer.Password!))
            {
                throw new Exception("Old password is incorrect.");
            }
            else
            {
                var hashedPassword = await _passwordServiece.HashPassword(newPassword);
                await _customerRepo.ChangePassword(hashedPassword, customerId);
            }
        }
    }
}
