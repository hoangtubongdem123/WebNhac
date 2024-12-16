using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Test1.Models;
using PagedList;

namespace Test1.Controllers
{
    public class TypesController : Controller
    {
        private WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
        public ActionResult Types(string sortOrder, string searchString, int? page, int? size)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.IDSortParm = sortOrder == "id" ? "id_desc" : "id";

            ViewBag.CurrentFilter = searchString;

            var types = from s in db.Types select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                types = types.Where(s => s.TypeName.Contains(searchString));
            }

            switch (sortOrder)
            {
                default:
                    goto case "id";
                case "id":
                    types = types.OrderBy(s => s.ID_Type);
                    break;
                case "id_desc":
                    types = types.OrderByDescending(s => s.ID_Type);
                    break;
                case "name_desc":
                    types = types.OrderByDescending(s => s.TypeName);
                    break;
                case "name":
                    types = types.OrderBy(s => s.TypeName);
                    break;
            }

            int pageSize = size ?? 7;
            int pageNumber = page ?? 1;

            return View(types.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult CreateType(string typeName, HttpPostedFileBase imageFile)
        {
            Types type = new Types()
            {
                TypeName = typeName,
            };

            var fileName = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(Server.MapPath("~/TypeBackGround"), fileName);

            imageFile.SaveAs(filePath);

            type.Path_Type = "/TypeBackGround/" + fileName;

            if (!string.IsNullOrEmpty(typeName))
            {
                db.Types.Add(type);
                db.SaveChanges();
            }
            return RedirectToAction("Types");
        }


        [HttpPost]
        public ActionResult UpdateType(int id, HttpPostedFileBase imageFile)
        {
            var type = db.Types.Find(id);
            if (type != null)
            {
                db.SaveChanges();
                if (imageFile != null)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/TypeBackGround"), fileName);

                    imageFile.SaveAs(filePath);

                    type.Path_Type = "/TypeBackGround/" + fileName;
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Types");
                }
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Types");
        }


        public ActionResult DeleteType(int id)
        {
            var type = db.Types.Find(id);
            if (type != null)
            {
                db.Types.Remove(type);
                db.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Types");
        }
    }
}
