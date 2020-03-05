using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public interface IBaseRepository<T> : IDisposable where T : BaseEntity
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        Task<T> InsertAsync(T entity);
        Task<List<T>> InsertBulkAsync(List<T> entities);
        Task InsertAndForgetBulkAsync(List<T> entities);
        int Update(T newEntity);
        Task<int> UpdateAsync(T newEntity);
        Task<int> UpdateBulkAsync(List<T> newEntities);
        Task<int> RemoveAsync(T entity);
        Task<int> RemoveBulkAsync(List<T> entities);
    }
}
