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

            //WebNgheNhacEntities db = new WebNgheNhacEntities();

            //List<Songs> t = db.Songs.ToList();
            //ViewBag.ListSongs = t;

            Song t = new Song();
            t.Id = 123;
            t.Name = "Never Gonna Give You Up";
            t.PathSongs = Url.Content("~/Songs/song1.mp3");
            t.PathBackground = Url.Content("~/img/909edb5e516afbcd5b007d19ecfd5897.jpg");
            t.Singer = "Tung";



            Song t1 = new Song();
            t1.Id = 124;
            t1.Name = "Let Me Go";
            t1.PathSongs = Url.Content("~/Songs/song2.mp3");
            
            t1.Singer = "Ha Quang Hoa ";

            Song t2 = new Song();
            t2.Id = 124;
            t2.Name = "Dark Soul";
            t2.PathSongs = Url.Content("~/Songs/song3.mp3");
           
            t2.Singer = "Tung";


            List<Song> ListSongs = new List<Song> { t , t1 , t2 };
            ViewBag.ListSongs = ListSongs;

            return View();
        }


    public ActionResult Text() { 
            
            
            
            return View(); }


    }
}