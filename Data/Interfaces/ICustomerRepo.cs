using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface ICustomerRepo
    {
        public Task<string> GeneratePassword(Customer user);
        public Task<List<Customer>> GetAllCustomers();
        public Task<Customer> Login(string birthday, string email);
    }
}
