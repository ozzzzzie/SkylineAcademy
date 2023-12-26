using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkylineAcademy.Models;

namespace SkylineAcademy.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MyDbContext _context;

        public StudentsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Students
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Students.Include(s => s.Major);
            return View(await myDbContext.ToListAsync());
        }
        // GET: Students/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Major)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Create()
        {
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "Mname");
            return View();
        }

        // POST: Students/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Sfname,Slname,Saddress,Sphonenumber,MajorId,Semail")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "Mname", student.MajorId);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "Mname", student.MajorId);
            return View(student);
        }
 
        // POST: Students/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,Sfname,Slname,Saddress,Sphonenumber,MajorId,Semail")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "Mname", student.MajorId);
            return View(student);
        }
 
        // GET: Students/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Major)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
       
        // POST: Students/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'MyDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
