using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
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
                    
                    if (!song.Playlists.Contains(playlist))
                    {
                        
                        song.Playlists.Add(playlist);
                        db.SaveChanges();

                        Console.WriteLine("Thêm Playlist vào bài hát thành công!");
                        return Json(new { success = true, message = "Bài hát đã được thêm vào Playlist "+ playlist.Name_Playlist  }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Bài hát "+song.NAME+" đã tồn tại trong Playlist này" }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json("Lỗi ", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DetailPlaylist(int id_playlist)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var playlist = db.Playlists
                .Where(t => t.ID_Playlist == id_playlist)
                .Select(t => new PlaylistDetailViewModel
                {
                    ID_Playlist = t.ID_Playlist,
                    Name_Playlist = t.Name_Playlist,
                    Songs = t.Songs.Select(s => new SongViewModel
                    {
                        ID_Song = s.ID_Song,
                        NAME = s.NAME,
                        Path_Song = s.Path_Song,
                        Path_BackGround = s.Path_BackGround,
                        Plays = s.Plays ?? 0,
                        Types = s.Types != null ? new TypeModel
                        {
                            TypeName = s.Types.TypeName
                        } : null ,
                         Singers = s.Singers != null ? new SingerViewModel
                         {
                             ID_Singer = s.Singers.ID_Singer,
                             NAME = s.Singers.NAME
                         } : null
                    }).ToList()
                })
                .FirstOrDefault();

            if (playlist == null)
            {
                return HttpNotFound("Playlist không tồn tại");
            }

            return View(playlist); 
        }


        public ActionResult AddPlaylist(string PlaylistName)
        {
            int id_user = Convert.ToInt32(Session["UserID"]);
            using (WebNgheNhacEntities1 db = new WebNgheNhacEntities1())
            {
                try
                {
                  
                    bool isDuplicate = db.Playlists
                                         .Any(p => p.Name_Playlist == PlaylistName && p.ID_User == id_user);
                    if (isDuplicate)
                    {
                        return Json(new { success = false, message = "Tên Playlist đã tồn tại." }, JsonRequestBehavior.AllowGet);
                    }

                    
                    var playlist = new Playlists
                    {
                        Name_Playlist = PlaylistName,
                        ID_User = id_user
                    };

                    db.Playlists.Add(playlist);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Thêm Playlist thành công!" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                   
                    return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }



        
        public JsonResult EditPlaylist(int id_playlist, string new_name)
        {
            try
            {
                using (WebNgheNhacEntities1 db = new WebNgheNhacEntities1())
                {
                    var playlist = db.Playlists.FirstOrDefault(p => p.ID_Playlist == id_playlist);

                    if (playlist == null)
                    {
                        return Json(new { success = false, message = "Playlist không tồn tại." }, JsonRequestBehavior.AllowGet);
                    }

                    bool isDuplicate = db.Playlists.Any(p => p.Name_Playlist == new_name && p.ID_User == playlist.ID_User);

                    if (isDuplicate)
                    {
                        return Json(new { success = false, message = "Tên Playlist đã tồn tại." }, JsonRequestBehavior.AllowGet);
                    }

                    playlist.Name_Playlist = new_name;
                    db.SaveChanges();

                    return Json(new { success = true, message = "Sửa Playlist thành công!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult DeletePlaylist(int id_playlist)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var playlist = db.Playlists.FirstOrDefault(p => p.ID_Playlist == id_playlist);
            db.Playlists.Remove(playlist);
            db.SaveChanges();
            return Json(new { success = true, message = "Xóa Playlist thành công!" }, JsonRequestBehavior.AllowGet);


        }


        public ActionResult DeleteSongInPlaylist(int playlistId, int songId)
        {
            using (WebNgheNhacEntities1 db = new WebNgheNhacEntities1())
            {
                try
                {
                    
                    var playlist = db.Playlists.FirstOrDefault(p => p.ID_Playlist == playlistId);
                    var song = db.Songs.FirstOrDefault(s => s.ID_Song == songId);

                    if (playlist == null)
                    {
                        return Json(new { success = false, message = "Playlist không tồn tại." }, JsonRequestBehavior.AllowGet);
                    }

                    if (song == null)
                    {
                        return Json(new { success = false, message = "Bài hát không tồn tại." }, JsonRequestBehavior.AllowGet);
                    }

                   
                    if (!song.Playlists.Any(p => p.ID_Playlist == playlistId))
                    {
                        return Json(new { success = false, message = $"Bài hát '{song.NAME}' không có trong Playlist '{playlist.Name_Playlist}'." }, JsonRequestBehavior.AllowGet);
                    }

                  
                    song.Playlists.Remove(playlist);
                    db.SaveChanges();

                    return Json(new { success = true, message = $"Bài hát '{song.NAME}' đã được xóa khỏi Playlist '{playlist.Name_Playlist}'." }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                   


                    return Json(new { success = false, message = "Đã xảy ra lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }



    }
}