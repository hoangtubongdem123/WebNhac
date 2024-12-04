using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Test1.Models;

namespace Test1.Controllers
{
    public class LibraryController : Controller
    {

        public ActionResult Library()
        {


            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            List<Singers> t = db.Singers.ToList();

            List<Types> b = db.Types.ToList();



            ViewBag.Allsinger = t;
            ViewBag.Alltype = b;


            return PartialView("_Library");

        }

        [HttpPost]
        public ActionResult SeachSinger(int id)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var caSi = db.Singers
                .Include("Songs")

                .Select(t => new
                {
                    t.ID_Singer,
                    t.NAME,
                    t.Path_Singer,
                    Songs = t.Songs.Select(s => new
                    {
                        s.ID_Song,
                        s.NAME,
                        s.Path_Song
                       
                    }).ToList()
                })
                .FirstOrDefault(c => c.ID_Singer == id)
                
                ;
         

          


            return Json(new {Singer = caSi}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailSinger()
        {
            return View();
        }


        public ActionResult ViewType() { return View(); }
        

        


    }






}