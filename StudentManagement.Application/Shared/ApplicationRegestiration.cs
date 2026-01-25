using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.Application.Behaviors;
using StudentManagement.Application.Util;
using StudentManagement.Application.Util.jwt;

namespace StudentManagement.Application.Shared
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplicationServiceRegistration(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "StudentApi",
                    ValidAudience = "StudentApiUsers",
                    ClockSkew = TimeSpan.FromMinutes(5),
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                };
            });
            
            // MediatR
            service.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            
            // FluentValidation - تسجيل جميع الـ Validators تلقائياً
            service.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
            
            // ValidationBehavior Pipeline
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            service.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(ApplicationAssemblyMarker).Assembly);
            });
            service.AddScoped<IGenerateJwtToken, GenerateJwtTokenService>();
            service.AddScoped<IUserRole, UserRole>();
        }
    }

    public class ApplicationAssemblyMarker { }

}