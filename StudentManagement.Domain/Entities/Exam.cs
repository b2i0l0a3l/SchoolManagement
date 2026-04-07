using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Domain.Entities
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty; // e.g. Midterm, Final
        public DateTime Date { get; set; }
        
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject? Subject { get; set; }

        public float MaxScore { get; set; } = 100;

        public ICollection<Grade>? Grades { get; set; }
    }
}
