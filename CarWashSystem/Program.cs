using CarWashSystem;
using CarWashSystem.Data;
using CarWashSystem.Interfaces;
using CarWashSystem.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(option => {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey=true,
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer=false,
            ValidateAudience=false,
        };
    });

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(name: "AllowOrigin", builder =>
        {
            builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
        });
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options=> {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description="Standard Authorization header using bearer scheme (\"bearer {token\"})",
        In=ParameterLocation.Header,
        Name="Authorization",
        Type=SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    });
builder.Services.AddDbContext<OnDemandDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("CarWashConnectionString")));

builder.Services.AddScoped<IUser,SQLUserRepository>();
builder.Services.AddScoped<IAddon, SQLAddonRepository>();
builder.Services.AddScoped<IWashPackage, SQLWashPackageRepository>();
builder.Services.AddScoped<IPayment, SQLPaymentRepository>();
builder.Services.AddScoped<IOrder,SQLOrderRepository>();
builder.Services.AddScoped<ICar, SQLCarRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
