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
    public class ClassroomAvailabilitiesController : Controller
    {
        private readonly MyDbContext _context;

        public ClassroomAvailabilitiesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: ClassroomAvailabilities
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.ClassroomAvailabilities.Include(c => c.Classroom).Include(c => c.Slot);
            return View(await myDbContext.ToListAsync());
        }

        // GET: ClassroomAvailabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClassroomAvailabilities == null)
            {
                return NotFound();
            }

            var classroomAvailability = await _context.ClassroomAvailabilities
                .Include(c => c.Classroom)
                .Include(c => c.Slot)
                .FirstOrDefaultAsync(m => m.ClassroomAvailabilityId == id);
            if (classroomAvailability == null)
            {
                return NotFound();
            }

            return View(classroomAvailability);
        }

        // GET: ClassroomAvailabilities/Create
        public IActionResult Create()
        {
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "ClassroomId", "ClassroomId");
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId");
            return View();
        }

        // POST: ClassroomAvailabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassroomAvailabilityId,SlotId,ClassroomId,Isavailable")] ClassroomAvailability classroomAvailability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classroomAvailability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "ClassroomId", "ClassroomId", classroomAvailability.ClassroomId);
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", classroomAvailability.SlotId);
            return View(classroomAvailability);
        }

        // GET: ClassroomAvailabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClassroomAvailabilities == null)
            {
                return NotFound();
            }

            var classroomAvailability = await _context.ClassroomAvailabilities.FindAsync(id);
            if (classroomAvailability == null)
            {
                return NotFound();
            }
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "ClassroomId", "ClassroomId", classroomAvailability.ClassroomId);
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", classroomAvailability.SlotId);
            return View(classroomAvailability);
        }

        // POST: ClassroomAvailabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassroomAvailabilityId,SlotId,ClassroomId,Isavailable")] ClassroomAvailability classroomAvailability)
        {
            if (id != classroomAvailability.ClassroomAvailabilityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classroomAvailability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassroomAvailabilityExists(classroomAvailability.ClassroomAvailabilityId))
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
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "ClassroomId", "ClassroomId", classroomAvailability.ClassroomId);
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", classroomAvailability.SlotId);
            return View(classroomAvailability);
        }

        // GET: ClassroomAvailabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClassroomAvailabilities == null)
            {
                return NotFound();
            }

            var classroomAvailability = await _context.ClassroomAvailabilities
                .Include(c => c.Classroom)
                .Include(c => c.Slot)
                .FirstOrDefaultAsync(m => m.ClassroomAvailabilityId == id);
            if (classroomAvailability == null)
            {
                return NotFound();
            }

            return View(classroomAvailability);
        }

        // POST: ClassroomAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClassroomAvailabilities == null)
            {
                return Problem("Entity set 'MyDbContext.ClassroomAvailabilities'  is null.");
            }
            var classroomAvailability = await _context.ClassroomAvailabilities.FindAsync(id);
            if (classroomAvailability != null)
            {
                _context.ClassroomAvailabilities.Remove(classroomAvailability);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassroomAvailabilityExists(int id)
        {
          return (_context.ClassroomAvailabilities?.Any(e => e.ClassroomAvailabilityId == id)).GetValueOrDefault();
        }
    }
}
