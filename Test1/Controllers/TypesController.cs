using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Test1.Models;

namespace Test1.Controllers
{
    public class TypesController : Controller
    {

        [HttpGet]
        public ActionResult Types(string searchTerm)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();


            var types = string.IsNullOrEmpty(searchTerm)
                        ? db.Types.ToList()
                        : db.Types.Where(t => t.TypeName.Contains(searchTerm)).ToList();

            return View(types);
        }


        [HttpPost]
        public ActionResult CreateType(string typeName, HttpPostedFileBase imageFile)
        {
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
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
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
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
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();
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
