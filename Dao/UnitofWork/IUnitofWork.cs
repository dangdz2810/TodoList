using Authentication.Repository.GenericRepositories;

namespace Authentication.Dao.UnitofWork
{
    public interface IUnitofWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Complete();
    }
}
