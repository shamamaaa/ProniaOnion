using System.Linq.Expressions;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity,new()
	{
        //Get methods

        IQueryable<T> GetAll(bool isTracking = true,
            bool ignoreQuery = false,
            params string[] includes);


        IQueryable<T> GetAllWhere(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null,
            bool IsDescending = false,
            int skip = 0,
            int take = 0,
            bool isTracking = true,
            bool ignoreQuery = false,
            params string[] includes
            );

        Task<T> GetByIdAsync(int id,
            bool isTracking = true,
            bool ignoreQuery = false,
            params string[] includes);

        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression,
            bool isTracking = true,
            bool ignoreQuery = false,
            params string[] includes);

        ///////
        ///
        Task<bool> IsExistAsync(Expression<Func<T, bool>>? expression,
            bool ignoreQuery = false);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        void SoftDelete(T entity);
        void ReverseSoftDelete(T entity);

        Task SaveChangesAsync();
    }
}

