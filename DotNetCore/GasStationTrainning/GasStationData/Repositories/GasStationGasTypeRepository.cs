using System;
using System.Collections.Generic;
using System.Text;
using GasStationData.Models;

namespace GasStationData.Repositories
{
    public class GasStationGasTypeRepository : RepositoryBase<Models.GasStationGasType>, IRepositories.IGasStationGasTypeRepository
    {
        public GasStationGasTypeRepository(GasStationContext dbContext) : base(dbContext)
        {
        }
    }
}
