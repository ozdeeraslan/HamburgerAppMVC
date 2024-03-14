using HamburgerAppV1.Data;
using HamburgerAppV1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HamburgerAppV1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Menuler.ToList());
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Hakkimizda")]
        public IActionResult Hakkimizda()
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
