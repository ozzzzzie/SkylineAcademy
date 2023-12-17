using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace SkylineAcademy.Controllers
{
    public class StudentDashboardController : Controller
    {
        [Authorize(Roles = "SuperAdmin,Student")]
        // GET: StudentDashboardController
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Student")]
        // GET: StudentDashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Student")]
        // GET: StudentDashboardController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Student")]
        // POST: StudentDashboardController/Create
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
        [Authorize(Roles = "SuperAdmin,Student")]
        // GET: StudentDashboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Student")]
        // POST: StudentDashboardController/Edit/5
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
        [Authorize(Roles = "SuperAdmin,Student")]
        // GET: StudentDashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Student")]
        // POST: StudentDashboardController/Delete/5
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
