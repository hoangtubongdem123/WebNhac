using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Test1.Models;
using PagedList;

namespace Test1.Controllers
{
    public class AlbumController : Controller
    {
        private WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
        public ActionResult Album(string sortOrder, string searchString, int? page, int? size)
        {
            int? sessionLevel = Session["Level"] as int?;

            if (sessionLevel == 1)
            {
                return HttpNotFound();
            }



            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.IDSortParm = sortOrder == "id" ? "id_desc" : "id";

            ViewBag.CurrentFilter = searchString;

            var albums = from s in db.Album select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(s => s.Album_Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                default:
                    goto case "id";
                case "id":
                    albums = albums.OrderBy(s => s.ID_Album);
                    break;
                case "id_desc":
                    albums = albums.OrderByDescending(s => s.ID_Album);
                    break;
                case "name_desc":
                    albums = albums.OrderByDescending(s => s.Album_Name);
                    break;
                case "name":
                    albums = albums.OrderBy(s => s.Album_Name);
                    break;
            }

            int pageSize = size ?? 7;
            int pageNumber = page ?? 1;

            return View(albums.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult CreateAlbum(string albumName, HttpPostedFileBase imageFile)
        {

            int? sessionLevel = Session["Level"] as int?;

            if (sessionLevel == 1)
            {
                return HttpNotFound();
            }

            Album album = new Album()
            {
                Album_Name = albumName,
            };
            var directoryPath = Server.MapPath("~/AlbumBackGround");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var fileName = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(Server.MapPath("~/AlbumBackGround"), fileName);

            imageFile.SaveAs(filePath);

            album.Path_Ablum = "/AlbumBackGround/" + fileName;

            if (!string.IsNullOrEmpty(albumName))
            {
                db.Album.Add(album);
                db.SaveChanges();
            }
            return RedirectToAction("Album");
        }


        [HttpPost]
        public ActionResult UpdateAlbum(int id, HttpPostedFileBase imageFile)
        {
            int? sessionLevel = Session["Level"] as int?;

            if (sessionLevel == 1)
            {
                return HttpNotFound();
            }
            var album = db.Album.Find(id);
            if (album != null)
            {
                db.SaveChanges();
                if (imageFile != null)
                {
                    var directoryPath = Server.MapPath("~/AlbumBackGround");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/AlbumBackGround"), fileName);

                    imageFile.SaveAs(filePath);

                    album.Path_Ablum = "/AlbumBackGround/" + fileName;
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Album");
                }
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Album");
        }


        public ActionResult DeleteAlbum(int id)
        {
            int? sessionLevel = Session["Level"] as int?;

            if (sessionLevel == 1)
            {
                return HttpNotFound();
            }
            var album = db.Album.Find(id);
            if (album != null)
            {
                db.Album.Remove(album);
                db.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Album");
        }
    }
}
