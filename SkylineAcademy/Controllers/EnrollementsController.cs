using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Enrollements.Include(e => e.Schedule).Include(e => e.Student);
            return View(await myDbContext.ToListAsync());
        }

        //GET: Student Previous Enrollements
        [Authorize(Roles = "SuperAdmin,Admin,Student")]
        public ActionResult StuPrevEnrollements()
        {
            Student stu = _context.Students.FirstOrDefault(x => x.Semail == User.Identity.Name);
            if (stu == null)
            {
                // Handle the case where the student isn't found
                return View("Error");            }

            // Retrieve the enrollments and associated schedules, teachers, slots and courses for the student
            var enrollements = from enrollement in _context.Enrollements
                               join schedule in _context.ClassSchedules on enrollement.ScheduleId equals schedule.ScheduleId
                               join course in _context.Courses on Convert.ToInt32(schedule.CourseId) equals course.CourseId
                               join teacher in _context.Teachers on Convert.ToInt32(schedule.TeacherId) equals teacher.TeacherId
                               join slot in _context.Slots on schedule.SlotId equals slot.SlotId
                               where enrollement.StudentId == stu.StudentId
                               select new  
                               {
                                   Enrollment = enrollement.EnrollementId,
                                   Schedule = schedule.ScheduleId,
                                   CourseName = course.Cname,
                                   TeacherName = teacher.Tfname + " " + teacher.Tlname,
                                   SlotDay = slot.WeekdayName,
                                   SlotTiming = slot.StartTime + " - " + slot.EndTime,
                                   Semester = schedule.Semester,
                                   AcademicYear = schedule.Academicyear
                                   
                               };

               return View(enrollements.ToList());
        }


        //GET: Student Timetables

        [Authorize(Roles = "SuperAdmin,Admin,Student")]
        public IActionResult StudentTimetable(string academicYear, string semester)
        {
            // Get the current student's ID
            Student stu = _context.Students.FirstOrDefault(x => x.Semail == User.Identity.Name);

            if (stu == null)
            {
                // Handle the case where the student isn't found
                return View("Error"); 
            }

            // Query to join tables and select required data
            var query = from enrollement in _context.Enrollements
                        join schedule in _context.ClassSchedules on enrollement.ScheduleId equals schedule.ScheduleId
                        join course in _context.Courses on Convert.ToInt32(schedule.CourseId) equals course.CourseId
                        join teacher in _context.Teachers on Convert.ToInt32(schedule.TeacherId) equals teacher.TeacherId
                        join classroom in _context.Classrooms on Convert.ToInt32(schedule.ClassroomId) equals classroom.ClassroomId
                        where enrollement.StudentId == stu.StudentId &&
                              schedule.Academicyear == academicYear &&
                              schedule.Semester == semester
                        select new
                        {
                            CourseName = course.Cname,
                            TeacherName = teacher.Tfname + " " + teacher.Tlname,
                            ClassroomNumber = classroom.ClassroomId,
                            Slot = schedule.Slot.SlotId
                        };

            return View(query.ToList());
        }

        //GET: Teacher Timetables

        [Authorize(Roles = "SuperAdmin,Admin,Teacher")]
        public IActionResult TeacherTimetable(string academicYear, string semester)
        {
            // Get the current teacher's ID
            Teacher tch = _context.Teachers.FirstOrDefault(x => x.Email == User.Identity.Name);

            if (tch == null)
            {
                // Handle the case where the teacher isn't found
                return View("Error"); 
            }

            // Query to join tables and select required data
            var query = from enrollement in _context.Enrollements
                        join schedule in _context.ClassSchedules on enrollement.ScheduleId equals schedule.ScheduleId
                        join course in _context.Courses on Convert.ToInt32(schedule.CourseId) equals course.CourseId
                        join teacher in _context.Teachers on Convert.ToInt32(schedule.TeacherId) equals teacher.TeacherId
                        join classroom in _context.Classrooms on Convert.ToInt32(schedule.ClassroomId) equals classroom.ClassroomId
                        where Convert.ToInt32(enrollement.Schedule.TeacherId) == tch.TeacherId &&
                              schedule.Academicyear == academicYear &&
                              schedule.Semester == semester
                        select new
                        {
                            CourseName = course.Cname,
                            ClassroomNumber = classroom.ClassroomId,
                            Slot = schedule.Slot.SlotId
                        };

            return View(query.ToList());
        }

        //GET: Teacher's Previous Enrollements
        [Authorize(Roles = "SuperAdmin,Admin,Teacher")]

        public ActionResult TeachPrevEnrollements()
        {
            // Get the current teacher's ID
            Teacher tch = _context.Teachers.FirstOrDefault(x => x.Email == User.Identity.Name);

            if (tch == null)
            {
                // Handle the case where the teacher isn't found
                return View("Error");
            }

            // Query to join tables and select required data
            var query = from enrollement in _context.Enrollements
                        join schedule in _context.ClassSchedules on enrollement.ScheduleId equals schedule.ScheduleId
                        join course in _context.Courses on Convert.ToInt32(schedule.CourseId) equals course.CourseId  
                        where Convert.ToInt32(schedule.TeacherId) == tch.TeacherId  
                        select new
                        {
                            EnrollementId = enrollement.EnrollementId,
                            CourseName = course.Cname,
                            Semester = schedule.Semester,
                            AcademicYear = schedule.Academicyear,
                            TeacherId = tch.TeacherId  
                        };

            return View(query.ToList());
        }



        // GET: Enrollements/Details/5

        [Authorize]

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

        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.ClassSchedules, "ScheduleId", "ScheduleId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }
      

        // POST: Enrollements/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Admin")]
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



        // GET: Enrollements/Edit/5


        [Authorize(Roles = "SuperAdmin,Admin")]
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

        [Authorize(Roles = "SuperAdmin,Admin")]


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



        // GET: Enrollements/Delete/5


        [Authorize(Roles = "SuperAdmin,Admin")]

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

        [Authorize(Roles = "SuperAdmin,Admin")]

               
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
