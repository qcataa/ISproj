using ISproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISproj.Controllers
{

    public class LoginController : Controller
    {
        public ActionResult Index(LoginModel model)
        {
            return Login(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            return View("Login");
        }

    }
}