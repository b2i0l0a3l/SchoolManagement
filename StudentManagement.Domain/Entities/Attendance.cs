using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManagement.Domain.enums;

namespace StudentManagement.Domain.Entities
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }
}
