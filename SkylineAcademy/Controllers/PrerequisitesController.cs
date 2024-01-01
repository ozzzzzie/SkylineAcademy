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
    public class PrerequisitesController : Controller
    {
        private readonly MyDbContext _context;

        public PrerequisitesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Prerequisites
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (_context.Prerequisites == null)
            {
                return Problem("Entity set 'MyDbContext.Prerequisites' is null.");
            }

            // LINQ query to join the Courses and Prerequisites
            var courseWithPrerequisites = await (from prerequisite in _context.Prerequisites
                                                 join course in _context.Courses on prerequisite.CourseId equals course.CourseId
                                                 join preCourse in _context.Courses on Convert.ToInt32(prerequisite.PrerequisiteId) equals preCourse.CourseId
                                                 select new
                                                 {
                                                     CourseId = course.CourseId,
                                                     CourseName = course.Cname,
                                                     PrerequisiteId = preCourse.CourseId,
                                                     PrerequisiteName = preCourse.Cname
                                                 }).ToListAsync();

            return View(courseWithPrerequisites);
        }


        // GET: Prerequisites/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prerequisites == null)
            {
                return NotFound();
            }

            var prerequisite = await _context.Prerequisites
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (prerequisite == null)
            {
                return NotFound();
            }

            return View(prerequisite);
        }
        // GET: Prerequisites/Create
        [Authorize(Roles = "SuperAdmin,Admin")]

        public IActionResult Create()
        {
            return View();
        }
        // POST: Prerequisites/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,PrerequisiteId")] Prerequisite prerequisite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prerequisite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prerequisite);
        }

        
        // GET: Prerequisites/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prerequisites == null)
            {
                return NotFound();
            }

            var prerequisite = await _context.Prerequisites.FindAsync(id);
            if (prerequisite == null)
            {
                return NotFound();
            }
            return View(prerequisite);
        }

        // POST: Prerequisites/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,PrerequisiteId")] Prerequisite prerequisite)
        {
            if (id != prerequisite.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prerequisite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrerequisiteExists(prerequisite.CourseId))
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
            return View(prerequisite);
        }
 
        // GET: Prerequisites/Delete/5
       [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prerequisites == null)
            {
                return NotFound();
            }

            var prerequisite = await _context.Prerequisites
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (prerequisite == null)
            {
                return NotFound();
            }

            return View(prerequisite);
        }

        // POST: Prerequisites/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prerequisites == null)
            {
                return Problem("Entity set 'MyDbContext.Prerequisites'  is null.");
            }
            var prerequisite = await _context.Prerequisites.FindAsync(id);
            if (prerequisite != null)
            {
                _context.Prerequisites.Remove(prerequisite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrerequisiteExists(int id)
        {
          return (_context.Prerequisites?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
