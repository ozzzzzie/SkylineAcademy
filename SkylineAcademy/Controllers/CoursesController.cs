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
    public class CoursesController : Controller
    {
        private readonly MyDbContext _context;

        public CoursesController(MyDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Courses.Include(c => c.Major);
            return View(await myDbContext.ToListAsync());
        }
        [Authorize]
        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Major)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "Mname");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Cname,MajorId,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "Mname", course.MajorId);
            return View(course);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "Mname", course.MajorId);
            return View(course);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Cname,MajorId,Credits")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
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
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "Mname", course.MajorId);
            return View(course);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Major)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'MyDbContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
