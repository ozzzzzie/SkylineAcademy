using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkylineAcademy.Controllers
{
    public class StudentDashboardController : Controller
    {
        // GET: StudentDashboardController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StudentDashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentDashboardController/Create
        public ActionResult Create()
        {
            return View();
        }

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

        // GET: StudentDashboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

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

        // GET: StudentDashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

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
