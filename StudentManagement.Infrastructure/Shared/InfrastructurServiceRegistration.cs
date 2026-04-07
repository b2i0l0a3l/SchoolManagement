using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreSystem.Infrastructure.Persistence.Repo;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Presistence;
using StudentManagement.Infrastructure.Presistence.Repo;

namespace StudentManagement.Infrastructure.Shared
{
    public static class InfrastructurServiceRegistration
    {
        public static void AddInfrastructurServiceRegistration(this IServiceCollection services,IConfiguration configuration)
        {

           
     
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
services.AddScoped<IDashboardRepo, DashboardRepo>();
        }
        
    }
}