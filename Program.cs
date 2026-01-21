using Bank_of_Waern.Data;
using Bank_of_Waern.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration["connString"]; //Secret key för connection string
var apiKey = builder.Configuration["ApiKey"]!;  //Secret key för JWT

builder.Services.AddDbContext<BankAppDataContext>(opt => opt.UseSqlServer(connString));
builder.Services.AddAutoMapperProfiles(); //Extension method för att lägga till alla AutoMapper profiler

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
builder.Services.AddScoped(); //Extension method för att lägga till alla AddScoped
builder.Services.AddSwaggerGenSetup(); //Extension method för att lägga till SwaggerGen

var app = builder.Build();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI(opt => opt.EnableTryItOutByDefault());
app.Run();