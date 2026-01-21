using Bank_of_Waern.Data.Profiles;

namespace Bank_of_Waern.Extensions
{
    public static class AutoMapperCollection
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(AccountProfile));
            services.AddAutoMapper(cfg => { }, typeof(TransactionProfile));
            services.AddAutoMapper(cfg => { }, typeof(LoanProfile));

            return services;
        }
    }
}
