using System;
using System.Collections.Generic;
using System.Text;
using GasStationData.Models;

namespace GasStationData.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GasStationContext _dbContext;
        private bool _disposed = false;
        public UnitOfWork(GasStationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public GasStationContext GetDBContext()
        {
            return _dbContext;
        }
        public void Commit()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _dbContext != null)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

    }
}
