﻿using System;
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

        // GET: ClassSchedules
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.ClassSchedules.Include(c => c.Slot);
            return View(await myDbContext.ToListAsync());
        }

        // GET: ClassSchedules/Details/5
        [Authorize]
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
        // GET: ClassSchedules/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Create()
        {
            // Retrieve the list of courses 
            var courses = _context.Courses.ToList();

            // Retrieve the list of teachers 
            var teachers = _context.Teachers.ToList();

            // Retrieve the list of classrooms 
            var classrooms = _context.Classrooms.ToList();

            // Pass the lists to the view using the ViewBag
            ViewBag.Teachers = new SelectList(teachers.Select(t => new {
                TeacherId = t.TeacherId,
                FullName = t.Tfname + " " + t.Tlname
            }), "TeacherId", "FullName");
            ViewBag.Classrooms = new SelectList(classrooms, "ClassroomId", "ClassroomId");
            ViewBag.Courses = new SelectList(courses, "CourseId", "Cname");

            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId");

            return View();
        }
        // POST: ClassSchedules/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,CourseId,AdministratorId,TeacherId,ClassroomId,SlotId,Semester,Academicyear")] ClassSchedule classSchedule)
        {
            // Check if the teacher is teaching a course from their faculty
            var teacher = await _context.Teachers
                .Include(t => t.Faculty)
                .FirstOrDefaultAsync(t => t.TeacherId == Convert.ToInt32(classSchedule.TeacherId));

            var courseId = classSchedule.CourseId;
            var course = await _context.Courses
                .Include(c => c.Major)
                .FirstOrDefaultAsync(c => c.CourseId == Convert.ToInt32(courseId));

            if (teacher != null && course != null && teacher.FacultyId != course.Major.FacultyId)
            {
                ModelState.AddModelError("TeacherId", "Teacher can only teach courses from their faculty");
            }
            else
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

        // GET: ClassSchedules/Edit/5
        
        [Authorize(Roles = "SuperAdmin,Admin")]
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
            // Retrieve the list of courses 
            var courses = _context.Courses.ToList();
            // Retrieve the list of teachers 
            var teachers = _context.Teachers.ToList();

            // Retrieve the list of classrooms 
            var classrooms = _context.Classrooms.ToList();

            // Pass the lists to the view using the ViewBag
            ViewBag.Teachers = new SelectList(teachers.Select(t => new {
                TeacherId = t.TeacherId,
                FullName = t.Tfname + " " + t.Tlname
            }), "TeacherId", "FullName");
            ViewBag.Classrooms = new SelectList(classrooms, "ClassroomId", "ClassroomId");
            ViewBag.Courses = new SelectList(courses, "CourseId", "Cname");
            ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "SlotId", classSchedule.SlotId);
            return View(classSchedule);
        }
        // POST: ClassSchedules/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,CourseId,AdministratorId,TeacherId,ClassroomId,SlotId,Semester,Academicyear")] ClassSchedule classSchedule)
        {
            // Check if the teacher is teaching a course from their faculty
            var teacher = await _context.Teachers
                .Include(t => t.Faculty)
                .FirstOrDefaultAsync(t => t.TeacherId == Convert.ToInt32(classSchedule.TeacherId));

            var courseId = classSchedule.CourseId;
            var course = await _context.Courses
                .Include(c => c.Major)
                .FirstOrDefaultAsync(c => c.CourseId == Convert.ToInt32(courseId));

            if (teacher != null && course != null && teacher.FacultyId != course.Major.FacultyId)
            {
                ModelState.AddModelError("TeacherId", "Teachers can only teach courses from their respective faculty.");
            }
            else
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
                    var errorMessage = "Teacher is already scheduled for a class during this time";

                    if (conflictingSchedule.Academicyear == classSchedule.Academicyear)
                        errorMessage += " in the same academic year";

                    if (conflictingSchedule.Semester == classSchedule.Semester)
                        errorMessage += " in the same semester";

                    ModelState.AddModelError("TeacherId", errorMessage);
                }

                if (conflictingClassroomSchedule != null)
                {
                    var errorMessage = "Classroom is already scheduled for a class during this time";

                    if (conflictingClassroomSchedule.Academicyear == classSchedule.Academicyear)
                        errorMessage += " in the same academic year";

                    if (conflictingClassroomSchedule.Semester == classSchedule.Semester)
                        errorMessage += " in the same semester";

                    ModelState.AddModelError("ClassroomId", errorMessage);
                }
            }

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

        // GET: ClassSchedules/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
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
        // POST: ClassSchedules/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
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
