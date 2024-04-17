using Authentication.Data;
using Authentication.Repository.GenericRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections;

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

        public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : class
        {
            if(_repositories == null)
            {
                _repositories = new Hashtable();
            }
            var type = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(type)) {
                var repo = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repo);
            }
            return _repositories[type] as IGenericRepository<TEntity>;
        }
    }
}
