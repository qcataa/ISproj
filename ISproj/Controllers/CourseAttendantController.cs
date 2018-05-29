using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISproj.Data;
using ISproj.Models;

namespace ISproj.Controllers
{
    public class CourseAttendantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseAttendantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseAttendant
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CourseAttendant.Include(c => c.Course).Include(c => c.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CourseAttendant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAttendant = await _context.CourseAttendant
                .Include(c => c.Course)
                .Include(c => c.Student)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (courseAttendant == null)
            {
                return NotFound();
            }

            return View(courseAttendant);
        }

        // GET: CourseAttendant/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.CourseModel, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.StudentViewModel, "id", "FullName");
            return View();
        }

        // POST: CourseAttendant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,Grade,Attendances")] CourseAttendant courseAttendant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseAttendant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.CourseModel, "Id", "Name", courseAttendant.CourseId);
            ViewData["StudentId"] = new SelectList(_context.StudentViewModel, "id", "FullName", courseAttendant.StudentId);
            return View(courseAttendant);
        }

        // GET: CourseAttendant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAttendant = await _context.CourseAttendant.SingleOrDefaultAsync(m => m.Id == id);
            if (courseAttendant == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.CourseModel, "Id", "Name", courseAttendant.CourseId);
            ViewData["StudentId"] = new SelectList(_context.StudentViewModel, "id", "FullName", courseAttendant.StudentId);
            return View(courseAttendant);
        }

        // POST: CourseAttendant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,Grade,Attendances")] CourseAttendant courseAttendant)
        {
            if (id != courseAttendant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseAttendant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseAttendantExists(courseAttendant.Id))
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
            ViewData["CourseId"] = new SelectList(_context.CourseModel, "Id", "Name", courseAttendant.CourseId);
            ViewData["StudentId"] = new SelectList(_context.StudentViewModel, "id", "FullName", courseAttendant.StudentId);
            return View(courseAttendant);
        }

        // GET: CourseAttendant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseAttendant = await _context.CourseAttendant
                .Include(c => c.Course)
                .Include(c => c.Student)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (courseAttendant == null)
            {
                return NotFound();
            }

            return View(courseAttendant);
        }

        // POST: CourseAttendant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseAttendant = await _context.CourseAttendant.SingleOrDefaultAsync(m => m.Id == id);
            _context.CourseAttendant.Remove(courseAttendant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseAttendantExists(int id)
        {
            return _context.CourseAttendant.Any(e => e.Id == id);
        }
    }
}
