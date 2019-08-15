
namespace GasStationData.Repositories.IRepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepositoryBase<TEntity> where TEntity: class
    {
        List<TEntity> GetAll();
        Task< List<TEntity> > GetAllAsync();

        TEntity FindById(params object[] id);
        Task<TEntity> FindByIdAsync(params object[] id);

        void InsertGraph(TEntity entity);

        void Update(TEntity entity);

        void Delete(params object[] id);

        void Delete(TEntity entity);

        void Insert(TEntity entity);
    }
}
