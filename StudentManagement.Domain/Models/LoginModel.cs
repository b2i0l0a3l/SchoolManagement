using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Models
{
    public record LoginModel
    {
        public string AccessKey { get; set; } = string.Empty;
    }
}