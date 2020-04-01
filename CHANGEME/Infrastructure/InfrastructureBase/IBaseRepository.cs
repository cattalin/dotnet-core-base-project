using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Base
{
    public interface IBaseRepository<T> : IDisposable where T : BaseEntity
    {
        T GetRawById(int id);
        Task<T> GetRawByIdAsync(int id);
        IEnumerable<T> GetRawList(int skip, int take);
        Task<IEnumerable<T>> GetRawListAsync(int skip, int take);
        T Insert(T entity);
        Task<T> InsertAsync(T entity);
        Task<List<T>> InsertBulkAsync(List<T> entities);
        Task InsertAndForgetBulkAsync(List<T> entities);
        int Update(T newEntity);
        Task<int> UpdateAsync(T newEntity);
        Task<int> UpdateBulkAsync(List<T> newEntities);
        Task<int> RemoveAsync(T entity);
        Task<int> RemoveBulkAsync(List<T> entities);
        Task<int> HardRemoveAsync(T entity);
        Task<int> HardRemoveBulkAsync(List<T> entities);
    }
}
