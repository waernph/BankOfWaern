using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface ICustomerRepo
    {
        public Task ChangePassword(string hashedPassword, int customerId);
        public Task<Customer> CreateCustomer(string firstName, string lastName, string gender, string street,
            string city, string zip, string country, string countryCode, string birthday, string emailAdress,
            string phoneCountryCode, string phoneNumber, string password);

        public Task SaveNewPassword(Customer user, string hashedPassword);
        public Task<Customer> Login(string birthday, string email);
        public Task<Customer> FindCustomer(int cusotmerId);
        public Task<bool> CheckIfCustomerExists(string email, string birthday);
        public Task<int> GetCustomerByEmail(string email);
    }
}
