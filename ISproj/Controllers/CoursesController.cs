using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISproj.Data;
using ISproj.Models;
using ISproj.Models.CoursesViewModels;

namespace ISproj.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseModel.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModel = await _context.CourseModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (courseModel == null)
            {
                return NotFound();
            }

            return View(courseModel);
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            var teachers = await _context.TeacherViewModel.ToListAsync();

            CreateViewModel model = new CreateViewModel {};
            model.Teachers = teachers;

            return View(model);
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Credits")] CourseModel courseModel, String TeacherCNP)
        {
            var teacherModel = await _context.TeacherViewModel
                .SingleOrDefaultAsync(m => m.LastName + " " + m.FirstName == TeacherCNP);

            if(teacherModel == null)
            {
                return NotFound();
            }

            courseModel.Teacher = teacherModel;

            if (ModelState.IsValid)
            {
                teacherModel.Courses.Append(courseModel);
                _context.Add(courseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseModel);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModel = await _context.CourseModel.SingleOrDefaultAsync(m => m.Id == id);
            if (courseModel == null)
            {
                return NotFound();
            }
            return View(courseModel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Credits")] CourseModel courseModel)
        {
            if (id != courseModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseModelExists(courseModel.Id))
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
            return View(courseModel);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModel = await _context.CourseModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (courseModel == null)
            {
                return NotFound();
            }

            return View(courseModel);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseModel = await _context.CourseModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.CourseModel.Remove(courseModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseModelExists(int id)
        {
            return _context.CourseModel.Any(e => e.Id == id);
        }
    }
}
