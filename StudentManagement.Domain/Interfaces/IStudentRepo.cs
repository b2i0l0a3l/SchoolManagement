using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Models;

namespace StudentManagement.Domain.Interfaces
{
    public interface IStudentRepo : IRepository<Student>
    {
        Task<Result<List<StudentGradeModel>>> GetPassedStudentByClassId(int ClassId);
        Task<Result<List<StudentGradeModel>>> GetAllPassedStudent();
    }
}