using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        IServiceScope scope;
        DbContext rawContext;

        public BaseRepository(DbContext context)
        {
            this.rawContext = context;
        }

        public BaseRepository(IServiceProvider serviceProvider)
        {
            scope = serviceProvider.CreateScope();
            rawContext = scope.ServiceProvider.GetRequiredService<DbContext>();
        }

        public virtual T GetRawById(int id)
        {
            return rawContext.Set<T>()
                .Where(x => x.Id == id)
                .Where(x => !x.IsDeleted)
                .AsNoTracking()
                .First();
        }

        public virtual async Task<T> GetRawByIdAsync(int id)
        {
            return await rawContext.Set<T>()
                .Where(x => x.Id == id)
                .Where(x => !x.IsDeleted)
                .AsNoTracking()
                .FirstAsync();
        }

        public virtual IEnumerable<T> GetRawList(int skip, int take)
        {
            var results = rawContext.Set<T>()
                .Where(x => !x.IsDeleted)
                .Skip(skip)
                .Take(take)
                .ToList();

            return results;
        }

        public virtual async Task<IEnumerable<T>> GetRawListAsync(int skip, int take)
        {
            var results = await rawContext.Set<T>()
                .Where(x => !x.IsDeleted)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return results;
        }

        public virtual T Insert(T entity)
        {
            SetCreationTimestamp(entity);

            rawContext.Set<T>().Add(entity);
            rawContext.SaveChanges();
            return entity;
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            SetCreationTimestamp(entity);

            await rawContext.Set<T>().AddAsync(entity);
            await rawContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<T>> InsertBulkAsync(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return new List<T>();
            }

            entities.ForEach(entity => SetCreationTimestamp(entity));

            await rawContext.Set<T>().AddRangeAsync(entities);
            await rawContext.SaveChangesAsync();
            return entities;
        }

        public virtual async Task InsertAndForgetBulkAsync(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return;
            }

            entities.ForEach(entity => SetCreationTimestamp(entity));

            await rawContext.Set<T>().AddRangeAsync(entities);
            await rawContext.SaveChangesAsync();
        }

        private T SetCreationTimestamp(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            return entity;
        }

        public virtual int Update(T newEntity)
        {
            SetUpdateTimestamp(newEntity);

            rawContext.Set<T>().Update(newEntity);
            return rawContext.SaveChanges();
        }

        public virtual async Task<int> UpdateAsync(T newEntity)
        {
            SetUpdateTimestamp(newEntity);

            rawContext.Set<T>().Update(newEntity);
            return await rawContext.SaveChangesAndDetachAsync(newEntity);
        }

        public virtual async Task<int> UpdateBulkAsync(List<T> entities)
        {
            entities.ForEach(newEntity => SetCreationTimestamp(newEntity));

            rawContext.Set<T>().UpdateRange(entities);
            return await rawContext.SaveChangesAsync();
        }

        private T SetUpdateTimestamp(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            return entity;
        }

        public virtual async Task<int> RemoveAsync(T entity)
        {
            entity.IsDeleted = true;
            return await UpdateAsync(entity);
        }

        public virtual async Task<int> RemoveBulkAsync(List<T> entities)
        {
            entities.ForEach(entity => entity.IsDeleted = true);
            return await UpdateBulkAsync(entities);
        }

        public virtual async Task<int> HardRemoveAsync(T entity)
        {
            rawContext.Set<T>().Remove(entity);
            return await rawContext.SaveChangesAsync();
        }

        public virtual async Task<int> HardRemoveBulkAsync(List<T> entities)
        {
            rawContext.Set<T>().RemoveRange(entities);
            return await rawContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (rawContext != null)
                rawContext.Dispose();
            
            if(scope!= null)
                scope.Dispose();
        }
    }
}
