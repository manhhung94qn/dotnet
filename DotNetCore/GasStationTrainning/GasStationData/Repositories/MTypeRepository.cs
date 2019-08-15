using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GasStationData.Models;
using Microsoft.EntityFrameworkCore;

namespace GasStationData.Repositories
{
    public class MTypeRepository : RepositoryBase<Models.MType>, IRepositories.IMTypeRepository
    {
        public MTypeRepository(GasStationContext dbContext) : base(dbContext)
        {
        }

        public override async Task<List<Models.MType>> GetAllAsync()
        {
            return await _dbContext.MType.Where(e => e.TypeType == 3).ToListAsync();
        }

        public string getTypeText(string typeCode, int typeType)
        {
            var mtype = _dbContext.MType.Where(m => m.TypeCode == typeCode && m.TypeType == typeType).FirstOrDefault();
            if (mtype != null)
            {
                return mtype.TypeText;
            }
            return "";
        }
    }
}
