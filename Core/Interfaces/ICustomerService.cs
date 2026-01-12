using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Core.Interfaces
{
    public interface ICustomerService
    {
        public Task <List<Customer>> GetAllCustomers();
        public Task<Customer> Login(string birthday, string email, string password);
        public Task<string> GenerateToken(Customer user);
    }
}
