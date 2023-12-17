using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace SkylineAcademy.Controllers
{
    public class TeacherDashboardController : Controller
    {
        [Authorize(Roles = "SuperAdmin,Teacher")]
        // GET: TeacherDashboardController
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Teacher")]
        // GET: TeacherDashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Teacher")]
        // GET: TeacherDashboardController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Teacher")]
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
        [Authorize(Roles = "SuperAdmin,Teacher")]
        // GET: TeacherDashboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Teacher")]
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
        [Authorize(Roles = "SuperAdmin,Teacher")]
        // GET: TeacherDashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Teacher")]
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
