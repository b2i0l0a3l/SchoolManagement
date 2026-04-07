using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Infrastructure.Presistence;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Interfaces;

namespace StoreSystem.Infrastructure.Persistence.Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _Context;
        protected DbSet<T> _Set;
        public Repository(AppDbContext context)
        {
            _Context = context;
            _Set = _Context.Set<T>();
        }

        public async Task<Result<T>> Add(T Entity)
        {
            try
            {
                await _Set.AddAsync(Entity);
                await _Context.SaveChangesAsync();
                return Entity;
            }
            catch
            {
                return new Error("AddFailed", StudentManagement.Domain.Common.ErrorType.Failure, "A database error occurred.");
            }
        }

        public async Task<Result<IEnumerable<T>>> AddRange(IEnumerable<T> entities)
        {
            try
            {
                await _Set.AddRangeAsync(entities);
                await _Context.SaveChangesAsync();
                return Result<IEnumerable<T>>.Success(entities);
            }
            catch
            {
                return new Error("AddRangeFailed", StudentManagement.Domain.Common.ErrorType.Failure, "A database error occurred during bulk insert.");
            }
        }

        public async Task<Result<bool>> Delete(int Id)
        {
            try
            {
                var result = await findAsync(Id);
                if (result == null) return new Error("DeleteFaild",StudentManagement.Domain.Common.ErrorType.NotFound, "Entity Not Found");;
                _Set.Remove(result);
                await _Context.SaveChangesAsync();
                return true;
            }catch
            {
                return new Error("DeleteFaild", StudentManagement.Domain.Common.ErrorType.Failure, "A database error occurred.");
            }
        }



        public async Task<Result<T?>> GetByCondition(Expression<Func<T, bool>> exp)
        {
            try
            {
                var result = await _Set.FirstOrDefaultAsync(exp);
                if (result == null) return new Error("GetFaild", StudentManagement.Domain.Common.ErrorType.NotFound, "Entity Not Found");
                return result;
            }
            catch
            {
                return new Error("GetFaild", StudentManagement.Domain.Common.ErrorType.Failure, "A database error occurred.");
            }
        }
        
        public async Task<Result<IEnumerable<T>?>> GetِAllByCondition(Expression<Func<T, bool>> exp)
        {
            try
            {
                var result = await _Set.Where(exp).ToListAsync();
                if (result == null) return new Error("GetFaild",StudentManagement.Domain.Common.ErrorType.NotFound, "Entity Not Found");
                return result;
            }catch
            {
                return new Error("GetFaild", StudentManagement.Domain.Common.ErrorType.Failure, "A database error occurred.");
            }
        }

        public async Task<Result<T?>> GetById(int Id)
        {
            try
            {
                var result = await findAsync(Id);
                if (result == null) return new Error("GetByIdFaild",StudentManagement.Domain.Common.ErrorType.NotFound, "Entity Not Found");;
                return result;
            }catch(Exception ex)
            {
                return new Error("GetByIdFaild",StudentManagement.Domain.Common.ErrorType.General,ex.Message);;
            }
        }

        public async Task<Result<bool>> Update(int Id, Action<T> UpdateAction)
        {
            try
            {
                var result = await findAsync(Id);
                if (result == null) return new Error("UpdateFaild", StudentManagement.Domain.Common.ErrorType.NotFound, "Entity Not Found"); ;
                UpdateAction(result);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return new Error("UpdateFaild", StudentManagement.Domain.Common.ErrorType.General, ex.Message); ;
            }
        }
      
        private async Task<T?> findAsync(int Id)
        => await _Set.FindAsync(Id);

     
        public async  Task<Result<IEnumerable<T>?>> GetAll()
        {
             try
            {
                List<T> items = await _Set.AsNoTracking().ToListAsync();
                if (items.Count <= 0) return Errors.DataNotFoundError;
                return items;   
            }
            catch
            {
                return new Error("AllFaild", StudentManagement.Domain.Common.ErrorType.Failure, "A database error occurred.");
            }
        }
    }
}