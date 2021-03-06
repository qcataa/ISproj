﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISproj.Data;
using ISproj.Models;
using Microsoft.AspNetCore.Identity;
using ISproj.Models.AccountViewModels;
using ISproj.Models.CreateViewModels;
using ISproj.Services;

namespace ISproj.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPDFWritter _pdfManager;

        public StudentController(
            IDbContext context, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IPDFWritter pdfManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _pdfManager = pdfManager;
        }

        public ICollection<Student> DoIndex()
        {
            return _context.StudentViewModel.ToList();
        }

        // GET: StudentViewModels
        public async Task<IActionResult> Index()
        {
            return View(DoIndex());
        }

        public Student DoDetails(int? id)
        {
            return _context.StudentViewModel
                .SingleOrDefault(m => m.id == id);
        }

        // GET: StudentViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentViewModel = DoDetails(id);
            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        // GET: StudentViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("id,Name,Address,Surname,Birthdate,CNP,Email")] Student student,
            [Bind("Email,Password,ConfirmPassword")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Student");
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            StudentCreateViewModel studentCreateViewModel = new StudentCreateViewModel
            {
                Name=student.Name,
                Surname=student.Surname,
                Address=student.Address,
                CNP=student.CNP,
                Email=student.Email
            };

            return View(studentCreateViewModel);
        }

        // GET: StudentViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentViewModel = await _context.StudentViewModel.SingleOrDefaultAsync(m => m.id == id);
            if (studentViewModel == null)
            {
                return NotFound();
            }
            return View(studentViewModel);
        }

        // POST: StudentViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Address,Surname,Birthdate,CNP")] Student studentViewModel)
        {
            if (id != studentViewModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentViewModelExists(studentViewModel.id))
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
            return View(studentViewModel);
        }

        public async Task<IActionResult> TimeTable()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));

            var email = user.Email;

            var courses = await _context.CourseAttendant
                .Include(attendant => attendant.Student)
                .Where(attendant => attendant.Student.Email == email)
                .Include(attendant => attendant.Course)
                .Select(attendant => attendant.Course)
                .ToListAsync();

            String[] days = { "Luni", "Marti", "Miercuri", "Joi", "Vineri" };

            courses.Sort((course1, course2) =>
            {
                var firstDay = Array.IndexOf(days, course1.Day);
                var secondDay = Array.IndexOf(days, course2.Day);

                if (firstDay != secondDay)
                {
                    return firstDay - secondDay;
                }

                if (course1.Hour != course2.Hour)
                {
                    return course1.Hour - course2.Hour;
                }

                return course1.Duration - course2.Duration;
            });

            return View(courses);
        }

        // GET: StudentViewModels/Edit/5
        public async Task<IActionResult> Report(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendances = await _context.CourseAttendant
                .Where(attendant => attendant.StudentId == id)
                .Include(attendant => attendant.Course)
                .ToListAsync();

            return View(attendances);
        }

        // GET: StudentViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentViewModel = await _context.StudentViewModel
                .SingleOrDefaultAsync(m => m.id == id);
            if (studentViewModel == null)
            {
                return NotFound();
            }

            return View(studentViewModel);
        }

        // POST: StudentViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.StudentViewModel.SingleOrDefaultAsync(m => m.id == id);
            _context.StudentViewModel.Remove(student);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByEmailAsync(student.Email);
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        private bool StudentViewModelExists(int id)
        {
            return _context.StudentViewModel.Any(e => e.id == id);
        }

        [HttpGet, ActionName("GetStudentGrades")]
        public async Task<IActionResult> GenerateGradesPdf()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));

            var email = user.Email;

            var grades = await _context.CourseAttendant
                .Include(attendant => attendant.Student)
                .Where(attendant => attendant.Student.Email == email)
                .Include(attendant => attendant.Course)
                .Select(attendant => attendant.Course)
                .ToListAsync();

            _pdfManager.CreateGradesPDF(grades);
            return View("Index");
        }

        public async Task<IActionResult> Grades()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));

            var email = user.Email;

            var grades = await _context.CourseAttendant
                .Include(attendant => attendant.Student)
                .Where(attendant => attendant.Student.Email == email)
                .Include(attendant => attendant.Course)
                .Select(attendant => attendant.Course)
                .ToListAsync();

            String[] days = { "Luni", "Marti", "Miercuri", "Joi", "Vineri" };

            grades.Sort((course1, course2) =>
            {
                var firstDay = Array.IndexOf(days, course1.Day);
                var secondDay = Array.IndexOf(days, course2.Day);

                if (firstDay != secondDay)
                {
                    return firstDay - secondDay;
                }

                if (course1.Hour != course2.Hour)
                {
                    return course1.Hour - course2.Hour;
                }

                return course1.Duration - course2.Duration;
            });

            return View(grades);
        }

    }
}
