using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagement.Application.Features.Messages.Request.command.Register;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Util.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterRequest>().ReverseMap();
        }
    }
}