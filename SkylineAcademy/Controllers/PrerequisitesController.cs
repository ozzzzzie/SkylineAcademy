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
        [Authorize]
        // GET: Prerequisites
        public async Task<IActionResult> Index()
        {
              return _context.Prerequisites != null ? 
                          View(await _context.Prerequisites.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.Prerequisites'  is null.");
        }
        [Authorize]
        // GET: Prerequisites/Details/5
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: Prerequisites/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: Prerequisites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: Prerequisites/Edit/5
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: Prerequisites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: Prerequisites/Delete/5
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: Prerequisites/Delete/5
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
