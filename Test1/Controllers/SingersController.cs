using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    public class SingersController : Controller
    {
        public ActionResult Singers()
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var singers = db.Singers.ToList();
            return View(singers);
        }

        [HttpPost]
        public ActionResult CreateSinger(string name)
        {
            Singers singer = new Singers();
            singer.NAME = name;
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            if (name != null)
            {
                db.Singers.Add(singer);
                db.SaveChanges();
            }
            return RedirectToAction("Singers");
        }

        public ActionResult DeleteSinger(int id)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var singer = db.Singers.Find(id);
            if (singer != null)
            {
                db.Singers.Remove(singer);
                db.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }

            return RedirectToAction("Singers");
        }

        public ActionResult UpdateSinger(int id, string name)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var singer = db.Singers.Find(id);
            if (singer != null)
            {
                singer.NAME = name;
                db.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Singers");
        }
    }
}