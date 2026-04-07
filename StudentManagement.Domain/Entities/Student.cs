using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Domain.enums;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Domain.Entities
{
    public class Student 
    {
        [Key]
        public int Id{ get; set; }
        public string? UserId {get;set;}
        [MaxLength(40)]
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public enGender Gender { get; set; }
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public Class? Class { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Parent? Parent { get; set; }

        public ICollection<Grade>? Grades { get; set; }
    }
}