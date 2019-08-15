using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GasStationData.Models;
using Microsoft.EntityFrameworkCore;

namespace GasStationData.Repositories
{
    public class DistrictRepository : RepositoryBase<MDistrict>, IRepositories.IDistrictRepository
    {
        public DistrictRepository(GasStationContext dbContext) : base(dbContext)
        {
        }
        public override async Task<List<MDistrict>> GetAllAsync()
        {
            return await _dbContext.MDistrict.OrderBy(e => e.DistrictName).ToListAsync();
        }

        public List<MDistrict> GetAllDistrictsOrderBy(string orderBy)
        {
            List<MDistrict> districts = new List<MDistrict>();
            //switch (orderBy)
            //{
            //    case "name":
            //        districts = _dbContext.MDistrict.OrderBy()
            //        break;
            //    default:
            //        break;
            //}
            return districts;
        }
    }
}
