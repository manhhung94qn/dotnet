using GasStationData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationData.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        GasStationContext GetDBContext();
    }
}
