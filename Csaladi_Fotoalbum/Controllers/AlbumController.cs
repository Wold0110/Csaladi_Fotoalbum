using Csaladi_Fotoalbum.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace Csaladi_Fotoalbum.Controllers
{
    public class AlbumController : Controller
    {
        [HttpGet]
        [Route("/Album")]
        public IActionResult Index()
        {
            DatabaseContext dc = new();
            return View(dc.Albums.ToList());
        }
        [HttpGet]
        [Route("/Album/DelImg/{album}")]
        public RedirectResult DelImg(string album)
        {
            string pic = HttpContext.Request.Query["picture"].ToString();
            if (System.IO.File.Exists("wwwroot/photos/" + album + "/" + pic))
                System.IO.File.Delete("wwwroot/photos/" + album + "/" + pic);
            return Redirect("/Album/" + album);
        }

        #region Change_Data
        [HttpGet]
        [Route("/Album/Edit/{album}")]
        public IActionResult Edit(int album)
        {
            ViewBag.Album = album;
            DatabaseContext dc = new();
            return View(dc);
        }
        [HttpPost]
        [Route("/Album/Update")]
        public RedirectResult Update()
        {
            DatabaseContext dc = new();
            int id = int.Parse(Request.Form["id"]);
            string name = Request.Form["name"];
            DateTime date = DateTime.Parse(Request.Form["date"]);
            int color = int.Parse(Request.Form["color"]);
            int location = int.Parse(Request.Form["location"]);

            dc.Albums.Where(x => x.Id == id).ToList().ForEach(x => {
                x.Name = name;
                x.Date = date;
                x.Location = location;
                x.Color= color;
            });

            dc.SaveChanges();
            return Redirect("/Album/" + id);
        }
        #endregion Change_Data

        #region New_Album
        [HttpGet]
        [Route("/Album/New")]
        public IActionResult New()
        {
            return View(new DatabaseContext());
        }

        [HttpPost]
        [Route("/Album/Insert")]
        public RedirectResult Insert()
        {
            Album a = new();
            a.Name = Request.Form["name"];
            a.Location = int.Parse(Request.Form["location"]);
            a.Color = int.Parse(Request.Form["color"]);
            a.Date = DateTime.Parse(Request.Form["date"]);
            DatabaseContext dc = new();
            dc.Albums.Add(a);
            dc.SaveChanges();
            return Redirect("..");
        }
        #endregion New_Album

        #region DeleteAlbum
        [HttpGet]
        [Route("/Album/Del/{id}")]
        public IActionResult Del(int id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        [Route("/Album/Remove")]
        public RedirectResult Remove()
        {
            int id = int.Parse(Request.Form["id"]);
            DatabaseContext dc = new DatabaseContext();
            Album a = dc.Albums.Where(x => x.Id == id).FirstOrDefault();
            dc.Albums.Remove(a);
            List<Connection> c = dc.Connections.Where(x => x.Album == id).ToList();
            c.ForEach(x => dc.Connections.Remove(x));
            if (Directory.Exists("wwwroot/photos/" + id))
            {
                foreach (string x in Directory.GetFiles("wwwroot/photos/" + id))
                    System.IO.File.Delete(x);
                Directory.Delete("wwwroot/photos/" + id);
            }


            dc.SaveChanges();
            return Redirect("..");
        }
        #endregion

        [HttpGet]
        [Route("/Album/{id}")]
        public IActionResult View(int id) {
            ViewBag.size = HttpContext.Session.GetString("imgsize");
            ViewBag.Id = id;
            return View(new DatabaseContext());
        }

        [HttpPost]
        [Route("/Album/ImgSize")]
        public RedirectResult ImgSize()
        {
            HttpContext.Session.SetString("imgsize", Request.Form["size"]);
            return Redirect("/Album/" + Request.Form["album"]);
        }

        #region Connection
        [HttpPost]
        [Route("/Album/LinkPerson")]
        public RedirectResult LinkPerson()
        {
            int album = int.Parse(Request.Form["id"]);
            int person = int.Parse(Request.Form["person"]);
            Connection c = new();
            c.Album = album;c.Person = person;
            DatabaseContext db = new();
            db.Connections.Add(c);
            db.SaveChanges();
            return Redirect("/Album/"+album);
        }
        
        [HttpGet]
        [Route("/Album/{album}/{person}")]
        public RedirectResult Unlink(int album, int person)
        {
            DatabaseContext dc = new();
            Connection c = dc.Connections.Where(x => x.Album == album && x.Person == person).First();
            dc.Connections.Remove(c);
            dc.SaveChanges();
            return Redirect("/Album/" + album);
        }
        #endregion Connection

        #region FileMovement
        [HttpPost]
        [Route("/Album/Upload/{id}")]
        public RedirectResult Upload(int id, List<IFormFile> photo)
        {
            string path = "wwwroot/photos/" + id;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            foreach (var formFile in photo)
            {
                if (formFile.Length > 0)
                {
                    string fileName = formFile.FileName;
                    int index = 1;
                    while(System.IO.File.Exists(path + "/" + fileName))
                    {
                        fileName = Path.GetFileNameWithoutExtension(formFile.FileName) + "_"+index + Path.GetExtension(formFile.FileName);
                        index++;
                    }
                    using (var stream = new FileStream(path+"/"+fileName, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                }
            }
            return Redirect("/Album/"+id);
        }
        
        [HttpGet]
        [Route("/Album/Download/{id}")]
        public FileResult Download(int id)
        {
            DatabaseContext dc = new();
            string name = dc.Albums.Where(x => x.Id == id).First().Name;
            string output = name + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".zip";
            string dir = "wwwroot/photos/" + id;
            if (System.IO.File.Exists(output))
                System.IO.File.Delete(output);
            if (Directory.Exists(dir))
                ZipFile.CreateFromDirectory(dir, output);
            
            return File(System.IO.File.ReadAllBytes(output), System.Net.Mime.MediaTypeNames.Application.Octet, output);
        }
        #endregion FileMovement
    }
}
