using GasStationData.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GasStationData.Repositories.IRepositories
{
    public interface IGasStationRepository: IRepositoryBase<Models.GasStation>
    {
        Task<List<Models.GasStation>> GetAsyncAllGasStationWithQuery(string gasName, long district, string[] gasType);
        bool CheckIsNotExistName(string gasName);
    }

}
