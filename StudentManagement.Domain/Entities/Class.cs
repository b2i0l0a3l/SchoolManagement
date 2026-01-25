using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Domain.Entities
{
    public class Class 
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string ClassName { get; set; } = string.Empty;
        public DateOnly Year { get; set; } 
        public ICollection<Student>? Student { get; set; }
        public ICollection<TeacherSubject>? teacherSubjects { get; set; }
    }
}