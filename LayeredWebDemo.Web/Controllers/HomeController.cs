using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LayeredWebDemo.BLL.Interfaces;

namespace LayeredWebDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;

        public HomeController(IUserService service)
        {
            _userService = service;
        }

        public ActionResult Index()
        {
            return View(_userService.GetUsers());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}