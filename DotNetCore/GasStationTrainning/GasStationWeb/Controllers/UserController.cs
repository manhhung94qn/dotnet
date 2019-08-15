using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationData.Models;
using GasStationData.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace GasStationWeb.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]

        public IActionResult Index()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public IActionResult Index([FromForm] User _user)
        {
            if (ModelState.IsValid)
            {
                User user = _userRepository.CheckIsAdmin(_user.Email, _user.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                    return View(_user);
                }
                HttpContext.Session.SetString("userId", user.UserId.ToString());
            return Redirect("Home/index");
            }
            else
            {
                ModelState.AddModelError("","Vui long kiem tra lai");
                return View(_user);
            }
        }
    }
}