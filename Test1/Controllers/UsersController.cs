using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using Test1.Models; // Replace with the namespace where your model1.edmx entities are located

namespace Test1.Controllers
{
    public class UsersController : Controller
    {
        private WebNgheNhacEntities1 db = new WebNgheNhacEntities1(); // EF DbContext generated from model1.edmx

        // GET: Users
        public ActionResult Index(string sortOrder, string searchString, int? page, int? size)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            ViewBag.IDSortParm = sortOrder == "id" ? "id_desc" : "id";

            ViewBag.CurrentFilter = searchString;

            // Query users with LINQ
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
                    users = users.OrderBy(u => u.UserName); // Default sorting
                    break;
            }

            // Set page size (default 5) and page number (default 1)
            int pageSize = size ?? 5;
            int pageNumber = page ?? 1;

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: Users/CreateUser
        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: Users/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "ID_User,UserName,PassWord")] Users user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user); // Add new user using EF
                db.SaveChanges(); // Save changes
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/EditUser/5
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users user = db.Users.Find(id); // Fetch user by ID using EF
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/EditUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "ID_User,UserName,PassWord")] Users user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified; // Mark entity as modified
                db.SaveChanges(); // Save changes
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/DeleteUser/5
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Users user = db.Users.Find(id); // Fetch user by ID using EF
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/DeleteUser/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user); // Remove user using EF
                db.SaveChanges(); // Save changes
            }
            return RedirectToAction("Index");
        }

        // Dispose the context
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose(); // Dispose the DbContext properly
            }
            base.Dispose(disposing);
        }
    }
}
