using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationData.Models;
using GasStationData.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace GasStationWeb.Controllers
{
    public class GasStationController : Controller
    {
        private readonly IGasStationRepository _gasStationRepository;
        private readonly IMTypeRepository _mTypeRepository;
        private readonly IDistrictRepository _districtRepository;

        public GasStationController(IDistrictRepository districtRepository, IGasStationRepository gasStationRepository, IMTypeRepository mTypeRepository)
        {
            _gasStationRepository = gasStationRepository;
            _mTypeRepository = mTypeRepository;
            _districtRepository = districtRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.listDistrist = await _districtRepository.GetAllAsync();
            ViewBag.listGasType = _mTypeRepository.GetAll().Where(x => x.TypeType == 3).ToList();
            ViewBag.ratingList = _mTypeRepository.GetAll().Where(x => x.TypeType == 4).ToList();
            return View();
        }
    }
}