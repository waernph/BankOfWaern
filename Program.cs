

using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Core.Services;
using Bank_of_Waern.Data;
using Bank_of_Waern.Data.Interfaces;
using Bank_of_Waern.Data.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration["connString"];
builder.Services.AddControllers();
builder.Services.AddDbContext<BankAppDataContext>(opt => opt.UseSqlServer(connString));
builder.Services.AddSwaggerGen(options =>
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

builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints( endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI();



app.Run();

// ConnString: Data Source=PHILIPWAERN;Initial Catalog=Northwind;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;

//Scaffold-DbContext "Data Source=PHILIPWAERN;Initial Catalog=Bank of Waern;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -ContextDir Data -OutputDir Data/Entities 