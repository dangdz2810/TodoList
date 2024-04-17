
using Authentication.Dao.Specifications;
using Authentication.Data;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repository.GenericRepositories
{
    public class GenericRepository<T>(DataContext context) : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context = context;

        public async Task CreateAsync(T entity)
        {
             await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        //Lấy tất cả bản ghi theo điều kiện
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        // Đếm số lượng phần tử trả ra theo điều kiện
        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        // Lấy phần tử đầu tiên theo điều kiện
        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }

        public async Task DeleteRange(IReadOnlyList<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
        }
    }
}
