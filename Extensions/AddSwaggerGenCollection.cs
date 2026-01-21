using Microsoft.OpenApi;

namespace Bank_of_Waern.Extensions
{
    public static class AddSwaggerGenCollection
    {
        public static IServiceCollection AddSwaggerGenSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Klistra in JWT-token här"
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            return services;
        }
    }
}
