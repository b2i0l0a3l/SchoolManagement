using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Domain.Entities
{
    public class Department 
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        public Teacher? Teacher { get; set; }
    }
}