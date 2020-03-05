using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Database.Context
{
    public static class DatabaseContextExtensions
    {
        public static async Task<int> SaveChangesAndDetachAsync<T>(this DbContext context, T entity)
        {
            var result = await context.SaveChangesAsync();

            context.Entry(entity).State = EntityState.Detached;

            return result;
        }

        public static int SaveChangesAndDetach<T>(this DbContext context, T entity)
        {
            var result = context.SaveChanges();

            context.Entry(entity).State = EntityState.Detached;

            return result;
        }

        public static async Task<int> SaveChangesAndDetachAsync<T>(this DbContext context, List<T> entities)
        {
            var result = await context.SaveChangesAsync();

            entities.ForEach(entity =>
            {
                context.Entry(entity).State = EntityState.Detached;
            });

            return result;
        }

        public static int SaveChangesAndDetach<T>(this DbContext context, List<T> entities)
        {
            var result = context.SaveChanges();

            entities.ForEach(entity =>
            {
                context.Entry(entity).State = EntityState.Detached;
            });

            return result;
        }
    }
}
