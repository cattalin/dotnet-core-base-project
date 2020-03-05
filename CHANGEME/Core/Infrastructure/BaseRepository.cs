using Core.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        IServiceScope scope;
        DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
        }

        public BaseRepository(IServiceProvider serviceProvider)
        {
            scope = serviceProvider.CreateScope();
            context = scope.ServiceProvider.GetRequiredService<DbContext>();
        }

        public virtual T GetById(int id)
        {
            return context.Set<T>()
            .Where(x => x.Id == id)
            .AsNoTracking()
            .First();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>()
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstAsync();
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAndDetachAsync(entity);
            return entity;
        }

        public virtual async Task<List<T>> InsertBulkAsync(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return new List<T>();
            }

            await context.Set<T>().AddRangeAsync(entities);
            await context.SaveChangesAndDetachAsync(entities);
            return entities;
        }

        public virtual async Task InsertAndForgetBulkAsync(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return;
            }

            await context.Set<T>().AddRangeAsync(entities);
            await context.SaveChangesAsync(); ;
        }

        public virtual int Update(T newEntity)
        {
            context.Set<T>().Update(newEntity);
            return context.SaveChangesAndDetach(newEntity);
        }

        public virtual async Task<int> UpdateAsync(T newEntity)
        {
            context.Set<T>().Update(newEntity);
            return await context.SaveChangesAndDetachAsync(newEntity);
        }

        public virtual async Task<int> UpdateBulkAsync(List<T> newEntities)
        {
            context.Set<T>().UpdateRange(newEntities);
            return await context.SaveChangesAndDetachAsync(newEntities);
        }

        public virtual async Task<int> RemoveAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            return await context.SaveChangesAndDetachAsync(entity);
        }

        public virtual async Task<int> RemoveBulkAsync(List<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
            return await context.SaveChangesAndDetachAsync(entities);
        }

        public void Dispose()
        {
            context.Dispose();
            scope.Dispose();
        }
    }
}
