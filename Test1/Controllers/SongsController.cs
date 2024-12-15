using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using Test1.Models; // Replace with the namespace where your Model1.edmx entities are located

namespace Test1.Controllers
{
    public class SongsController : Controller
    {
        private WebNgheNhacEntities1 db = new WebNgheNhacEntities1(); // EF DbContext generated from Model1.edmx

        // GET: Songs
        public ActionResult Songs(string sortOrder, string searchString, int? page, int? size)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParm = sortOrder == "id" ? "id_desc" : "id";

            ViewBag.CurrentFilter = searchString;

            // Query songs with LINQ
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

        // GET: Songs/Create
        public ActionResult Create()
        {
            var typesList = db.Types.Select(t => t.TypeName).ToList();
            ViewBag.Types = new SelectList(typesList);
            return View();
        }

        // POST: Songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Song,NAME,Path_Song,Path_BackGround,TypeName")] Songs song)
        {
            if (ModelState.IsValid)
            {
                db.Songs.Add(song); // Add new song using EF
                db.SaveChanges(); // Save changes
                return RedirectToAction("Songs");
            }

            return View(song);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Songs song = db.Songs.Find(id); // Fetch song by ID using EF
            if (song == null)
            {
                return HttpNotFound();
            }

            ViewBag.Types = new SelectList(db.Types, "TypeName", "TypeName", song.Types);
            return View(song);
        }

        // POST: Songs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Song,NAME,Path_Song,Path_BackGround,TypeName")] Songs song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = System.Data.Entity.EntityState.Modified; // Mark entity as modified
                db.SaveChanges(); // Save changes
                return RedirectToAction("Songs");
            }

            ViewBag.Types = new SelectList(db.Types, "TypeName", "TypeName", song.Types);
            return View(song);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Songs song = db.Songs.Find(id); // Fetch song by ID using EF
            if (song == null)
            {
                return HttpNotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Songs song = db.Songs.Find(id);
                if (song != null)
                {
                    db.Songs.Remove(song); // Remove song using EF
                    db.SaveChanges(); // Save changes
                }
                return RedirectToAction("Songs");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while deleting the song.");
                return RedirectToAction("Songs");
            }
        }

        // Dispose the context
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
