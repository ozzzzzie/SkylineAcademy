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
    public class FacultiesController : Controller
    {
        private readonly MyDbContext _context;

        public FacultiesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Faculties
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.Faculties != null ? 
                          View(await _context.Faculties.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.Faculties'  is null.");
        }

        // GET: Faculties/Details/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Faculties == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.FacultyId == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // GET: Faculties/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Create()
        {
            return View();
        }
 
        // POST: Faculties/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacultyId,Fname")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Faculties == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);
        }
  
        // POST: Faculties/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacultyId,Fname")] Faculty faculty)
        {
            if (id != faculty.FacultyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faculty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.FacultyId))
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
            return View(faculty);
        }
     
        // GET: Faculties/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Faculties == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.FacultyId == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }
        
        // POST: Faculties/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Faculties == null)
            {
                return Problem("Entity set 'MyDbContext.Faculties'  is null.");
            }
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyExists(int id)
        {
          return (_context.Faculties?.Any(e => e.FacultyId == id)).GetValueOrDefault();
        }
    }
}
