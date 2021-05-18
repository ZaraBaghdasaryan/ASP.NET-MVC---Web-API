using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VOD.Database.Services
{
    public class DbReadService : IDbReadService //All the methods about reading the data from the database 
    {
        private VODContext _db;
        public DbReadService(VODContext db)
        {
            _db = db;
        }

        public async Task<List<TEntity>> GetAsync<TEntity>() where TEntity : class
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return await _db.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
        }

        public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
             return await _db.Set<TEntity>().AnyAsync(expression);

        }

       public void Include<TEntity>() where TEntity : class
        {
            var propertyNames = _db.Model
                
                .FindEntityType(typeof(TEntity))
                .GetNavigations()
                .Select(e => e.Name);

            foreach (var name in propertyNames)
                
                _db.Set<TEntity>().Include(name).Load();
        }
       public void Include<TEntity1, TEntity2>() where TEntity1 : class where TEntity2 : class
        {
            Include<TEntity1>();
            Include<TEntity2>();
        }
    }
}
