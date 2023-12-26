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
    public class TeachersController : Controller
    {
        private readonly MyDbContext _context;

        public TeachersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Teachers
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Teachers.Include(t => t.Faculty);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Teachers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Faculty)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Fname");
            return View();
        }


        // POST: Teachers/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherId,Tfname,Tlname,Taddress,Tphonenumber,Ishead,Email,FacultyId")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Fname", teacher.FacultyId);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Fname", teacher.FacultyId);
            return View(teacher);
        }
 
        // POST: Teachers/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherId,Tfname,Tlname,Taddress,Tphonenumber,Ishead,Email,FacultyId")] Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.TeacherId))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Fname", teacher.FacultyId);
            return View(teacher);
        }
 
        // GET: Teachers/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Faculty)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
 
        // POST: Teachers/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teachers == null)
            {
                return Problem("Entity set 'MyDbContext.Teachers'  is null.");
            }
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
          return (_context.Teachers?.Any(e => e.TeacherId == id)).GetValueOrDefault();
        }
    }
}
