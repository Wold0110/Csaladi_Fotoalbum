using Csaladi_Fotoalbum.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csaladi_Fotoalbum.Controllers
{
    public class ColorController : Controller
    {
        [HttpGet]
        [Route("/Color")]
        public IActionResult Index()
        {
            DatabaseContext dc = new();
            return View(dc.Colors.ToList());
        }

        [HttpGet]
        [Route("/Color/{id}")]
        public IActionResult Edit(int id)
        {
            DatabaseContext dc = new();
            Color c = dc.Colors.Where(x => x.Id == id).First();
            return View(c);
        }

        [HttpPost]
        [Route("/Color/Update")]
        public RedirectResult Update() {
            int id = int.Parse(Request.Form["id"]);
            string name = Request.Form["name"];
            string code = Request.Form["code"];
            DatabaseContext dc = new();
            dc.Colors.Where(x => x.Id == id).ToList().ForEach(x =>
            {
                x.Name = name;
                x.Colorcode = code.Replace("#", "");
            });
            dc.SaveChanges();

            return Redirect("/Color");
        }
    }
}
