using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StudentManagement.Domain.Common;

namespace StudentManagement.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<Result<T>> Add(T Entity);
        Task<Result<bool>> Delete(int Id);
        Task<Result<bool>> Update(int Id, Action<T> UpdateAction);
        Task<Result<T?>> GetById(int Id);
        Task<Result<T?>> GetByCondition(Expression<Func<T, bool>> exp);
        Task<Result<IEnumerable<T>?>> GetِAllByCondition(Expression<Func<T, bool>> exp);
        Task<Result<IEnumerable<T>?>> GetAll();
    }
}