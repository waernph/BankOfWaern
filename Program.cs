using Bank_of_Waern.Core.Interfaces;
using Bank_of_Waern.Core.Services;
using Bank_of_Waern.Data;
using Bank_of_Waern.Data.Interfaces;
using Bank_of_Waern.Data.Repos;
using BrewHub.Core.Interfaces;
using BrewHub.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration["connString"];
var apiKey = builder.Configuration["ApiKey"]!;  //Secret key för JWT

builder.Services.AddDbContext<BankAppDataContext>(opt => opt.UseSqlServer(connString));



//JWT
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5256",
            ValidAudience = "http://localhost:5256",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiKey)) //User-Secrets nyckel för JWT
        };
    });

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IJwtGetter, JwtGetter>();

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

var app = builder.Build();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints( endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI();



app.Run();

// ConnString: Data Source=PHILIPWAERN;Initial Catalog=Northwind;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;

//Scaffold-DbContext "Data Source=PHILIPWAERN;Initial Catalog=Bank of Waern;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -ContextDir Data -OutputDir Data/Entities 

// ApiKey1948051&%%485734579453%&//1203859