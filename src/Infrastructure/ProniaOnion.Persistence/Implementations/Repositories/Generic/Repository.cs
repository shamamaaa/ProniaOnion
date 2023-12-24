using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Persistence.Contexts;
using System.Linq.Expressions;

namespace ProniaOnion.Persistence.Implementations.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly DbSet<T> _table;
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _table = context.Set<T>();
            _context = context;
        }

        //Get methods

        public IQueryable<T> GetAll(bool isTracking = true, bool ignoreQuery = false, params string[] includes)
        {
            var query = _table.AsQueryable();

            query = IncludeMethod(query, includes);

            if (ignoreQuery) query = query.IgnoreQueryFilters();

            return isTracking ? query : query.AsNoTracking();
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null, bool IsDescending = false, int skip = 0, int take = 0, bool isTracking = true, bool ignoreQuery = false, params string[] includes)
        {
            var query = _table.AsQueryable();

            if (expression is not null) query = query.Where(expression);

            if (orderExpression is not null)
            {
                if (IsDescending)
                    query = query.OrderByDescending(orderExpression);
                else query = query.OrderBy(orderExpression);
            }

            if (skip != 0) query = query.Skip(skip);

            if (take != 0) query = query.Take(take);

            query = IncludeMethod(query, includes);

            if (ignoreQuery) query = query.IgnoreQueryFilters();

            return isTracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id, bool isTracking = true, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(x => x.Id == id);

            query = IncludeMethod(query, includes);

            if (!isTracking) query = query.AsNoTracking();

            if (ignoreQuery) query = query.IgnoreQueryFilters();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>>? expression, bool isTracking = true, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(expression);

            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            if (!isTracking) query = query.AsNoTracking();

            if (ignoreQuery) query = query.IgnoreQueryFilters();

            return await query.FirstOrDefaultAsync();

        }


        public async Task AddAsync(T item)
        {
            await _table.AddAsync(item);
        }

        public void Delete(T item)
        {
            _table.Remove(item);
        }

        public void SoftDelete(T item)
        {
            item.IsDeleted = true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T item)
        {
            _table.Update(item);
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>>? expression, bool ignoreQuery = false)
        {
            return ignoreQuery ? await _table.AnyAsync(expression) : await _table.IgnoreQueryFilters().AnyAsync(expression);
        }

        public void ReverseSoftDelete(T entity)
        {
            entity.IsDeleted = false;
        }

        private IQueryable<T> IncludeMethod(IQueryable<T> query, params string[] includes)
        {
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }
    }

}

