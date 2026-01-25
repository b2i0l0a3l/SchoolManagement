using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Domain.enums;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Domain.Entities
{
    public class User:IdentityUser
    {
 
        public string Role { get; set; } = string.Empty;
    }
}