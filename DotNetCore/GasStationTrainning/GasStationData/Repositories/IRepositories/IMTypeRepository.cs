using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationData.Repositories.IRepositories
{
    public interface IMTypeRepository: IRepositoryBase<Models.MType>
    {
        string getTypeText(string typeCode, int typeType);
    }
}
