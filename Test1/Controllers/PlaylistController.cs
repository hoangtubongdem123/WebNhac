using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using Test1.Models;


namespace Test1.Controllers
{
    public class PlaylistController : Controller
    {
        // GET: Playlist
        public ActionResult Playlist()
        {

            

            

            return View();
        }
        [HttpPost]
        public ActionResult GetListSinger()
        {
            try
            {
                using (var db = new WebNgheNhacEntities1())
                {
                 

                    db.Configuration.LazyLoadingEnabled = false;

                    List<Singers> t = db.Singers.ToList();
                    ViewBag.List = t;

                   
                    return Json(t);
                }
            }
            catch (Exception ex)
            {
             
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult AddSinger(Singers singer)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            if (singer == null) {
                return Json(new { success = false, message = "Vui long nhap thong tin" });

            }
            else
            {
                db.Singers.Add(singer);
                db.SaveChanges();
                return Json(new { success = true, message = "Them Thanh Cong" });

            }
            


            
        }



        [HttpPost]
        public ActionResult Edit(int id)
        {

            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            Singers singer = db.Singers.FirstOrDefault(s => s.ID_Singer == id);







            return Json(new { NAME = singer.NAME });



        }

        
   


    }
}