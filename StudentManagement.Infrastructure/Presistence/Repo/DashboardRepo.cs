using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Infrastructure.Presistence.Repo
{
    public class DashboardRepo : IDashboardRepo
    {
        private readonly AppDbContext _Context;
        public DashboardRepo(AppDbContext context)        {
            _Context = context;
        }

        public async Task<Result<DashboardSummaryModel>> GetSummary()
        {
            var connection = _Context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                connection.Open();

            try
            {
                string sql = @"
                    SELECT 
                        (SELECT COUNT(*) FROM ""Students"") AS TotalStudents,
                        (SELECT COUNT(*) FROM ""Teachers"") AS TotalTeachers,
                        (SELECT COUNT(*) FROM ""Courses"") AS TotalCourses,
                        (SELECT COUNT(*) FROM ""Departments"") AS TotalDepartments
                ";
                var total = await connection.QueryFirstOrDefaultAsync<DashboardSummaryModel>(sql);
                if (total == null)
                    return new Error("GetSummaryERROR", ErrorType.NotFound, "No Data Found For Dashboard Summary.");
                return total;
            }
            catch
            {
                return new Error("GetSummaryERROR", ErrorType.Failure, "Error Happend By Fetching Dashboard Summary Data.");
            }
        }

    }
}