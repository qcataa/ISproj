using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISproj.Models;
using Microsoft.AspNetCore.Mvc;

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
