using Authentication.Dao.Specifications;
using System.Linq.Expressions;

namespace Authentication.Repository.GenericRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        ////Có điền kiện
        //Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        //Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        //Task<int> GetCountWithSpecAsync(ISpecification<T> spec);
        //void DeleteRange(IReadOnlyList<T> entity);

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null);
        Task<T> Get(Expression<Func<T, bool>>? filter = null, string includeProperties = "");

    }
}
