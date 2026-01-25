using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Infrastructure.Presistence.Repo
{
    public class StudentRepo : Repository<Student>, IStudentRepo
    {
        public StudentRepo(AppDbContext context,ILogger<StudentRepo> logger) :base(context,logger)
        {
        }
        public async Task<Result<List<StudentGradeModel>>> GetPassedStudentByClassId(int ClassId)
        {
            var param = new NpgsqlParameter("p_class_Id", ClassId);
            List<StudentGradeModel> result = await _Context.Set<StudentGradeModel>().FromSqlRaw("SELECT * FROM get_All_student_grades_By_class_Id(@p_class_Id)", param).ToListAsync();
            if (result.Count == 0) new Error("NoStudentPassedFunds",ErrorType.NotFound,"There Is No Result Yets");
            return result;
        }
        public async Task<Result<List<StudentGradeModel>>> GetAllPassedStudent()
        {
            List<StudentGradeModel> result = await _Context.Set<StudentGradeModel>().FromSqlRaw("SELECT * FROM get_All_student_grades()").ToListAsync();
            if (result.Count == 0) new Error("NoStudentPassedFunds",ErrorType.NotFound,"There Is No Result Yets");
            return result;
        }
    }
}