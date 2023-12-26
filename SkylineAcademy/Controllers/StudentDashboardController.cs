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
    }
}
