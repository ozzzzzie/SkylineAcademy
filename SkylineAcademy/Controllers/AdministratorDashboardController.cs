using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkylineAcademy.Controllers
{
    public class AdministratorDashboardController : Controller
    {
        [Authorize(Roles ="SuperAdmin,Admin")]
        // GET: AdministratorController
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: AdministratorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: AdministratorController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: AdministratorController/Create
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: AdministratorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: AdministratorController/Edit/5
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
        [Authorize(Roles = "SuperAdmin,Admin")]
        // GET: AdministratorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        // POST: AdministratorController/Delete/5
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
