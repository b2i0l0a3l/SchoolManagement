using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Domain.Entities
{
    public class Subject 
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string SubjectName { get; set; } = string.Empty;
        public DateTime CreatAt { get; set; } = DateTime.UtcNow;
        public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
    }
}