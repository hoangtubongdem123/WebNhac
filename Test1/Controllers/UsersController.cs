using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Users()
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var users = db.Users.ToList();
            return View(users);
        }


        [HttpPost]
        public ActionResult CreateUser(string username, string password)
        {
            Users user = new Users();
            user.PassWord = password;
            user.UserName = username;
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            if (password != null && username != null) 
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Users");
        }

        public ActionResult DeleteUser(int id)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Users");
        }

        public ActionResult UpdateUser(int id, string username, string password) 
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
            var user = db.Users.Find(id);
            if (password != null && username != null)
            {
                user.PassWord = password;
                user.UserName = username;
                db.SaveChanges();
            }
            return RedirectToAction("Users");
        }

    }
}