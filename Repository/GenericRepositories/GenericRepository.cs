
using Authentication.Dao.Specifications;
using Authentication.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Authentication.Repository.GenericRepositories
{
    public class GenericRepository<T>(DbSet<T> dbSet) : IGenericRepository<T> where T : class
    {
        internal DbSet<T> _dbSet = dbSet;

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        //Lấy tất cả bản ghi theo điều kiện
        //public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        //{
        //    return await ApplySpecification(spec).ToListAsync();
        //}
        //// Đếm số lượng phần tử trả ra theo điều kiện
        //public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        //{
        //    return await ApplySpecification(spec).CountAsync();
        //}
        //// Lấy phần tử đầu tiên theo điều kiện
        //public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        //{
        //    return await ApplySpecification(spec).FirstOrDefaultAsync();
        //}
        //private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        //{
        //    return SpecificationEvaluator<T>.GetQuery(_dbSet, spec);
        //}

        public void DeleteRange(IReadOnlyList<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,string ? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<T> Get(Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
