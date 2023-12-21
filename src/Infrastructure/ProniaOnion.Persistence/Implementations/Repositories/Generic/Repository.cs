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
            _table.Update(item);
        }

        public IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null,
            bool IsDescending = false, int skip = 0,
            int take = 0,
            bool isTracking = true,
            bool IsDeleted = false,
            params string[] includes)
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

            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            if (IsDeleted) query = query.IgnoreQueryFilters();

            return isTracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T item = await _table.FirstOrDefaultAsync(c => c.Id == id);
            return item;

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T item)
        {
            _table.Update(item);
        }

    }
}

