using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    public class SongsController : Controller
    {
        public ActionResult Songs()
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var songs = db.Songs.ToList();
            return View(songs);
        }

        [HttpPost]
        public ActionResult CreateSong(string name, int singerId, int typeId, HttpPostedFileBase songFile, HttpPostedFileBase imageFile)
        {
            Songs song = new Songs();
            
            var songFileName = Path.GetFileName(songFile.FileName);
            var songFilePath = Path.Combine(Server.MapPath("~/Songs"), songFileName);
            var imageFileName = Path.GetFileName(imageFile.FileName);
            var imageFilePath = Path.Combine(Server.MapPath("~/SongBackGround"), imageFileName);

            songFile.SaveAs(songFilePath);
            imageFile.SaveAs(songFilePath);

            song.Path_Song = "/Songs/" + songFileName;
            song.Path_BackGround = "/SongBackGround/" + imageFileName;

            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            if (name != null)
            {
                song.NAME = name;
                song.ID_Type = typeId;
                song.ID_Singer = singerId;
                db.Songs.Add(song);
                db.SaveChanges();
            }

            return RedirectToAction("Songs");
        }

        public ActionResult DeleteSong(int id)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var song = db.Songs.Find(id);
            if (song != null)
            {
                db.Songs.Remove(song);
                db.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }

            return RedirectToAction("Songs");
        }

        public ActionResult UpdateSong(int id, string name, int singerId, int typeId, HttpPostedFileBase songFile, HttpPostedFileBase imageFile)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var song = db.Songs.Find(id);
            if (song != null)
            {
                song.NAME = name;
                song.ID_Singer = singerId;  
                song.ID_Type = typeId;  
                db.SaveChanges();
                if (imageFile != null)
                {
                    var songFileName = Path.GetFileName(songFile.FileName);
                    var songFilePath = Path.Combine(Server.MapPath("~/Songs"), songFileName);
                    var imageFileName = Path.GetFileName(imageFile.FileName);
                    var imageFilePath = Path.Combine(Server.MapPath("~/SongBackGround"), imageFileName);

                    songFile.SaveAs(songFilePath);
                    imageFile.SaveAs(songFilePath);

                    song.Path_Song = "/Songs/" + songFileName;
                    song.Path_BackGround = "/SongBackGround/" + imageFileName;
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Songs");
                }
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Songs");

        }
        public ActionResult SearchSong(string keywords)
        {
            using (var db = new WebNgheNhacEntities1())
            {
                if (keywords != null)
                {
                    var searchResults = db.Songs
                                          .Where(s => s.NAME.Contains(keywords))
                                          .ToList();
                    return PartialView("Songs", searchResults);
                }
                else
                {
                    return RedirectToAction("Songs");
                }
            }
        }

    }
}