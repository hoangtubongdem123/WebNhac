using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Test1.Models;

namespace Test1.Controllers
{
    public class TypesController : Controller
    {
        public ActionResult Types()
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var types = db.Types.ToList();
            return View(types);
        }
    }
}