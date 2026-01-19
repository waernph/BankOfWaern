using Bank_of_Waern.Data.Entities;
using Bank_of_Waern.Data.Interfaces;
using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Bank_of_Waern.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly BankAppDataContext _context;

        public CustomerRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task ChangePassword(string oldPassword, string newPassword, int customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId)!;

            if (customer.Password != oldPassword)
                throw new Exception("You entered the wrong current password");
            else
            {
                customer.Password = newPassword;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Customer> CreateCustomer(string firstName, string lastName, string gender, string street, 
            string city, string zip, string country, string countryCode, string birthday, string emailAdress, 
            string phoneCountryCode, string phoneNumber)
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
                Password = Guid.NewGuid().ToString().Substring(0, 16)
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

        public async Task<string> GeneratePassword(Customer user)
        {
            var temp = Guid.NewGuid().ToString().Substring(0, 16);
            user.Password = temp;
            _context.Customers.Update(user);
            await _context.SaveChangesAsync();
            return user.Password;
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