using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Models
{
    public record SubjectModel
    {
        public int Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public DateTime CreatAt { get; set; }
    }
}
