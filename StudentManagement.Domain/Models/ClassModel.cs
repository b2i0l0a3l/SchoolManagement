using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Models
{
    public record ClassModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public DateOnly Year { get; set; } 

    }
}