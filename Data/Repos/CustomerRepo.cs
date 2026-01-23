using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;

namespace Bank_of_Waern.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly BankAppDataContext _context;
        private readonly IPasswordService _passwordService;

        public CustomerRepo(BankAppDataContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task ChangePassword(string hashedPassword, int customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId)!;
                customer.Password = hashedPassword;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfCustomerExists(string email, string birthday)
        {
            var birthdayParsed = DateOnly.ParseExact(birthday, "yyyyMMdd");
            var customerExists = await _context.Customers.AnyAsync(c => c.Emailaddress == email && c.Birthday == birthdayParsed);
            return customerExists;

        }

        public async Task<Customer> CreateCustomer(string firstName, string lastName, string gender, string street, 
            string city, string zip, string country, string countryCode, string birthday, string emailAdress, 
            string phoneCountryCode, string phoneNumber,string password)
        {
            
            var newCustomer = new Customer
            {
                Givenname = firstName,
                Surname = lastName,
                Gender = gender,
                Streetaddress = street,
                City = city,
                Zipcode = zip,
                Country = country,
                CountryCode = countryCode,
                Birthday = DateOnly.ParseExact(birthday, "yyyyMMdd"),
                Emailaddress = emailAdress,
                Telephonecountrycode = phoneCountryCode,
                Telephonenumber = phoneNumber,
                Password = password
            };
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<Customer> FindCustomer(int cusotmerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == cusotmerId);
            if (customer == null)
                throw new Exception("wrong customerID");
            else 
                return customer;
        }

        public async Task SaveNewPassword(Customer user, string hashedPassword)
        {
            user.Password = hashedPassword;
            _context.Customers.Update(user);
            await _context.SaveChangesAsync();

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

        public async Task<int> GetCustomerByEmail(string email)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Emailaddress == email);
            if (customer == null)
                throw new Exception("No registered customer");
            else
                return customer.CustomerId;
        }
    }
}
//62be4ac7-3202-40