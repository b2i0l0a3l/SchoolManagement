using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Domain.Entities
{
    public class TeacherSubject 
    {
        public int Id { get; set; }
        public int ClassId{ get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher? Teacher { get; set; }
        [ForeignKey("SubjectId")]
        public Subject? Subject { get; set; }
        public Class? Class { get; set; }
    }
}