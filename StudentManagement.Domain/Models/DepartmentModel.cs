using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Models
{
    public record DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
