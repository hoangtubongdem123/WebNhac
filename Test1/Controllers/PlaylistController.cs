using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
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
        
        public ActionResult getAllPlaylist()
        {
            var userId =Convert.ToInt32(Session["UserID"]);
            var username = Session["UserName"];

            WebNgheNhacEntities1 db= new WebNgheNhacEntities1();
            var t = db.Playlists
                 .Where(p => p.ID_User == userId)
                .Select(v => new {
                    v.ID_Playlist,
                    v.Name_Playlist,
                   



                })
                
                .ToList();



            return Json(t, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertSongInPlaylist(int playlistId, int songId)
        {
            using (WebNgheNhacEntities1 db = new WebNgheNhacEntities1())
            {
               
                var playlist = db.Playlists.FirstOrDefault(p => p.ID_Playlist == playlistId);
                var song = db.Songs.FirstOrDefault(s => s.ID_Song == songId);

              
                if (playlist != null && song != null)
                {
                    
                    if (!db.Playlists.Contains(playlist))
                    {
                        
                        db.Playlists.Add(playlist);
                        db.SaveChanges();

                        Console.WriteLine("Thêm Playlist vào bài hát thành công!");
                        return Json("Thành công", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Bài hát đã tồn tại trong Playlist", JsonRequestBehavior.AllowGet);
                    }
                }

                return Json("Playlist hoặc bài hát không tồn tại", JsonRequestBehavior.AllowGet);
            }
        }





    }
}