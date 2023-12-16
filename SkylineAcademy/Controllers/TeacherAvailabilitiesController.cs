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
    public class TeacherAvailabilitiesController : Controller
    {
        private readonly MyDbContext _context;

        public TeacherAvailabilitiesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: TeacherAvailabilities
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.TeacherAvailabilities.Include(t => t.Slot);
            return View(await myDbContext.ToListAsync());
        }

        // GET: TeacherAvailabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeacherAvailabilities == null)
            {
                return NotFound();
            }

            var teacherAvailability = await _context.TeacherAvailabilities
                .Include(t => t.Slot)
                .FirstOrDefaultAsync(m => m.TeacherAvailabilityId == id);
            if (teacherAvailability == null)
            {
                return NotFound();
            }

            return View(teacherAvailability);
        }

        // GET: TeacherAvailabilities/Create
        public IActionResult Create()
        {
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId");
            return View();
        }

        // POST: TeacherAvailabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherAvailabilityId,SlotId,TeacherId,Isavailable")] TeacherAvailability teacherAvailability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherAvailability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", teacherAvailability.SlotId);
            return View(teacherAvailability);
        }

        // GET: TeacherAvailabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeacherAvailabilities == null)
            {
                return NotFound();
            }

            var teacherAvailability = await _context.TeacherAvailabilities.FindAsync(id);
            if (teacherAvailability == null)
            {
                return NotFound();
            }
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", teacherAvailability.SlotId);
            return View(teacherAvailability);
        }

        // POST: TeacherAvailabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherAvailabilityId,SlotId,TeacherId,Isavailable")] TeacherAvailability teacherAvailability)
        {
            if (id != teacherAvailability.TeacherAvailabilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherAvailability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherAvailabilityExists(teacherAvailability.TeacherAvailabilityId))
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
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", teacherAvailability.SlotId);
            return View(teacherAvailability);
        }

        // GET: TeacherAvailabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeacherAvailabilities == null)
            {
                return NotFound();
            }

            var teacherAvailability = await _context.TeacherAvailabilities
                .Include(t => t.Slot)
                .FirstOrDefaultAsync(m => m.TeacherAvailabilityId == id);
            if (teacherAvailability == null)
            {
                return NotFound();
            }

            return View(teacherAvailability);
        }

        // POST: TeacherAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeacherAvailabilities == null)
            {
                return Problem("Entity set 'MyDbContext.TeacherAvailabilities'  is null.");
            }
            var teacherAvailability = await _context.TeacherAvailabilities.FindAsync(id);
            if (teacherAvailability != null)
            {
                _context.TeacherAvailabilities.Remove(teacherAvailability);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherAvailabilityExists(int id)
        {
          return (_context.TeacherAvailabilities?.Any(e => e.TeacherAvailabilityId == id)).GetValueOrDefault();
        }
    }
}
