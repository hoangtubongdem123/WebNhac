using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Account()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetListSong(int page =1, int pageSize=4 )
        {



            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();


            var songs = db.Songs
            .Include("Singers")
            .OrderBy(song => song.ID_Song)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
             .Select(song => new
                 {
             song.ID_Song,
             song.NAME,
             song.Path_BackGround,
             song.Path_Song,
             song.Types,
             Singer = new
             {
                 song.Singers.ID_Singer,
                 song.Singers.NAME
             }
         })
    .ToList();

            var totalRecords = db.Songs.Count();
            return Json(new {Song = songs , TotalRecords = totalRecords, CurrentPage = page } ,JsonRequestBehavior.AllowGet);


        }


    }


}