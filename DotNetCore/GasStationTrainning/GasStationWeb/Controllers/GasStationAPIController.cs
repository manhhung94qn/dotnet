using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationData.Models;
using GasStationData.Repositories.IRepositories;
using GasStationData.UnitOfWork;
using GasStationWeb.ModelsViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GasStationWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GasStationAPIController : ControllerBase
    {
        private readonly IGasStationRepository _gasStationRepository;
        private readonly IMTypeRepository _mTypeRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IGasStationGasTypeRepository _gasStationGasTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GasStationAPIController(
            IUnitOfWork unitOfWork,
            IGasStationRepository gasStationRepository,
            IMTypeRepository mTypeRepository,
            IDistrictRepository districtRepository,
            IGasStationGasTypeRepository gasStationGasTypeRepository
        )
        {
            _gasStationRepository = gasStationRepository;
            _mTypeRepository = mTypeRepository;
            _districtRepository = districtRepository;
            _gasStationGasTypeRepository = gasStationGasTypeRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<GasStationResul> GetAllGasStations([FromBody]Request request)
        {
            GasStationResul gasStationResul = new GasStationResul();
            List<GasStaionMV> gasStationMVs = new List<GasStaionMV>();
            List<GasStation> gasStations = await _gasStationRepository.GetAsyncAllGasStationWithQuery(request.GasName, request.District, request.GasTypes);
            gasStationResul.Count = gasStations.Count();
            List<GasStation> gasStationsResult = gasStations.Skip((request.Page - 1) * 10).Take(10).ToList();

            foreach (var gasStation in gasStationsResult)
            {
                GasStaionMV gasStationMV = new GasStaionMV();
                gasStationMV.GasStationId = gasStation.GasStationId;
                gasStationMV.GasStationName = gasStation.GasStationName;
                gasStationMV.Address = gasStation.Address;
                gasStationMV.District = _districtRepository.FindById(gasStation.District);
                gasStationMV.OpeningTime = gasStation.OpeningTime;
                gasStationMV.Longitude = gasStation.Longitude;
                gasStationMV.Latitude = gasStation.Latitude;
                Rating rating = new Rating();
                rating.Code = gasStation.Rating;
                rating.Name = _mTypeRepository.getTypeText(gasStation.Rating, 4);
                gasStationMV.Rating = rating;
                List<ModelsViews.Type> types = new List<ModelsViews.Type>();
                foreach (var gastype in gasStation.GasStationGasType)
                {
                    ModelsViews.Type type = new ModelsViews.Type();
                    type.GasTypeCode = gastype.GasType;
                    type.GasTypeName = _mTypeRepository.getTypeText(gastype.GasType, 3);
                    types.Add(type);
                }
                gasStationMV.Types = types;
                gasStationMVs.Add(gasStationMV);
            }

            gasStationResul.GasStaionMVs = gasStationMVs;

            return gasStationResul;
        }

        [HttpPost]
        public AddGasStationResult AddGasStation([FromBody]GasStaionMV gasStationMV)
        {
            AddGasStationResult addGasStationResult = new AddGasStationResult();
            List<Error> errors = new List<Error>();
            if ( !ModelState.IsValid || !_gasStationRepository.CheckIsNotExistName(gasStationMV.GasStationName))
            {
                if (!_gasStationRepository.CheckIsNotExistName(gasStationMV.GasStationName))
                {
                    errors.Add(new Error("E0003", string.Format(Resources.Resource.E0003,gasStationMV.GasStationName)));                    
                }
                addGasStationResult.Errors = errors;
                addGasStationResult.Status = false;
                return addGasStationResult;
            }
            GasStation gasStation = new GasStation();
            try {
                gasStation.GasStationName = gasStationMV.GasStationName;
                gasStation.District = gasStationMV.District.DistrictId;
                gasStation.OpeningTime = gasStationMV.OpeningTime;
                gasStation.Address = gasStationMV.Address;
                gasStation.Longitude = gasStationMV.Longitude;
                gasStation.Latitude = gasStationMV.Latitude;
                gasStation.Rating = gasStationMV.Rating.Code;
                gasStation.InsertedAt = DateTime.Now;
                gasStation.InsertedBy = Convert.ToInt64(HttpContext.Session.GetString("userId"));
                gasStation.UpdatedAt = DateTime.Now;
                gasStation.UpdatedBy = Convert.ToInt64(HttpContext.Session.GetString("userId"));
                _gasStationRepository.Insert(gasStation);
                _unitOfWork.Commit();
                try {
                    foreach (var item in gasStationMV.Types)
                    {
                        GasStationGasType gasStationGasType = new GasStationGasType();
                        gasStationGasType.GasStationId = gasStation.GasStationId;
                        gasStationGasType.GasType = item.GasTypeCode;
                        _gasStationGasTypeRepository.Insert(gasStationGasType);
                        _unitOfWork.Commit();
                    }
                }
                catch {
                    addGasStationResult.Status = false;
                }
            }
            catch {
                ModelState.AddModelError("", "Dữ liệu không hợp lệ, vui lòng kiểm tra lại");
                addGasStationResult.Status = false;
                return addGasStationResult;
            }
            addGasStationResult.GasStation = gasStation;
            addGasStationResult.Status = true;
            return addGasStationResult;
        }

    }

     public class GasStationResul
    {
        public int Count { get; set; }
        public List<GasStaionMV> GasStaionMVs { get; set; }
    }

    public class AddGasStationResult
    {
        public bool Status { get; set; }
        public List<Error> Errors { get; set; }
        public GasStation GasStation { get; set; }

    }

    public class Request
    {
        public string GasName { get; set; }
        public int Page { get; set; }
        public long District { get; set; }
        public string[] GasTypes { get; set; }
    }

}