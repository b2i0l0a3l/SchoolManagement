using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagement.Application.Features.Messages.Request.command.StudentCommandRequest;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Util.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentModel>().ReverseMap();
            CreateMap<Student, AddNewStudentRequest>().ReverseMap();
            CreateMap<Student, UpdateStudentRequest>().ReverseMap();
        }
    }
}
