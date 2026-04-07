using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Domain.Entities
{
    public class Teacher 
    {
        [Key]
        public int Id { get; set; }
        public string? UserId {get;set;}
        public string FullName { get; set; } = string.Empty;
        public DateTime HireDate { get; set; } = DateTime.UtcNow;
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; } 
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
    }
}