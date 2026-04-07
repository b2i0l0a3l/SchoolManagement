using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Domain.Interfaces
{
    public interface IDashboardRepo 
    {
        Task<Result<DashboardSummaryModel>> GetSummary();
      
    }
}