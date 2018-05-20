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
    public class ScholarshipController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScholarshipController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Scholarship
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Scholarship.Include(s => s.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Scholarship/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scholarship = await _context.Scholarship
                .Include(s => s.Student)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (scholarship == null)
            {
                return NotFound();
            }

            return View(scholarship);
        }

        // GET: Scholarship/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.StudentViewModel, "id", "FullName");
            return View();
        }

        // POST: Scholarship/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,StudentId")] Scholarship scholarship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scholarship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.StudentViewModel, "id", "FullName", scholarship.StudentId);
            return View(scholarship);
        }

        // GET: Scholarship/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scholarship = await _context.Scholarship.SingleOrDefaultAsync(m => m.Id == id);
            if (scholarship == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.StudentViewModel, "id", "FullName", scholarship.StudentId);
            return View(scholarship);
        }

        // POST: Scholarship/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,StudentId")] Scholarship scholarship)
        {
            if (id != scholarship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scholarship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScholarshipExists(scholarship.Id))
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
            ViewData["StudentId"] = new SelectList(_context.StudentViewModel, "id", "FullName", scholarship.StudentId);
            return View(scholarship);
        }

        // GET: Scholarship/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scholarship = await _context.Scholarship
                .Include(s => s.Student)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (scholarship == null)
            {
                return NotFound();
            }

            return View(scholarship);
        }

        // POST: Scholarship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scholarship = await _context.Scholarship.SingleOrDefaultAsync(m => m.Id == id);
            _context.Scholarship.Remove(scholarship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScholarshipExists(int id)
        {
            return _context.Scholarship.Any(e => e.Id == id);
        }
    }
}
