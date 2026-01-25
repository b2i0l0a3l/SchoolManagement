using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Models
{
    public record TeacherModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public int DepartmentId { get; set; }
    }
}
