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
    public class GradesController : Controller
    {
        private readonly MyDbContext _context;

        public GradesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Grades
        public async Task<IActionResult> Index()
        {
            return _context.Grades != null ?
                        View(await _context.Grades.ToListAsync()) :
                        Problem("Entity set 'MyDbContext.Grades'  is null.");
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grades/Create
        //public IActionResult Create()
        //{
        //    var enrollements = _context.Enrollements.ToList();

        //    ViewBag.Enrollements = new SelectList(enrollements, "EnrollementId", "EnrollementId");

        //    return View();
        //}
        public IActionResult Create()
        {
            // Get the current teacher's ID
            Teacher tch = _context.Teachers.FirstOrDefault(x => x.Email == User.Identity.Name);
            if (tch == null)
            {
                // Handle the case where the teacher isn't found
                return View("Error");
            }

            var enrollments = _context.Enrollements
                .Where(e => _context.ClassSchedules.Any(cs => cs.ScheduleId == e.ScheduleId && Convert.ToInt32(cs.TeacherId) == tch.TeacherId))
                .Select(e => new
                {
                    EnrollementId = e.EnrollementId,
                    Description = $"ID: {e.EnrollementId} - Student: {_context.Students.FirstOrDefault(s => s.StudentId == e.StudentId).Sfname} - Course: {_context.Courses.FirstOrDefault(c => c.CourseId == Convert.ToInt32(e.Schedule.CourseId)).Cname}"
                })
                .ToList();

            ViewBag.Enrollments = new SelectList(enrollments, "EnrollementId", "Description");

            return View();
        }

        //public ActionResult TeacherStudentsGrades()
        //{
        //    Teacher techr = _context.Teachers.FirstOrDefault(x => x.Email == User.Identity.Name);

        //    List<ClassSchedule> sched = _context.ClassSchedules
        //        .Where(x => x.TeacherId == techr.TeacherId.ToString())
        //        .ToList();

        //    List<Grade> studentsGrades = new List<Grade>();

        //    foreach (ClassSchedule schedule in sched)
        //    {
        //        List<Enrollement> enrollements = _context.Enrollements
        //            .Where(x => x.ScheduleId == schedule.ScheduleId)
        //            .ToList();

        //        foreach (Enrollement enrollement in enrollements)
        //        {
        //            Grade grade = _context.Grades.FirstOrDefault(x => x.EnrollementId == enrollement.EnrollementId);
        //            if (grade != null)
        //            {
        //                studentsGrades.Add(grade);
        //            }
        //        }
        //    }

        //    return View(studentsGrades);
        //}

        public ActionResult TeacherStudentsGrades()
        {
            Teacher techr = _context.Teachers.FirstOrDefault(x => x.Email == User.Identity.Name);

            var results = from Enrollement in _context.Enrollements
                          join Grade in _context.Grades on Enrollement.EnrollementId equals Grade.EnrollementId
                          join Student in _context.Students on Enrollement.StudentId equals Student.StudentId
                          join Course in _context.Courses on Convert.ToInt32(Enrollement.Schedule.CourseId) equals Course.CourseId
                          where Convert.ToInt32(Enrollement.Schedule.TeacherId) == techr.TeacherId

                          select new
                          {
                              GradeId = Grade.GradeId,
                              StudentId = Enrollement.StudentId,
                              CourseId = Enrollement.Schedule.CourseId,
                              CourseName = Course.Cname,
                              TeacherId = Enrollement.Schedule.TeacherId,
                              Midterm = Grade.Midterm,
                              Final = Grade.Final,
                              Total = Grade.Total,
                              Passedcourse = Grade.Passedcourse,
                              StudentFirstName = Student.Sfname,
                              StudentLastName = Student.Slname
                          };

            return View(results.ToList());
        }

        public ActionResult StudentsGrades()
        {
            Student stu = _context.Students.FirstOrDefault(x => x.Semail == User.Identity.Name);

            var results = from Enrollement in _context.Enrollements
                          join Grade in _context.Grades on Enrollement.EnrollementId equals Grade.EnrollementId
                          join Student in _context.Students on Enrollement.StudentId equals Student.StudentId
                          join Course in _context.Courses on Convert.ToInt32(Enrollement.Schedule.CourseId) equals Course.CourseId
                          join Teacher in _context.Teachers on Convert.ToInt32(Enrollement.Schedule.TeacherId) equals Teacher.TeacherId
                          where Convert.ToInt32(Enrollement.StudentId) == stu.StudentId

                          select new
                          {
                              GradeId = Grade.GradeId,
                              StudentId = Enrollement.StudentId,
                              CourseId = Enrollement.Schedule.CourseId,
                              CourseName = Course.Cname,
                              TeacherId = Enrollement.Schedule.TeacherId,
                              TeacherFirstName = Teacher.Tfname,
                              TeacherLastName = Teacher.Tlname,
                              Midterm = Grade.Midterm,
                              Final = Grade.Final,
                              Total = Grade.Total,
                              Passedcourse = Grade.Passedcourse,
                              StudentFirstName = Student.Sfname,
                              StudentLastName = Student.Slname
                          };

            var gradeTotals = results.Select(r => r.Total);
            double averageTotal = (double)(gradeTotals.Any() ? gradeTotals.Average() : 0.0);

            ViewBag.AverageTotal = averageTotal;

            return View(results.ToList());
        }


        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeId,EnrollementId,Midterm,Final,Total,Passedcourse")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var enrollements = _context.Enrollements.ToList();

            ViewBag.Enrollements = new SelectList(enrollements, "EnrollementId", "EnrollementId");


            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeId,EnrollementId,Midterm,Final,Total,Passedcourse")] Grade grade)
        {
            if (id != grade.GradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.GradeId))
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
            return View(grade);
        }

        // GET: Grades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grades == null)
            {
                return Problem("Entity set 'MyDbContext.Grades'  is null.");
            }
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return (_context.Grades?.Any(e => e.GradeId == id)).GetValueOrDefault();
        }
    }
}
