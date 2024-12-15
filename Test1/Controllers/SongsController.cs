using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    public class SongsController : Controller
    {
        public ActionResult Songs()
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var songs = db.Songs.ToList();
            return View(songs);
        }
    }
}