using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Pustokk.DAL.DataContext;
using Pustokk.DAL.DataContext.Entities.Common;
using Pustokk.DAL.Paginate;
using Pustokk.DAL.Repositories.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.DAL.Repositories
{
    public class EfCoreRepository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly AppDbContext _context;

        public EfCoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var entityEntry = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            var entityEntry = _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool ignoreQueryFilter = false)
        {
            IQueryable<T> query = _context.Set<T>();

            if (ignoreQueryFilter)
                query = query.IgnoreQueryFilters();

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                query = orderBy(query);

            var result = await query.ToListAsync();

            return result;
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            var query = Query();
            var result = await query.FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool IsTracking = true)
        {
            var query = Query();

            if (predicate != null)
                query = query.Where(predicate);

            if (!IsTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (orderBy != null)
                query = orderBy(query);

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<Paginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true)
        {
            IQueryable<T> queryable = _context.Set<T>();

            if (!enableTracking) queryable = queryable.AsNoTracking();

            if (include != null) queryable = include(queryable);

            if (predicate != null) queryable = queryable.Where(predicate);

            if (orderBy != null) queryable = orderBy(queryable);

            return await queryable.ToPaginateAsync(index, size);
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var entityEntry = _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }
    }
}
