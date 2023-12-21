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
    public class EnrollementsController : Controller
    {
        private readonly MyDbContext _context;

        public EnrollementsController(MyDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Enrollements
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Enrollements.Include(e => e.Schedule).Include(e => e.Student);
            return View(await myDbContext.ToListAsync());
        }
        public ActionResult StuPrevEnrollements()
        {
            Student stu = _context.Students.Where(x => x.Semail == User.Identity.Name).FirstOrDefault();
            List<Enrollement> stuprevenr = _context.Enrollements.Where(x => x.StudentId == stu.StudentId).ToList();

            foreach (Enrollement enrollement in stuprevenr)
            {
                List<ClassSchedule> sched = _context.ClassSchedules.Where(x => x.ScheduleId == enrollement.ScheduleId).ToList();

                foreach (ClassSchedule i in sched)
                {
                    if (int.TryParse(i.TeacherId, out int teacherId))
                    {
                        List<Teacher> teacher = _context.Teachers.Where(x => x.TeacherId == teacherId).ToList();

                    }
                }
            }

            return View(stuprevenr);
        }

        [Authorize]
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: Enrollements/Create
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.ClassSchedules, "ScheduleId", "ScheduleId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: Enrollements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("EnrollementId,StudentId,ScheduleId,EnrollementDate")] Enrollement enrollement)
        //{
        //      var stuEnr = _context.ClassSchedules.Where(e => e.ScheduleId == enrollement.ScheduleId).FirstOrDefault();

        //        List<Prerequisite> crses = _context.Prerequisites.Where(e => e.CourseId.ToString() == stuEnr.CourseId.ToString()).ToList();

        //        foreach (Prerequisite i in crses)
        //        {
        //            var chk  = _context.Enrollements.Where(x=> x.StudentId == enrollement.StudentId && x.Schedule.CourseId == i.PrerequisiteId).Count();

        //            if (chk == 0)
        //            {
        //                ModelState.AddModelError("ScheduleId", "Student has not completed the course prerequisite.");
        //            }
        //        } 
        //    if (ModelState.IsValid)
        //    {
        //         _context.Add(enrollement);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ScheduleId"] = new SelectList(_context.ClassSchedules, "ScheduleId", "ScheduleId", enrollement.ScheduleId);
        //    ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollement.StudentId);
        //    return View(enrollement);
        //}

        public async Task<IActionResult> Create([Bind("EnrollementId,StudentId,ScheduleId,EnrollementDate")] Enrollement enrollement)
        {
            var stuEnr = await _context.ClassSchedules.FirstOrDefaultAsync(e => e.ScheduleId == enrollement.ScheduleId);
            var student = await _context.Students.FindAsync(enrollement.StudentId);

            if (stuEnr != null && student != null)
            {
                var course = await _context.Courses.FindAsync(Convert.ToInt32(stuEnr.CourseId));

                if (course.MajorId != student.MajorId)
                {
                    ModelState.AddModelError("ScheduleId", "Students cannot join courses that are not within their majors.");
                }
                else
                {
                    // Check if the student is already enrolled in a schedule with the same SlotId
                    var isEnrolled = await _context.Enrollements.AnyAsync(x => x.StudentId == enrollement.StudentId && x.Schedule.SlotId == stuEnr.SlotId);

                    if (isEnrolled)
                    {
                        ModelState.AddModelError("ScheduleId", "Student is already enrolled in a schedule at the same time.");
                    }
                    else
                    {
                        List<Prerequisite> crses = await _context.Prerequisites.Where(e => e.CourseId.ToString() == stuEnr.CourseId.ToString()).ToListAsync();

                        foreach (Prerequisite i in crses)
                        {
                            var chk = await _context.Enrollements.Where(x => x.StudentId == enrollement.StudentId && x.Schedule.CourseId == i.PrerequisiteId).CountAsync();

                            if (chk == 0)
                            {
                                ModelState.AddModelError("ScheduleId", "Student has not completed the necessary course prerequisite.");
                            }
                        }
                    }
                }
            }

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

        [Authorize(Roles = "SuperAdmin,Admin")]
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: Enrollements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollementId,StudentId,ScheduleId,EnrollementDate")] Enrollement enrollement)
        {
            var stuEnr = await _context.ClassSchedules.FirstOrDefaultAsync(e => e.ScheduleId == enrollement.ScheduleId);
            var student = await _context.Students.FindAsync(enrollement.StudentId);

            if (stuEnr != null && student != null)
            {
                var course = await _context.Courses.FindAsync(Convert.ToInt32(stuEnr.CourseId));

                if (course.MajorId != student.MajorId)
                {
                    ModelState.AddModelError("ScheduleId", "Students cannot join courses that are not within their majors.");
                }
                else
                {
                    // Check if the student is already enrolled in a schedule with the same SlotId
                    var isEnrolled = await _context.Enrollements.AnyAsync(x => x.StudentId == enrollement.StudentId && x.Schedule.SlotId == stuEnr.SlotId);

                    if (isEnrolled)
                    {
                        ModelState.AddModelError("ScheduleId", "Student is already enrolled in a schedule at the same time.");
                    }
                    else
                    {
                        List<Prerequisite> crses = await _context.Prerequisites.Where(e => e.CourseId.ToString() == stuEnr.CourseId.ToString()).ToListAsync();

                        foreach (Prerequisite i in crses)
                        {
                            var chk = await _context.Enrollements.Where(x => x.StudentId == enrollement.StudentId && x.Schedule.CourseId == i.PrerequisiteId).CountAsync();

                            if (chk == 0)
                            {
                                ModelState.AddModelError("ScheduleId", "Student has not completed the necessary course prerequisite.");
                            }
                        }
                    }
                }
            }
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
        [Authorize(Roles = "SuperAdmin,Admin")]
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
        [Authorize(Roles = "SuperAdmin,Admin")]
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
