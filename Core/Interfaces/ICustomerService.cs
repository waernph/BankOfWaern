using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Core.Interfaces
{
    public interface ICustomerService
    {
        public Task<Customer> Login(string birthday, string email, string password);
        public Task<string> GenerateToken(Customer user);
        Task<Customer> CreateCustomer(string firstName, string lastName, string gender, string street, string city, string zip, string country, string countryCode, string birthday, string emailAdress, string phoneCountryCode, string phoneNumber);
        Task ChangePassword(string oldPassword, string newPassword, string confirmPassword);
    }
}
