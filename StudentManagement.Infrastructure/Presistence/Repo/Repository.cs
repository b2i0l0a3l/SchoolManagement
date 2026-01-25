using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Infrastructure.Presistence.Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _Context;
        protected DbSet<T> _Set;
        private ILogger<Repository<T>> _Logger;
        public Repository(AppDbContext context,ILogger<Repository<T>> logger)
        {
            _Context = context;
            _Set = _Context.Set<T>();
            _Logger = logger;
        }

        public async Task<Result<T>> Add(T Entity)
        {
            try
            {
                await _Set.AddAsync(Entity);
                await Save();
                return Entity;
            }
            catch (Exception ex)
            {
                _Logger.LogError("Error happend While adding Data: {0}", ex.Message);
                return new Error("AddFailed",ErrorType.General,ex.Message);
            }
        }


        public async Task<Result<bool>> Delete(int Id)
        {
            try
            {
                var result = await findAsync(Id);
                if (result == null) return new Error("DeleteFaild",ErrorType.General,"Entity Not Found");;
                _Set.Remove(result);
                await Save();
                return true;
            }catch(Exception ex)
            {
                _Logger.LogError("Error happend While Deleting Data: {0}", ex.Message);

                return new Error("DeleteFaild",ErrorType.General,ex.Message);
            }
        }

        public async Task<Result<IEnumerable<T>?>> GetAll()
        {
            try
            {
                var result = await _Set.AsNoTracking().ToListAsync();
                if (result == null|| !result.Any()) return new Error("GetFaild",ErrorType.General,"Entity Not Found");
                return result;
            }catch(Exception ex)
            {
                _Logger.LogError("Error happend While Getting All Data : {0}", ex.Message);
                return new Error("GetFaild",ErrorType.General,ex.Message);
            }
        }

        public async Task<Result<T?>> GetByCondition(Expression<Func<T, bool>> exp)
        {
            try
            {
                var result = await _Set.FirstOrDefaultAsync(exp);
                if (result == null) return new Error("GetFaild",ErrorType.General,"Entity Not Found");
                return result;
            }catch(Exception ex)
            {
                _Logger.LogError("Error happend While Getting Data by Condition : {0}", ex.Message);
                return new Error("GetFaild",ErrorType.General,ex.Message);
            }
        }

        public async Task<Result<T?>> GetById(int Id)
        {
            try
            {
                var result = await findAsync(Id);
                if (result == null) return new Error("GetByIdFaild",ErrorType.General,"Entity Not Found");;
                return result;
            }catch(Exception ex)
            {
                _Logger.LogError("Error happend While Getting Data By Id: {0}", ex.Message);
                return new Error("GetByIdFaild",ErrorType.General,ex.Message);;
            }
        }

        public async Task<Result<bool>> Update(int Id, Action<T> UpdateAction)
        {
            try
            {
                var result = await findAsync(Id);
                if (result == null) return new Error("UpdateFaild", ErrorType.General, "Entity Not Found"); ;
                UpdateAction(result);

                await Save();
                return true;
            }
            catch (Exception ex)
            {
                _Logger.LogError("Error happend While Updating Data: {0}", ex.Message);
                return new Error("UpdateFaild", ErrorType.General, ex.Message); ;
            }
        }
        public async Task<Result<IEnumerable<T>>> GetAllByCondition(Expression<Func<T, bool>> exp)
        {
            try
            {
                if (exp == null) return new Error("QueryNullError", ErrorType.Validation, "Expression is null.");
                var result = await _Set.Where(exp).AsNoTracking().ToListAsync();
                if(result == null|| !result.Any())return new Error("GettingAllFaild", ErrorType.General, "Getting All Data by Condition Failed!"); ;
                if(result.Count == 0)return new Error("GettingAllIsEmptyFaild", ErrorType.General, "there is no record Founds!"); ;
                return result;
                
            } catch (Exception ex)
            {
                _Logger.LogError("Error happend While Getting All Data By Condition: {0}", ex.Message);
                return new Error("UpdateFaild", ErrorType.General, ex.Message); ;
            }
        }
        private async Task<T?> findAsync(int Id)
        => await _Set.FindAsync(Id);
        private async Task<int> Save()
        {
            return await _Context.SaveChangesAsync();
        }
    }
}