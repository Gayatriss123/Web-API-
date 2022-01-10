using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//This is a MVC controller which returns the view related to Authentication token

namespace CustomerServiceWebAPI.Controllers
{
    public class AuthenticationTokenController : Controller
    {
       
        public ActionResult RegisterCustomer()
        {
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult CustomerData()
        {
            return View();
        }
        public ActionResult CustomerNames()
        {
            return View();
        }
    }
}