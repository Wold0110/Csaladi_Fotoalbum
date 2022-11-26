using Csaladi_Fotoalbum.DatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace Csaladi_Fotoalbum.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        [Route("/Person")]
        public IActionResult Index()
        {
            DatabaseContext dc = new();
            return View(dc.People.ToList());
        }

        [HttpGet]
        [Route("/Person/NewPerson")]
        public IActionResult NewPerson() => View();

        [HttpPost]
        [Route("/Person/InsertPerson")]
        public RedirectResult InsertPerson()
        {
            DatabaseContext dc = new();
            Person p = new();
            p.Name = Request.Form["name"];
            dc.People.Add(p);
            dc.SaveChanges();
            return Redirect("/Person");
        }

        [HttpGet]
        [Route("/Person/{id}")]
        public IActionResult EditPerson(int id)
        {
            DatabaseContext dc = new();
            Person p = dc.People.Where(x => x.Id == id).First();
            return View(p);
        }
        [HttpPost]
        [Route("/Person/UpdatePerson")]
        public RedirectResult UpdatePerson() {
            int id = int.Parse(Request.Form["id"]);
            string name = Request.Form["name"];
            DatabaseContext dc = new();
            dc.People.Where(x => x.Id == id).ToList().ForEach(x => x.Name = name);
            dc.SaveChanges();
            return Redirect("/Person");
        }
    }
}
