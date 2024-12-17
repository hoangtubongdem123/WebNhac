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



        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            //using (WebNgheNhacEntities1 db = new WebNgheNhacEntities1())
            using (WebNgheNhacEntities1 db = new WebNgheNhacEntities1())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == username && u.PassWord == password);
                if (user == null)
                {
                    int status = 1;
                    ViewBag.Status = status;

                    return View("Login");
                    //return RedirectToAction("Login", "Account");

                    //Response.Write("<script>alert('login failed');</script>");

                }



                
                Session["UserID"] = user.ID_User;
                Session["UserName"] = user.UserName;
                Session["Level"] = user.Level;

                int? sessionLevel = Session["Level"] as int?;

                if(sessionLevel >1){
                    return RedirectToAction("Dashboard", "Home");
                }

                // Redirect to Home/Index
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Register View
        public ActionResult Register()
        {
            return View();
        }




        public ActionResult Account()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            using (WebNgheNhacEntities1 db = new WebNgheNhacEntities1())
            {
               
                if (db.Users.Any(u => u.UserName == username))
                {
                    TempData["ErrorMessage"] = "Tài khoản đã tồn tại."; 
                    return RedirectToAction("Register", "Account");
                }

               
                db.Users.Add(new Users
                {
                    UserName = username,
                    PassWord = password,
                    Level = 1

                });
                db.SaveChanges();

                TempData["SuccessMessage"] = "Tài khoản đã được tạo thành công!";

                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session.Clear(); // Clear all session data
            return RedirectToAction("Login", "Account");
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