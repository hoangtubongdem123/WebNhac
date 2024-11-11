using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    public class LibraryController : Controller
    {
        
        public ActionResult Library()
        {


           WebNgheNhacEntities db = new WebNgheNhacEntities();

            List<Singers> t = db.Singers.ToList();

            List<Types> b = db.Types.ToList();



            ViewBag.Allsinger = t;
            ViewBag.Alltype = b;


            return PartialView("_Library");

        }



    }
}