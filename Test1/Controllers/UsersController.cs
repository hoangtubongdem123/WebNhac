﻿using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using Test1.Models;

namespace Test1.Controllers
{
    public class UsersController : Controller
    {
        private WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

        public ActionResult Index(string sortOrder, string searchString, int? page, int? size)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            ViewBag.IDSortParm = sortOrder == "id" ? "id_desc" : "id";

            ViewBag.CurrentFilter = searchString;

            var users = from u in db.Users select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id":
                    users = users.OrderBy(u => u.ID_User);
                    break;
                case "id_desc":
                    users = users.OrderByDescending(u => u.ID_User);
                    break;
                case "username_desc":
                    users = users.OrderByDescending(u => u.UserName);
                    break;
                default:
                    users = users.OrderBy(u => u.UserName);
                    break;
            }

            int pageSize = size ?? 5;
            int pageNumber = page ?? 1;

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "ID_User,UserName,PassWord")] Users user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "ID_User,UserName,PassWord")] Users user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
