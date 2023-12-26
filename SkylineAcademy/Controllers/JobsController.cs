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
    public class JobsController : Controller
    {
        private readonly MyDbContext _context;

        public JobsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Jobs
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Jobs.Include(j => j.Department);
            return View(await myDbContext.ToListAsync());
        }
 
        // GET: Jobs/Details/5
   
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.Department)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Dname");
            return View();
        }

        // POST: Jobs/Create
        [Authorize(Roles = "SuperAdmin,Admin")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,Jname,Jdescription,DepartmentId")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Dname", job.DepartmentId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Dname", job.DepartmentId);
            return View(job);
        }
 
        // POST: Jobs/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,Jname,Jdescription,DepartmentId")] Job job)
        {
            if (id != job.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.JobId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Dname", job.DepartmentId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.Department)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }
 
        // POST: Jobs/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jobs == null)
            {
                return Problem("Entity set 'MyDbContext.Jobs'  is null.");
            }
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
          return (_context.Jobs?.Any(e => e.JobId == id)).GetValueOrDefault();
        }
    }
}
