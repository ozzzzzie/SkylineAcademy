using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkylineAcademy.Models;
using System.Data;

namespace SkylineAcademy.Controllers
{
    public class ProfileController : Controller
    {
        private readonly MyDbContext _context;

        public ProfileController(MyDbContext context)
        {
            _context = context;
        }
        // GET: ProfileController
        [Authorize(Roles = "Teacher")]
        public ActionResult Index()
        {
            Teacher tprofile = _context.Teachers.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            return View(tprofile);
        }
        [Authorize(Roles = "Student")]
        public ActionResult StudentIndex()
        {
            Student sprofile = _context.Students.Where(x => x.Semail == User.Identity.Name).FirstOrDefault();

            return View(sprofile);
        }
    }
}
