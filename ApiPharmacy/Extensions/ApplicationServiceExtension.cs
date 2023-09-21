using Application.UnitOfWork;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ApiPharmacy.Extensions;

public static class ApplicationServiceExtension
{
    public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()    //WithOrigins("https://domini.com")
                .AllowAnyMethod()           //WithMethods(*GET", "POST")
                .AllowAnyHeader());         //WithHeaders(*accept*, "content-type")
            });
    
    public static void AddApplicationServices(this IServiceCollection services) 
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();
        }
}
