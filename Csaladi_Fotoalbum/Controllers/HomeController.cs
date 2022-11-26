using Csaladi_Fotoalbum.DatabaseModels;
using Csaladi_Fotoalbum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace Csaladi_Fotoalbum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        [Route("/Location")]
        public RedirectResult Location()
        {
            HttpContext.Session.SetString("location", Request.Form["location"]);
            return Redirect("..");
        }


        [HttpGet]
        [Route("/DelPerson/{id}")]
        public RedirectResult DelPerson(string id)
        {
            List<string> filter = HttpContext.Session.GetString("filter").Split(';').ToList();
            filter.Remove(id);
            string newFilter = "";
            if(filter.Count > 0)
            {
                newFilter = filter.First();
                filter.Skip(1).ToList().ForEach(x => newFilter += ";"+x);
            }
            HttpContext.Session.SetString("filter", newFilter);
            return Redirect("..");
        }

        [HttpPost]
        [Route("/AddPerson")]
        public RedirectResult AddPerson()
        {
            string filter = HttpContext.Session.GetString("filter");
            filter+=(String.IsNullOrEmpty(filter) ? "" : ";")+Request.Form["person"];
            HttpContext.Session.SetString("filter", filter);
            return Redirect("..");
        }
        [HttpGet]
        public IActionResult Index()
        {
            DatabaseContext dc = new();
            ViewBag.people = String.IsNullOrEmpty(HttpContext.Session.GetString("filter")) ? "" : HttpContext.Session.GetString("filter");
            ViewBag.location = String.IsNullOrEmpty(HttpContext.Session.GetString("location")) ? "" : HttpContext.Session.GetString("location");
            return View(dc);
        }

        [HttpGet]
        public IActionResult Download()
        {
            DatabaseContext dc = new();
            return View(dc);
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