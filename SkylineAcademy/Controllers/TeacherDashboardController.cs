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
    }
}
