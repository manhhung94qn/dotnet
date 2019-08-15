using GasStationData.Models;
using GasStationData.Repositories.IRepositories;
using GasStationData.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStationData.Repositories
{
    public class GasStationRepository : RepositoryBase<Models.GasStation>, IGasStationRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public GasStationRepository(Models.GasStationContext dbContext, IUnitOfWork unitOfWork) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async override Task<List<Models.GasStation>> GetAllAsync()
        {
            return await _dbContext.GasStation.Include(e => e.GasStationGasType).Include(e => e.GasStationFeedback).ToListAsync();
        }

        public async Task<List<GasStation>> GetAsyncAllGasStationWithQuery(string gasName, long district, string[] gasType)
        {
            Boolean bGasName = String.IsNullOrEmpty(gasName);           
            List<GasStation> gasStations = await _dbContext.GasStation
                 .Include(e => e.GasStationGasType)
                 .Where(e =>
                    (String.IsNullOrEmpty(gasName) ? true : e.GasStationName.ToLowerInvariant().Contains(gasName)) &&
                    (district > 0 ? e.District == district : true) &&
                    (gasType.Length > 0 ? isExitGasType(gasType, e) : true)
                 )
                 .ToListAsync();
            return gasStations;
        }

        public bool CheckIsNotExistName(string gasName) {
            GasStation gasStation = _dbContext.GasStation.Where(e=>e.GasStationName == gasName).FirstOrDefault();
            return gasStation == null ? true : false;
        }

        public bool isExitGasType(string[] clientList, GasStation gasStation)
        {
            foreach (var item in gasStation.GasStationGasType)
            {
                if (clientList.Contains(item.GasType) )
                {
                    return true;
                }
            }
            return false;
        }
    }

}
