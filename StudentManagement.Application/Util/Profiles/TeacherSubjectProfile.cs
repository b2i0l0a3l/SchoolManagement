using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagement.Application.Features.Messages.Request.command.TeacherSubjectCommandRequest;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Util.Profiles
{
    public class TeacherSubjectProfile : Profile
    {
        public TeacherSubjectProfile()
        {
            CreateMap<TeacherSubject, TeacherSubjectModel>().ReverseMap();
            CreateMap<TeacherSubject, AddNewTeacherSubjectRequest>().ReverseMap();
            CreateMap<TeacherSubject, UpdateTeacherSubjectRequest>().ReverseMap();
        }
    }
}
