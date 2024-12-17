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
            
            if (Session["UserID"] == null) 
            {
               
                return RedirectToAction("Login", "Account");
            }
            
            return View();
        }

        public ActionResult getAllType()
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var type = db.Types.Select(t => new
            {   t.ID_Type,
                t.TypeName,
                t.Path_Type

            })

                    .ToList();


            return Json(new {Types = type}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getAllAlbum()
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var album = db.Album.Select(t => new
            {
                t.ID_Album,
                t.Album_Name,
                t.Path_Ablum

            })

                    .ToList();


            return Json(new { Album = album }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewSearch(string query)

        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var songs = db.Songs
                    .Where(s => s.NAME.Contains(query))
                    .Select(s => new SongViewModel
                    {
                        ID_Song = s.ID_Song,
                        NAME = s.NAME,
                        Path_Song = s.Path_Song,
                        Path_BackGround = s.Path_BackGround,
                        Plays = s.Plays ?? 0,
                        Singers = s.Singers != null ? new SingerViewModel
                        {
                            ID_Singer = s.Singers.ID_Singer,
                            NAME = s.Singers.NAME
                        } : null
                    })
                    .ToList();
            

            var singers = db.Singers
                            .Where(si => si.NAME.Contains(query))
                            .ToList();
            var singersiDs = singers.Select(s => s.ID_Singer).ToList();

          

          

            var types = db.Types
                          .Where(t => t.TypeName.Contains(query))
                          .ToList();

            var typeIds = types.Select(t => t.ID_Type).ToList();

            

           

          


            var allSongs = songs

                   .ToList();

            var allSingers = singers

                   .ToList();


            //var allSinger = singers
            var result = Tuple.Create(allSongs, types, allSingers);
            
            if (!result.Item1.Any() && !result.Item2.Any() && !result.Item3.Any())
            {
                return PartialView("_Noresult");
            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {

                return PartialView("_SearchResults", result);
            }
            return View(result);



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
        [HttpGet]
        public ActionResult Tangluotnghe(int id_song) {

            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var song = db.Songs.FirstOrDefault(t => t.ID_Song == id_song);


            song.Plays += 1;
            db.SaveChanges();



            return Json(new {luotnghe=song.Plays}, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Dashboard()
        {
            int? sessionLevel = Session["Level"] as int?;

            if (sessionLevel == 1)
            {
                return HttpNotFound();
            }
            return View("Dashboard");
        }

    }
}