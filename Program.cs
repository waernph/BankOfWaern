using Bank_of_Waern.Data;
using Bank_of_Waern.Data.Profiles;
using Bank_of_Waern.Estensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration["connString"]; //Secret key för connection string
var apiKey = builder.Configuration["ApiKey"]!;  //Secret key för JWT

builder.Services.AddDbContext<BankAppDataContext>(opt => opt.UseSqlServer(connString));
builder.Services.AddAutoMapper(cfg => { }, typeof(AccountProfile));
builder.Services.AddAutoMapper(cfg => { }, typeof(TransactionProfile));
builder.Services.AddAutoMapper(cfg => { }, typeof(LoanProfile));

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
builder.Services.AddScopedServices(); //Extension method för att lägga till alla AddScoped


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
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI(opt => opt.EnableTryItOutByDefault());


app.Run();

