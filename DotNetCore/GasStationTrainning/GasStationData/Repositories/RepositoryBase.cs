using GasStationData.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStationData.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly Models.GasStationContext _dbContext;

        public RepositoryBase(Models.GasStationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TEntity> GetAll() {
            return _dbContext.Set<TEntity>().ToList();
        }

        public virtual async Task< List<TEntity> > GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> FindByIdAsync(params object[] id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public TEntity FindById(params object[] id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void InsertGraph(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(params object[] id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            //var objectState = entity as IObjectState;
            //if (objectState != null)
            //    objectState.State = ObjectState.Deleted;
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Set<TEntity>().Add(entity);
        }
    }
}
