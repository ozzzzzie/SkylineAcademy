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
    public class ClassSchedulesController : Controller
    {
        private readonly MyDbContext _context;

        public ClassSchedulesController(MyDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: ClassSchedules
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.ClassSchedules.Include(c => c.Slot);
            return View(await myDbContext.ToListAsync());
        }
        [Authorize]
        // GET: ClassSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClassSchedules == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedules
                .Include(c => c.Slot)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (classSchedule == null)
            {
                return NotFound();
            }

            return View(classSchedule);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: ClassSchedules/Create
        public IActionResult Create()
        {
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId");
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: ClassSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ScheduleId,CourseId,AdministratorId,TeacherId,ClassroomId,SlotId,Semester,Academicyear")] ClassSchedule classSchedule)
        //{
        //    var conflictingSchedule = await _context.ClassSchedules
        //        .FirstOrDefaultAsync(cs => cs.TeacherId == classSchedule.TeacherId &&
        //                                   cs.SlotId == classSchedule.SlotId &&
        //                                   cs.Academicyear == classSchedule.Academicyear &&
        //                                   cs.Semester == classSchedule.Semester);

        //    if (conflictingSchedule != null)
        //    {
        //        var errorMessage = "Teacher is already scheduled for a class at this time";

        //        if (conflictingSchedule.Academicyear == classSchedule.Academicyear)
        //            errorMessage += " in the same academic year";

        //        if (conflictingSchedule.Semester == classSchedule.Semester)
        //            errorMessage += " in the same semester";

        //        ModelState.AddModelError("TeacherId", errorMessage);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(classSchedule);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", classSchedule.SlotId);
        //    return View(classSchedule);
        //}

        public async Task<IActionResult> Create([Bind("ScheduleId,CourseId,AdministratorId,TeacherId,ClassroomId,SlotId,Semester,Academicyear")] ClassSchedule classSchedule)
        {
            var conflictingSchedule = await _context.ClassSchedules
                .FirstOrDefaultAsync(cs => cs.TeacherId == classSchedule.TeacherId &&
                                           cs.SlotId == classSchedule.SlotId &&
                                           cs.Academicyear == classSchedule.Academicyear &&
                                           cs.Semester == classSchedule.Semester);

            var conflictingClassroomSchedule = await _context.ClassSchedules
                .FirstOrDefaultAsync(cs => cs.ClassroomId == classSchedule.ClassroomId &&
                                           cs.SlotId == classSchedule.SlotId &&
                                           cs.Academicyear == classSchedule.Academicyear &&
                                           cs.Semester == classSchedule.Semester);

            if (conflictingSchedule != null)
            {
                var errorMessage = "Teacher is already scheduled for a class at this time";

                if (conflictingSchedule.Academicyear == classSchedule.Academicyear)
                    errorMessage += " in the same academic year";

                if (conflictingSchedule.Semester == classSchedule.Semester)
                    errorMessage += " in the same semester";

                ModelState.AddModelError("TeacherId", errorMessage);
            }

            if (conflictingClassroomSchedule != null)
            {
                var errorMessage = "Classroom is already scheduled for a class at this time";

                if (conflictingClassroomSchedule.Academicyear == classSchedule.Academicyear)
                    errorMessage += " in the same academic year";

                if (conflictingClassroomSchedule.Semester == classSchedule.Semester)
                    errorMessage += " in the same semester";

                ModelState.AddModelError("ClassroomId", errorMessage);
            }

            if (ModelState.IsValid)
            {
                _context.Add(classSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", classSchedule.SlotId);
            return View(classSchedule);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: ClassSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClassSchedules == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedules.FindAsync(id);
            if (classSchedule == null)
            {
                return NotFound();
            }
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", classSchedule.SlotId);
            return View(classSchedule);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: ClassSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,CourseId,AdministratorId,TeacherId,ClassroomId,SlotId,Semester,Academicyear")] ClassSchedule classSchedule)
        {
            if (id != classSchedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassScheduleExists(classSchedule.ScheduleId))
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
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", classSchedule.SlotId);
            return View(classSchedule);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: ClassSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClassSchedules == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedules
                .Include(c => c.Slot)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (classSchedule == null)
            {
                return NotFound();
            }

            return View(classSchedule);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: ClassSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClassSchedules == null)
            {
                return Problem("Entity set 'MyDbContext.ClassSchedules'  is null.");
            }
            var classSchedule = await _context.ClassSchedules.FindAsync(id);
            if (classSchedule != null)
            {
                _context.ClassSchedules.Remove(classSchedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassScheduleExists(int id)
        {
          return (_context.ClassSchedules?.Any(e => e.ScheduleId == id)).GetValueOrDefault();
        }
    }
}
