using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Test1.Models;

namespace Test1.Controllers
{
    public class LibraryController : Controller
    {

        public ActionResult Library()
        {


            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            List<Singers> t = db.Singers.ToList();

            List<Types> b = db.Types.ToList();





            return PartialView("_Library");

        }

        [HttpPost]
        public ActionResult SeachSinger(int id)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var caSi = db.Singers
                .Include("Songs")

                .Select(t => new
                {
                    t.ID_Singer,
                    t.NAME,
                    t.Path_Singer,
                    Songs = t.Songs.Select(s => new
                    {
                        s.ID_Song,
                        s.NAME,
                        s.Path_Song
                       
                    }).ToList()
                })
                .FirstOrDefault(c => c.ID_Singer == id)
                
                ;
         

          


            return Json(new {Singer = caSi}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetailSinger(int id_singer)
        {
            try
            {
                using (var db = new WebNgheNhacEntities1())
                {
                    var caSi = db.Singers
                        .Where(t => t.ID_Singer == id_singer)
                        .Select(t => new SingerDetailViewModel
                        {
                            ID_Singer = t.ID_Singer,
                            NAME = t.NAME,
                            Path_Singer = t.Path_Singer,
                            Songs = t.Songs.Select(s => new SongViewModel
                            {
                                ID_Song = s.ID_Song,
                                NAME = s.NAME,
                                Path_Song = s.Path_Song,
                                Path_BackGround = s.Path_BackGround,
                                Plays = s.Plays ?? 0 ,
                                Types = s.Types != null ? new TypeModel
                                {
                                   
                                    TypeName = s.Types.TypeName 
                                } : null
                                

                            }).ToList()
                        })
                        .FirstOrDefault();

                    if (caSi == null)
                    {
                        return HttpNotFound("Ca sĩ không tồn tại");
                    }

                    return View(caSi);
                }
            }
            catch (Exception ex)
            {
                // Log exception details and display to user
                return Content($"Lỗi: {ex.Message}");
            }
        }



        public ActionResult ViewType() { return View(); }

        public ActionResult ListSinger() { return View(); }


        public ActionResult DetailType(int id_type)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var type = db.Types.Where(t => t.ID_Type == id_type).Select(t => new TypeDetailViewModel
            {
                ID_Type = t.ID_Type,
                TypeName = t.TypeName,
                Path_Type = t.Path_Type,
                Songs = t.Songs.Select(s => new SongViewModel
                {
                    ID_Song = s.ID_Song,
                    NAME = s.NAME,
                    Path_Song = s.Path_Song,
                    Path_BackGround = s.Path_BackGround,
                    Plays = s.Plays ?? 0,
                    Singers = s.Singers != null ? new SingerViewModel
                    {
                        ID_Singer = s.Singers.ID_Singer,
                        NAME = s.Singers.NAME
                    } : null 
                }).ToList()
            }).FirstOrDefault();

            return View(type);
        }




        public ActionResult ListType()
        {

            return View();
        }




        public ActionResult ListAlbum()
        {


            return View();
        }

        public ActionResult DetailAlbum(int id_album)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            var album = db.Album.Where(t => t.ID_Album == id_album).Select(t => new AlbumDetaiViewModel
            {
                ID_Album = t.ID_Album,
                Album_Name = t.Album_Name,
                Path_Album = t.Path_Ablum,
                Songs = t.Songs.Select(s => new SongViewModel
                {
                    ID_Song = s.ID_Song,
                    NAME = s.NAME,
                    Path_Song = s.Path_Song,
                    Path_BackGround = s.Path_BackGround,
                    Plays = s.Plays ?? 0,
                    Singers = s.Singers != null ? new SingerViewModel
                    {
                        ID_Singer = s.Singers.ID_Singer,
                        NAME = s.Singers.NAME
                    } : null
                }).ToList()
            }).FirstOrDefault();

            return View(album);
        }



    }






}