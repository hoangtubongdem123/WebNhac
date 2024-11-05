using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {


            Songs t = new Songs();
            t.Id = 123;
            t.PathSongs = Url.Content("~/Songs/song1.mp3") ;
            t.PathBackground = "~/Songs/song1.mp3";
            t.Singer = "Tung";



            Songs t1 = new Songs();
            t1.Id = 124;
            t1.PathSongs = Url.Content("~/Songs/song2.mp3");
            t1.PathBackground = "~/Songs/song1.mp3";
            t1.Singer = "Tung";

            Songs t2 = new Songs();
            t2.Id = 124;
            t2.PathSongs = Url.Content("~/Songs/song3.mp3");
            t2.PathBackground = "~/Songs/song1.mp3";
            t2.Singer = "Tung";




            ViewBag.id = t.Id;
            ViewBag.PathSongs = t.PathSongs;


            List<Songs> ListSongs = new List<Songs> { t ,t1,t2 };
            ViewBag.ListSongs = ListSongs;


            return View();
        }
    }
}