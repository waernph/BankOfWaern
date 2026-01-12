using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank_of_Waern.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly BankAppDataContext _context;

        public CustomerRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<string> GeneratePassword(Customer user)
        {
            var temp = Guid.NewGuid().ToString().Substring(0, 16);
            user.Password = temp;
            _context.Customers.Update(user);
            await _context.SaveChangesAsync();
            return user.Password;
        }

        public Task<List<Customer>> GetAllCustomers()
        {
            return _context.Customers.ToListAsync();
        }

        public async Task<Customer> Login(string birthday, string email)
        {
            DateOnly parsedBirthday =  DateOnly.ParseExact(birthday, "yyyyMMdd");
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Birthday == parsedBirthday && c.Emailaddress == email);
            if (customer == null)
            {
                throw new Exception("No registered customer");
            }
            else
                return customer;
        }
    }
}
//62be4ac7-3202-40