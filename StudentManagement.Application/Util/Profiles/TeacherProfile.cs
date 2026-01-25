using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagement.Application.Features.Messages.Request.command.TeacherCommandRequest;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Util.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherModel>().ReverseMap();
            CreateMap<Teacher, AddNewTeacherRequest>().ReverseMap();
            CreateMap<Teacher, UpdateTeacherRequest>().ReverseMap();
        }
    }
}
