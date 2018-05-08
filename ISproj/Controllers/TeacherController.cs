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
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
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
                .SingleOrDefaultAsync(m => m.CNP == id);
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
        public async Task<IActionResult> Create([Bind("CNP,FirstName,LastName")] Teacher teacherViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacherViewModel);
        }

        // GET: TeacherViewModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherViewModel = await _context.TeacherViewModel.SingleOrDefaultAsync(m => m.CNP == id);
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
        public async Task<IActionResult> Edit(string id, [Bind("CNP,FirstName,LastName")] Teacher teacherViewModel)
        {
            if (id != teacherViewModel.CNP)
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
                    if (!TeacherViewModelExists(teacherViewModel.CNP))
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
                .SingleOrDefaultAsync(m => m.CNP == id);
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
            var teacherViewModel = await _context.TeacherViewModel.SingleOrDefaultAsync(m => m.CNP == id);
            _context.TeacherViewModel.Remove(teacherViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherViewModelExists(string id)
        {
            return _context.TeacherViewModel.Any(e => e.CNP == id);
        }
    }
}
