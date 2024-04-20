using Authentication.Data;
using Authentication.Repository.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;
using System.Data.Entity;
namespace Authentication.Dao.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        private DataContext _context;
        private Hashtable _repositories;
        //private IDbContextTransaction _transaction; // Thêm biến để theo dõi giao dịch

        public UnitofWork(DataContext context, Hashtable repositories)
        {
            _context = context;
            _repositories = repositories;
        }
        public async Task<int> Complete()
        {
            await _context.SaveChangesAsync();
            return 1;
        }

        public IGenericRepository<T>? Repository<T>() where T : class
        {
            if(_repositories == null)
            {
                _repositories = new Hashtable();
            }
            var type = typeof(T).Name;
            if(!_repositories.ContainsKey(type)) {
                var dbSet = _context.Set<T>();
                var repo = new GenericRepository<T>(dbSet);
                _repositories.Add(type, repo);
            }
            return _repositories[type] as IGenericRepository<T>;
        }
    }
}
