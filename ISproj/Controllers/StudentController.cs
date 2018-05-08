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
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentViewModel.ToListAsync());
        }

        // GET: StudentViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("id,Name,Address,Surname,Birthdate,CNP")] Student studentViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
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
            var studentViewModel = await _context.StudentViewModel.SingleOrDefaultAsync(m => m.id == id);
            _context.StudentViewModel.Remove(studentViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentViewModelExists(int id)
        {
            return _context.StudentViewModel.Any(e => e.id == id);
        }
    }
}
