using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Domain.enums;

namespace StudentManagement.Domain.Models
{
    public record StudentModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public enGender Gender { get; set; } 
        public int ClassId{ get; set; }
    }
}
