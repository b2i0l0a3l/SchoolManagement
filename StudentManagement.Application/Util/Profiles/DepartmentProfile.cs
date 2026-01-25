using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagement.Application.Features.Messages.Request.command.DepartmentCommandRequest;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Util.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentModel>().ReverseMap();
            CreateMap<Department, AddNewDepartmentRequest>().ReverseMap();
            CreateMap<Department, UpdateDepartmentRequest>().ReverseMap();
        }
    }
}
