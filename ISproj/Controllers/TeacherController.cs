using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISproj.Data;
using ISproj.Models;
using ISproj.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using ISproj.Models.CreateViewModels;

namespace ISproj.Controllers
{
    public class TeacherController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TeacherController(
            IDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public ICollection<Teacher> DoIndex()
        {
            return _context.TeacherViewModel.ToList();
        }

        // GET: TeacherViewModels
        public async Task<IActionResult> Index()
        {
            return View(DoIndex());
        }

        // GET: TeacherViewModels
        public async Task<IActionResult> Timetable()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));

            var email = user.Email;

            var courses = await _context.CourseModel
                .Include(course => course.Teacher)
                .Where(course => course.Teacher.Email == email)
                .ToListAsync();

            String[] days = { "Luni", "Marti", "Miercuri", "Joi", "Vineri" };

            courses.Sort((course1, course2) =>
            {
                var firstDay = Array.IndexOf(days, course1.Day);
                var secondDay = Array.IndexOf(days, course2.Day);

                if(firstDay != secondDay)
                {
                    return firstDay - secondDay;
                }

                if(course1.Hour != course2.Hour)
                {
                    return course1.Hour - course2.Hour;
                }

                return course1.Duration - course2.Duration;
            });

            return View(courses);
        }

        // GET: TeacherViewModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherViewModel = await _context.TeacherViewModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (teacherViewModel == null)
            {
                return NotFound();
            }

            return View(teacherViewModel);
        }

        // GET: TeacherViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeacherViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,FirstName,LastName,Email")] Teacher teacher,
            [Bind("Email,Password,ConfirmPassword")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Teacher");
                    _context.Add(teacher);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            TeacherCreateViewModel teacherCreateViewModel = new TeacherCreateViewModel
            {
                LastName = teacher.LastName,
                FirstName = teacher.FirstName,
                Email = teacher.Email,
            };

            return View(teacherCreateViewModel);
        }

        // GET: TeacherViewModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherViewModel = await _context.TeacherViewModel.SingleOrDefaultAsync(m => m.Id == id);
            if (teacherViewModel == null)
            {
                return NotFound();
            }
            return View(teacherViewModel);
        }

        // POST: TeacherViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName")] Teacher teacherViewModel)
        {
            if (id != teacherViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherViewModelExists(teacherViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacherViewModel);
        }

        // GET: TeacherViewModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherViewModel = await _context.TeacherViewModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (teacherViewModel == null)
            {
                return NotFound();
            }

            return View(teacherViewModel);
        }

        // POST: TeacherViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var teacher = await _context.TeacherViewModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.TeacherViewModel.Remove(teacher);
            await _context.SaveChangesAsync();
            
            var user = await _userManager.FindByEmailAsync(teacher.Email);
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        private bool TeacherViewModelExists(string id)
        {
            return _context.TeacherViewModel.Any(e => e.Id == id);
        }
    }
}
