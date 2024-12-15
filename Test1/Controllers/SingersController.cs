using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
        public ActionResult CreateSinger(string name, HttpPostedFileBase imageFile)
        {
            Singers singer = new Singers();
            singer.NAME = name;

            var fileName = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(Server.MapPath("~/SingerBackGround"), fileName);

            imageFile.SaveAs(filePath);

            singer.Path_Singer = "/SingerBackGround/" + fileName;

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

        public ActionResult UpdateSinger(int id, string name, HttpPostedFileBase imageFile)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var singer = db.Singers.Find(id);
            if (singer != null)
            {
                singer.NAME = name;
                db.SaveChanges();
                if (imageFile != null)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/SingerBackGround"), fileName);

                    imageFile.SaveAs(filePath);

                    singer.Path_Singer = "/SingerBackGround/" + fileName;
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Singers");
                }   
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Singers");

        }
        public ActionResult SearchSinger(string keywords)
        {
            using (var db = new WebNgheNhacEntities1())
            {
                if (keywords != null)
                {
                    var searchResults = db.Singers
                                          .Where(s => s.NAME.Contains(keywords))
                                          .ToList();
                    return PartialView("Singers",searchResults);
                }
                else
                {
                    return RedirectToAction("Singers");
                }
            }
        }

    }
}