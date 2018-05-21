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
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TeacherController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        // GET: TeacherViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.TeacherViewModel.ToListAsync());
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
