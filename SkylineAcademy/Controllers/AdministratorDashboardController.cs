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
    }
}
