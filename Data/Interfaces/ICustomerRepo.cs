using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface ICustomerRepo
    {
        public Task ChangePassword(string oldPassword, string newPassword);
        public Task<Customer> CreateCustomer(string firstName, string lastName, string gender, string street, string city, string zip, string country, string countryCode, string birthday, string emailAdress, string phoneCountryCode, string phoneNumber);
        public Task<string> GeneratePassword(Customer user);
        public Task<Customer> Login(string birthday, string email);
    }
}
