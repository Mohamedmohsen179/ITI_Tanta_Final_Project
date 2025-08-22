using System;
using System.Diagnostics;
using System.Linq;
using ITI_Tanta_Final_Project.context;
using ITI_Tanta_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITI_Tanta_Final_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalCourses = _context.Courses.Count();

            ViewBag.ActiveSessionsComputed = _context.Sessions
            .AsEnumerable()
            .Count(s => s.IsActive);

            ViewBag.TotalUsers = _context.Users.Count();

            if (_context.Grades.Any())
                ViewBag.AvgGrade = _context.Grades.Average(g => g.value);
            else
                ViewBag.AvgGrade = 0;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
