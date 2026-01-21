using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Core.Services;
using Bank_of_Waern.Data.Interfaces;
using Bank_of_Waern.Data.Repos;

namespace Bank_of_Waern.Extensions
{
    public static class AddScopedCollection
    {
        public static IServiceCollection AddScoped(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IAccountTypeRepo, AccountTypeRepo>();
            services.AddScoped<IDispositionRepo, DispositionRepo>();
            services.AddScoped<IAdminRepo, AdminRepo>();
            services.AddScoped<ITransactionRepo, TransactionRepo>();
            services.AddScoped<ILoanRepo, LoanRepo>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountTypeService, AccountTypeService>();
            services.AddScoped<IDispositionService, DispositionService>();
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IPasswordService, PasswordService>();

            return services;
        }

    }
}
