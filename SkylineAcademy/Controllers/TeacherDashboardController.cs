using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace SkylineAcademy.Controllers
{
    public class TeacherDashboardController : Controller
    {
        // GET: TeacherDashboardController
        [Authorize(Roles = "SuperAdmin,Teacher")]

        public ActionResult Index()
        {
            return View();
        }
    }
}
