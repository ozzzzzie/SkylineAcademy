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
    public class ClassroomsController : Controller
    {
        private readonly MyDbContext _context;

        public ClassroomsController(MyDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Classrooms
        public async Task<IActionResult> Index()
        {
              return _context.Classrooms != null ? 
                          View(await _context.Classrooms.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.Classrooms'  is null.");
        }
        [Authorize]
        // GET: Classrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms
                .FirstOrDefaultAsync(m => m.ClassroomId == id);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }


        //Get the classroom schedules
        [Authorize]
        public IActionResult ClassroomTimetable(string academicYear, string semester, int? clsroom)
        {
            //get the classrooms
            var classrooms = _context.Classrooms.ToList();
            ViewBag.Classrooms = new SelectList(classrooms, "ClassroomId", "ClassroomId");


            //LINQ query to get all the information for all classrooms
            var query = from schedule in _context.ClassSchedules
                        join course in _context.Courses on Convert.ToInt32(schedule.CourseId) equals course.CourseId
                        join teacher in _context.Teachers on Convert.ToInt32(schedule.TeacherId) equals teacher.TeacherId
                        join classroom in _context.Classrooms on Convert.ToInt32(schedule.ClassroomId) equals classroom.ClassroomId
                        where schedule.Academicyear == academicYear &&
                              schedule.Semester == semester
                        select new
                        {
                            CourseName = course.Cname,
                            ClassroomNumber = schedule.ClassroomId,
                            TeacherName = teacher.Tfname + " " + teacher.Tlname,
                            Slot = schedule.Slot.SlotId,
                            Facilities = classroom.Facilities
                        };

            //adding a where statement to the query if the user selects a specific classroom
            if (clsroom.HasValue)
            {
                query = query.Where(x => Convert.ToInt16(x.ClassroomNumber) == clsroom.Value);
            }

            return View(query.ToList());
        }


       // GET: Classrooms/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
         public IActionResult Create()
        {
            return View();
        }


        // POST: Classrooms/Create
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassroomId,Capacity,Facilities")] Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classroom);
        }

        // GET: Classrooms/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }
            return View(classroom);
        }

        // POST: Classrooms/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassroomId,Capacity,Facilities")] Classroom classroom)
        {
            if (id != classroom.ClassroomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassroomExists(classroom.ClassroomId))
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
            return View(classroom);
        }

        // GET: Classrooms/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Classrooms == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms
                .FirstOrDefaultAsync(m => m.ClassroomId == id);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // POST: Classrooms/Delete/5
        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Classrooms == null)
            {
                return Problem("Entity set 'MyDbContext.Classrooms'  is null.");
            }
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom != null)
            {
                _context.Classrooms.Remove(classroom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassroomExists(int id)
        {
          return (_context.Classrooms?.Any(e => e.ClassroomId == id)).GetValueOrDefault();
        }
    }
}
