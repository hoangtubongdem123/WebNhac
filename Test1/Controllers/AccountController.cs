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
                return RedirectToAction("Library", "Library");
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

        public ActionResult ChangePasswordHandle(string UserName, string oldPassword, string newPassword, string cfmPassword)
        {
            if (Session["UserId"] == null)
            {

                ViewBag.ErrorMessage = "vui long dang nhap";
                return View("Account");
            }
            using (WebNgheNhacEntities1 db = new WebNgheNhacEntities1())
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(cfmPassword))
                {
                    ViewBag.ErrorMessage = "Vui lòng điền đầy đủ thông tin.";
                    return View("Account");
                }

                if (newPassword != cfmPassword)
                {
                    ViewBag.ErrorMessage = "Mật khẩu xác nhận không khớp.";
                    return View("Account");
                }

                var user = db.Users.SingleOrDefault(u => u.UserName == UserName);
                if (user == null)
                {
                    ViewBag.ErrorMessage = "Người dùng không tồn tại.";
                    return View("Account");
                }

                if (user.PassWord != oldPassword)
                {
                    ViewBag.ErrorMessage = "Mật khẩu cũ không đúng.";
                    return View("Account");
                }
                user.PassWord = newPassword;
                db.SaveChanges();

            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult login2()
        {


           
            Session["UserName"] = "Khách";



            return RedirectToAction("Library", "Library");
        }



    }


}