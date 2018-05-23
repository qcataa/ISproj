using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ISproj.Models;
using Microsoft.AspNetCore.Identity;

namespace ISproj.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (HttpContext.User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Faculty");
            }

            if (HttpContext.User.IsInRole("Teacher"))
            {
                return RedirectToAction("Timetable", "Teacher");
            }

            if (HttpContext.User.IsInRole("Student"))
            {
                return RedirectToAction("Timetable", "Student");
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
