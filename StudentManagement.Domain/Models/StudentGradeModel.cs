using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Domain.enums;

namespace StudentManagement.Domain.Models
{
    public class StudentGradeModel
    {
        public int studentid { get; set; }
        public string fullname { get; set; } = string.Empty;
        public string classname { get; set; } = string.Empty;
        public enGender gender { get; set; }
        public float grade { get; set; }
    }
}