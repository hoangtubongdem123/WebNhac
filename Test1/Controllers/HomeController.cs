using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {



            return View();
        }

        public ActionResult getAllType()
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var type = db.Types.Select(t => new
            {
                t.TypeName,
                t.Path_Type

            })

                    .ToList();


            return Json(new {Types = type}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Search(string query)
        
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var songs = db.Songs
                    .Where(s => s.NAME.Contains(query))
                    .Select(s => new { s.ID_Song, s.NAME })
                    .ToList();

            
            var types = db.Types
                          .Where(t => t.TypeName.Contains(query))
                          .Select(t => new {  t.TypeName })
                          .ToList();

         
            var albums = db.Album
                           .Where(a => a.Name_Album.Contains(query))
                           .Select(a => new { a.ID_Album, a.Name_Album })
                           .ToList();

            var singers = db.Singers
                            .Where(si => si.NAME.Contains(query))
                            .Select(si => new { si.ID_Singer, si.NAME })
                            .ToList();

            return Json(new
            {
                Songs = songs,
                Types = types,
                Albums = albums,
                Singers = singers
            }, JsonRequestBehavior.AllowGet);



        }

        [HttpGet]
        public ActionResult getAllSinger()
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var singer = db.Singers.Select(t => new
            {   
                t.ID_Singer,
                t.NAME,
                t.Path_Singer

            })

                    .ToList();


            return Json(new { Singers = singer }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }

    }
}