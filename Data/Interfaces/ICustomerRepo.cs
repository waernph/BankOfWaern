using Bank_of_Waern.Data.Entities;

namespace Bank_of_Waern.Data.Interfaces
{
    public interface ICustomerRepo
    {
        public Task<List<Customer>> GetAllCustomers();
    }
}
