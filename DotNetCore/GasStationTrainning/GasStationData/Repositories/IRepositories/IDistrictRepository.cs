using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationData.Repositories.IRepositories
{
    public interface IDistrictRepository: IRepositoryBase<Models.MDistrict>
    {
        List<GasStationData.Models.MDistrict> GetAllDistrictsOrderBy(string orderBy);
    }
}
