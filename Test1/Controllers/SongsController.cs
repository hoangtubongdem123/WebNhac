using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PagedList;
using Test1.Models;

namespace Test1.Controllers
{
    public class SongsController : Controller
    {
        private WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

        public ActionResult Songs(string sortOrder, string searchString, int? page, int? size)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParm = sortOrder == "id" ? "id_desc" : "id";

            ViewBag.CurrentFilter = searchString;

            var songs = from s in db.Songs select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                songs = songs.Where(s => s.NAME.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id":
                    songs = songs.OrderBy(s => s.ID_Song);
                    break;
                case "id_desc":
                    songs = songs.OrderByDescending(s => s.ID_Song);
                    break;
                case "name_desc":
                    songs = songs.OrderByDescending(s => s.NAME);
                    break;
                default:
                    songs = songs.OrderBy(s => s.NAME);
                    break;
            }

            int pageSize = size ?? 5;
            int pageNumber = page ?? 1;

            return View(songs.ToPagedList(pageNumber, pageSize));
        }

        private void PopulateDropDowns()
        {
            ViewBag.Singers = new SelectList(db.Singers, "ID_Singer", "NAME");
            ViewBag.Types = new SelectList(db.Types, "ID_Type", "TypeName");
            ViewBag.Album = new SelectList(db.Album, "ID_Album", "Album_Name");
        }

        public ActionResult Create()
        {
            PopulateDropDowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Song,NAME,ID_Singer,ID_Type,ID_Album")] Songs song, HttpPostedFileBase imageFile, HttpPostedFileBase songFile)
        {
            PopulateDropDowns();
            var imageFileName = Path.GetFileName(imageFile.FileName);
            var songFileName = Path.GetFileName(songFile.FileName);
            var imagePath = Path.Combine(Server.MapPath("~/SongBackGround"), imageFileName);
            var songPath = Path.Combine(Server.MapPath("~/Songs"), songFileName);
            if (ModelState.IsValid)
            {
                imageFile.SaveAs(imagePath);
                songFile.SaveAs(songPath);
                song.Path_Song = "/Songs/" + songFileName;
                song.Path_BackGround = "/SongBackGround/" + imageFileName;

                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Songs");
            }

            return View(song);
        }

        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Songs song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.Singers = new SelectList(db.Singers, "ID_Singer", "NAME");
            ViewBag.Types = new SelectList(db.Types, "ID_Type", "TypeName");
            ViewBag.Album = new SelectList(db.Album, "ID_Album", "Album_Name");
            return View(song);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Song,NAME,ID_Singer,ID_Type,ID_Album")] Songs song, HttpPostedFileBase imageFile, HttpPostedFileBase songFile)
        {
            PopulateDropDowns();
            if (ModelState.IsValid)
            {
                var existingSong = db.Songs.Find(song.ID_Song);
                if (existingSong == null)
                {
                    return HttpNotFound();
                }

                existingSong.NAME = song.NAME;
                existingSong.ID_Singer = song.ID_Singer;
                existingSong.ID_Type = song.ID_Type;
                existingSong.ID_Album = song.ID_Album;

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var imageFileName = Path.GetFileName(imageFile.FileName);
                    var imagePath = Path.Combine(Server.MapPath("~/SongBackGround"), imageFileName);
                    imageFile.SaveAs(imagePath);
                    existingSong.Path_BackGround = "/SongBackGround/" + imageFileName;
                }

                if (songFile != null && songFile.ContentLength > 0)
                {
                    var songFileName = Path.GetFileName(songFile.FileName);
                    var songPath = Path.Combine(Server.MapPath("~/Songs"), songFileName);
                    songFile.SaveAs(songPath);
                    existingSong.Path_Song = "/Songs/" + songFileName;
                }

                db.Entry(existingSong).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Songs");
            }

            ViewBag.Types = new SelectList(db.Types, "TypeName", "TypeName", song.ID_Type);
            return View(song);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Songs song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }

            return View(song);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Songs song = db.Songs.Find(id);
                if (song != null)
                {
                    db.Songs.Remove(song);
                    db.SaveChanges();
                }
                return RedirectToAction("Songs");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while deleting the song.");
                return RedirectToAction("Songs");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
