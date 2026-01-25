using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Models
{
    public class ClassStudentModel
    {
        public int ClassId { get; set; }
        public List<StudentModel>? Students { get; set; }

    }
}