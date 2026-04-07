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
           
            
            service.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            
            service.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
            
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            service.AddScoped<IGenerateJwtToken, GenerateJwtTokenService>();
        }
    }

    public class ApplicationAssemblyMarker { }

}