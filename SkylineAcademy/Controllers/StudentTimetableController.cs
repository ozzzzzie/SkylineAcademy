using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkylineAcademy.Controllers
{
    public class StudentTimetableController : Controller
    {
        // GET: StudentTimetableController
        public ActionResult Index()
        {
            return View();
        }
    }
}
