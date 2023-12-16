using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkylineAcademy.Models;

namespace SkylineAcademy.Controllers
{
    public class EnrollementsController : Controller
    {
        private readonly MyDbContext _context;

        public EnrollementsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Enrollements
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Enrollements.Include(e => e.Schedule).Include(e => e.Student);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Enrollements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enrollements == null)
            {
                return NotFound();
            }

            var enrollement = await _context.Enrollements
                .Include(e => e.Schedule)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollementId == id);
            if (enrollement == null)
            {
                return NotFound();
            }

            return View(enrollement);
        }

        // GET: Enrollements/Create
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.ClassSchedules, "ScheduleId", "ScheduleId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: Enrollements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollementId,StudentId,ScheduleId,EnrollementDate")] Enrollement enrollement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_context.ClassSchedules, "ScheduleId", "ScheduleId", enrollement.ScheduleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollement.StudentId);
            return View(enrollement);
        }

        // GET: Enrollements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrollements == null)
            {
                return NotFound();
            }

            var enrollement = await _context.Enrollements.FindAsync(id);
            if (enrollement == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.ClassSchedules, "ScheduleId", "ScheduleId", enrollement.ScheduleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollement.StudentId);
            return View(enrollement);
        }

        // POST: Enrollements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollementId,StudentId,ScheduleId,EnrollementDate")] Enrollement enrollement)
        {
            if (id != enrollement.EnrollementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollementExists(enrollement.EnrollementId))
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
            ViewData["ScheduleId"] = new SelectList(_context.ClassSchedules, "ScheduleId", "ScheduleId", enrollement.ScheduleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollement.StudentId);
            return View(enrollement);
        }

        // GET: Enrollements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrollements == null)
            {
                return NotFound();
            }

            var enrollement = await _context.Enrollements
                .Include(e => e.Schedule)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollementId == id);
            if (enrollement == null)
            {
                return NotFound();
            }

            return View(enrollement);
        }

        // POST: Enrollements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrollements == null)
            {
                return Problem("Entity set 'MyDbContext.Enrollements'  is null.");
            }
            var enrollement = await _context.Enrollements.FindAsync(id);
            if (enrollement != null)
            {
                _context.Enrollements.Remove(enrollement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollementExists(int id)
        {
          return (_context.Enrollements?.Any(e => e.EnrollementId == id)).GetValueOrDefault();
        }
    }
}
