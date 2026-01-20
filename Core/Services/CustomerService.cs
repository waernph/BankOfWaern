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

        public CustomerService(ICustomerRepo customerRepo, IConfiguration config, IJwtHelper jwtHelper)
        {
            _customerRepo = customerRepo;
            _config = config;
            _jwtHelper = jwtHelper;
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
            else if (user.Password != password)
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
            var newCustomer = await _customerRepo.CreateCustomer(firstName, lastName, gender, street,
             city, zip, country, countryCode, birthday, emailAdress,
             phoneCountryCode, phoneNumber);
            return newCustomer;
        }

        public async Task ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var customerId = await _jwtHelper.GetLoggedInCustomerId();

            if (newPassword != confirmPassword)
            {
                throw new Exception("New password and confirm password do not match.");
            }
            else
            {
                await _customerRepo.ChangePassword(oldPassword, newPassword, customerId);
            }
        }
    }
}
