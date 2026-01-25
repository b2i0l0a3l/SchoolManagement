using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagement.Application.Features.Messages.Request.command.ClassCommandRequest;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Util.Profiles
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassModel>().ReverseMap();
            CreateMap<Class, AddNewClassRequest>().ReverseMap();
            CreateMap<Class, UpdateClassRequest>().ReverseMap();
        }
    }
}