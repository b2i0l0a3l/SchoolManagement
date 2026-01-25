using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagement.Application.Features.Messages.Request.command.GradeCommandRequest;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Util.Profiles
{
    public class GradeProfile : Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, GradeModel>().ReverseMap();
            CreateMap<Grade, AddNewGradeRequest>().ReverseMap();
            CreateMap<Grade, UpdateGradeRequest>().ReverseMap();
        }
    }
}
