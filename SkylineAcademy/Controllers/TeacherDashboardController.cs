﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkylineAcademy.Controllers
{
    public class TeacherDashboardController : Controller
    {
        // GET: TeacherDashboardController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TeacherDashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeacherDashboardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherDashboardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherDashboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TeacherDashboardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherDashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeacherDashboardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}