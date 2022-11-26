using Csaladi_Fotoalbum.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csaladi_Fotoalbum.Controllers
{
    public class LocationController : Controller
    {
        [HttpGet]
        [Route("/Location")]
        public IActionResult Index()
        {
            DatabaseContext dc = new();
            return View(dc.Locations.ToList());
        }

        [HttpGet]
        [Route("/Location/{id}")]
        public IActionResult Edit(string id)
        {
            DatabaseContext dc = new();
            int index = int.Parse(id);
            Location l = dc.Locations.Where(x => x.Id == index).First();
            return View(l);
        }

        [HttpPost]
        [Route("/Location/Update")]
        public RedirectResult Update() {
            int id = int.Parse(Request.Form["id"]); 
            string name = Request.Form["name"];
            DatabaseContext dc = new();
            dc.Locations.Where(x => x.Id == id).ToList().ForEach(x => x.Name = name);
            dc.SaveChanges();

            return Redirect("/Location");
        }

        [HttpGet]
        [Route("/Location/NewLocation")]
        public IActionResult New() {
            return View();
        }

        [HttpPost]
        [Route("/Location/Insert")]
        public RedirectResult Insert() {
            Location l = new();
            l.Name = Request.Form["name"];
            DatabaseContext dc = new();
            dc.Locations.Add(l);
            dc.SaveChanges();
            return Redirect("/Location");
        }
    }
}
