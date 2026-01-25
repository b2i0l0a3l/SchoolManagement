using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagement.Application.Features.Messages.Request.command.SubjectCommandRequest;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Util.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectModel>().ReverseMap();
            CreateMap<Subject, AddNewSubjectRequest>().ReverseMap();
            CreateMap<Subject, UpdateSubjectRequest>().ReverseMap();
        }
    }
}
