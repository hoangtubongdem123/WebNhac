using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Test1.Models;
using PagedList;

namespace Test1.Controllers
{
    public class SingersController : Controller
    {
        private WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
        public ActionResult Singers(string sortOrder, string searchString, int? page, int? size)
        {
            int? sessionLevel = Session["Level"] as int?;
            if (Session["UserID"] == null)
            {

                return RedirectToAction("Login", "Account");
            }
            if (sessionLevel == 1)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name" ;
            ViewBag.IDSortParm = sortOrder == "id" ? "id_desc" : "id";

            ViewBag.CurrentFilter = searchString;

            var singers = from s in db.Singers select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                singers = singers.Where(s => s.NAME.Contains(searchString));
            }

            switch (sortOrder)
            {
                default:
                    goto case "id";
                case "id":
                    singers = singers.OrderBy(s => s.ID_Singer);
                    break;
                case "id_desc":
                    singers = singers.OrderByDescending(s => s.ID_Singer);
                    break;
                case "name_desc":
                    singers = singers.OrderByDescending(s => s.NAME);
                    break;
                case "name":
                    singers = singers.OrderBy(s => s.NAME);
                    break;
            }

            int pageSize = size ?? 7;
            int pageNumber = page ?? 1;

            return View(singers.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult CreateSinger(string name, HttpPostedFileBase imageFile)
        {
            int? sessionLevel = Session["Level"] as int?;

            if (sessionLevel == 1)
            {
                return HttpNotFound();
            }

            Singers singer = new Singers();
            singer.NAME = name;

            var fileName = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(Server.MapPath("~/SingerBackGround"), fileName);

            imageFile.SaveAs(filePath);

            singer.Path_Singer = "/SingerBackGround/" + fileName;

            if (name != null)
            {
                db.Singers.Add(singer);
                db.SaveChanges();
            }

            return RedirectToAction("Singers");
        }

        public ActionResult DeleteSinger(int id)
        {
            int? sessionLevel = Session["Level"] as int?;

            if (sessionLevel == 1)
            {
                return HttpNotFound();
            }

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
            int? sessionLevel = Session["Level"] as int?;

            if (sessionLevel == 1)
            {
                return HttpNotFound();
            }

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
            int? sessionLevel = Session["Level"] as int?;

            if (sessionLevel == 1)
            {
                return HttpNotFound();
            }
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