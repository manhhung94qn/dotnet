using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GasStationWeb.Models;
using Microsoft.EntityFrameworkCore;
using GasStationData.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;

namespace GasStationWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGasStationRepository _gasStationRepository;
        private readonly IMTypeRepository _mTypeRepository;
        private readonly IDistrictRepository _districtRepository;
        public HomeController(
            IGasStationRepository gasStationRepository,
            IMTypeRepository mTypeRepository,
            IDistrictRepository districtRepository
        )
        {
            _gasStationRepository = gasStationRepository;
            _mTypeRepository = mTypeRepository;
            _districtRepository = districtRepository;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.listGasType = await _mTypeRepository.GetAllAsync();

            ViewBag.listDistrist = await _districtRepository.GetAllAsync() ;

            List<GasStationData.Models.GasStation> gasStations = await _gasStationRepository.GetAllAsync();
            return View();
        }

    }
}
