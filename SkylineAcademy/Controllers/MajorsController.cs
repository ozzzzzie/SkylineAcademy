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
    public class MajorsController : Controller
    {
        private readonly MyDbContext _context;

        public MajorsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Majors
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Majors.Include(m => m.Faculty);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Majors/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Majors == null)
            {
                return NotFound();
            }

            var major = await _context.Majors
                .Include(m => m.Faculty)
                .FirstOrDefaultAsync(m => m.MajorId == id);
            if (major == null)
            {
                return NotFound();
            }

            return View(major);
        }
        // GET: Majors/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Fname");
            return View();
        }

        // POST: Majors/Create
        [Authorize(Roles = "SuperAdmin,Admin")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MajorId,Mname,Mdescription,FacultyId")] Major major)
        {
            if (ModelState.IsValid)
            {
                _context.Add(major);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Fname", major.FacultyId);
            return View(major);
        }

        // GET: Majors/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Majors == null)
            {
                return NotFound();
            }

            var major = await _context.Majors.FindAsync(id);
            if (major == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Fname", major.FacultyId);
            return View(major);
        }

        // POST: Majors/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MajorId,Mname,Mdescription,FacultyId")] Major major)
        {
            if (id != major.MajorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(major);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MajorExists(major.MajorId))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "Fname", major.FacultyId);
            return View(major);
        }

        // GET: Majors/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Majors == null)
            {
                return NotFound();
            }

            var major = await _context.Majors
                .Include(m => m.Faculty)
                .FirstOrDefaultAsync(m => m.MajorId == id);
            if (major == null)
            {
                return NotFound();
            }

            return View(major);
        }
 
        // POST: Majors/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Majors == null)
            {
                return Problem("Entity set 'MyDbContext.Majors'  is null.");
            }
            var major = await _context.Majors.FindAsync(id);
            if (major != null)
            {
                _context.Majors.Remove(major);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MajorExists(int id)
        {
          return (_context.Majors?.Any(e => e.MajorId == id)).GetValueOrDefault();
        }
    }
}
