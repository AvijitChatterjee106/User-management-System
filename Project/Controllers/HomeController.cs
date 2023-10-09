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
        public IActionResult Registration1(UserClass uc)
        {
            try
            {
                UserReg ur = new UserReg();
                ur.Reg(uc);
                return Ok();
            }catch (Exception ex)
            {
                // SQL exception occurred, extract the error message
                string errorMessage = ex.Message;
                return StatusCode(500, errorMessage); // Send a 500 Internal Server Error with the error message
            }
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null)
            {
                HttpContext.Session.Remove("email");
                HttpContext.Session.Remove("password");
                return View();
            }
            else
            {
                return View();
            }
        }
        public IActionResult Login1(UserClass user)
        {
            try
            {

                UserClass uc = new UserClass();
                uc = UserReg.userLogin(user);

                if (uc.email != user.email)
                {
                    uc.status = 10;
                }
                else if (uc.password == user.password && uc.email == user.email && uc.isActive == 1)
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
                else if (uc.isActive == 0 && uc.email != null && uc.password != null)
                {
                    uc.status = 21;
                }

                return Ok(uc);
            }
            catch(Exception ex)
            {
                string errorMessage = ex.Message;
                return StatusCode(500, errorMessage); // Send a 500 Internal Server Error with the error message

            }
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
               // return RedirectToAction("Login", "UserLogin");
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
        public IActionResult InsProd(ProductClass pc)
        {
            try
            {
                string ui = HttpContext.Session.GetString("UserId");
                UserReg.insertProduct(pc, ui);
                return Ok();
            }
            catch (SqlException ex)
            {
                // SQL exception occurred, extract the error message
                string errorMessage = ex.Message;
                return StatusCode(500, errorMessage); // Send a 500 Internal Server Error with the error message
            }
        }

        public IActionResult ProductList()
        {
            return View();

        }
        public JsonResult ProductList1()
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null)
            {
                return Json(UserReg.AllProductlist());
            }
            else
            {
                return Json(null);
            }
            
        }
		public IActionResult delProductList1()
		{
			if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null)
			{
				try
				{
					var productList = UserReg.deProductlist1();
					return Json(productList);
				}
				catch (Exception ex)
				{
					Console.WriteLine("An error occurred while fetching the product list: " + ex.Message);
					return StatusCode(500);
				}
			}
			else
			{
				return Json(null);
			}
		}
		public IActionResult delProductList()
        {
			if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null)
			{
				try
				{
					var productList = UserReg.deProductlist();
					return Json(productList);
				}
				catch (Exception ex)
				{
					Console.WriteLine("An error occurred while fetching the product list: " + ex.Message);
					return StatusCode(500); 
				}
			}
			else
			{
				return Json(null);
			}
		}
        public IActionResult UpdateProductIsApprove1()
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null)
            {
                try
                {
                    string ui = HttpContext.Session.GetString("UserId");
                    var productList = UserReg.apprProductlist(ui);
                    return Json(productList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while fetching the product list: " + ex.Message);
                    return StatusCode(500);
                }
            }
            else
            {
                return Json(null);
            }
        }
        public IActionResult UpdateProductIsReject1()
        {
            if (HttpContext.Session.GetString("email") != null && HttpContext.Session.GetString("password") != null)
            {
                try
                {
                    var productList = UserReg.rejProductlist();
                    return Json(productList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while fetching the product list: " + ex.Message);
                    return StatusCode(500);
                }
            }
            else
            {
                return Json(null);
            }
        }

        public IActionResult UpdateProductIsDelete(string pname)
        {
            try
            {
                string ui = HttpContext.Session.GetString("UserId");
                ProductClass pc = new ProductClass();
                pc = UserReg.upIsdelete(pname, ui);
                return Ok(pc);
            }
            catch(Exception ex)
            {
                string errorMessage = ex.Message;
                return StatusCode(500, errorMessage); // Send a 500 Internal Server Error with the error message
            }
        }
        public IActionResult UpdateProductdoActive(string pname)
        {
            try
            {
                string ui = HttpContext.Session.GetString("UserId");
            UserReg.doActive(pname, ui);
                // return View();
                return Ok();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return StatusCode(500, errorMessage); // Send a 500 Internal Server Error with the error message
            }
        }

        public JsonResult UpdateProductIsApprove(string pname)
        {
            string ui = HttpContext.Session.GetString("UserId");
            ProductClass pc = new ProductClass();
            pc = UserReg.upIsapprove(pname, ui);
            return Json(pc);
        }
        public JsonResult UpdateProductIsReject(string pname)
        {
            string ui = HttpContext.Session.GetString("UserId");
            ProductClass pc = new ProductClass();
            pc = UserReg.upIsreject(pname, ui);
            return Json(pc);
        }
       

      
        public void UpdateuserdoApprove(string email)
        {
            string ui = HttpContext.Session.GetString("UserId");
            UserReg.doApprove(email, ui);
        }
        
        public void UpdateuserdoInActive(string email)
        {
            string ui = HttpContext.Session.GetString("UserId");
            UserReg.doInActive(email, ui);
        }
        public void UpdateuserdoActive(string email)
        {
            string ui = HttpContext.Session.GetString("UserId");
            UserReg.douserActive(email, ui);
        }
    }
}