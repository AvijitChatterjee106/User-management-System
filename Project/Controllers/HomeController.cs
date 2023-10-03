using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Project.DataLayer;
using Microsoft.AspNetCore.Identity;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Registration()
        {
            return View();
        }
        public void Registration1(UserClass uc)
        {
            UserReg ur = new UserReg();
            ur.Reg(uc);
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Login1(UserClass user)
        {
            UserClass uc = new UserClass();
            uc = UserReg.userLogin(user);
            if (uc == null)
            {
                uc.status = 10;
            }
            else if (uc.password == user.password)
            {
                HttpContext.Session.SetString("email", uc.email);
                HttpContext.Session.SetString("password", uc.password);
                HttpContext.Session.SetString("UserId", uc.UserId);

                uc.status = 200;
                uc.password = "";
            }
            else if (uc.password != user.password)
            {
                uc.status = 20;
            }
            else
            {
                uc.status = 21;
            }
            return Ok(uc);
        }

        public IActionResult UserDetails()
        {
            
            return View();
        }
        public JsonResult UserDetails1()
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null)
            {
                return Json(UserReg.userdetails());
            }
            else
            {
                return Json(null);
            }
        }
        public JsonResult Product()
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null)
            {
                return Json(UserReg.AllProduct());
            }
            else
            {
                return Json(null);
            }
        }

        public IActionResult InsertProduct()
        {
            return View();
        }
        public void InsProd(ProductClass pc)
        {
            string ui = HttpContext.Session.GetString("UserId");
            UserReg.insertProduct(pc,ui);
        }
        public IActionResult ProductList()
        {
            return View();
        }
    }
}