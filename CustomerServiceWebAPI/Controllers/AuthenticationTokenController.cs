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
       //This method returns Registration Form View
        public ActionResult RegisterCustomer()
        {
            return View();
        }
        //This method returns Login Form View
        public ActionResult Login()
        {
            return View();
        }
        //This method returns View which shows Customer Table Data on button click
        public ActionResult CustomerData()
        {
            return View();
        }
        //This method returns View which shows list of Customer Names on button click
        public ActionResult CustomerNames()
        {
            return View();
        }
    }
}