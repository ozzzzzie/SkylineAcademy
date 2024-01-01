using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace SkylineAcademy.Controllers
{
    public class StudentDashboardController : Controller
    {
        // GET: StudentDashboardController
        [Authorize(Roles = "SuperAdmin,Student")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
