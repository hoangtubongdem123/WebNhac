using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    public class TypesController : Controller
    {
        public ActionResult Types(string searchTerm, string sortColumn, string sortDirection, int page = 1, int pageSize = 10)
        {
            
            WebNgheNhacEntities1 db = new WebNgheNhacEntities1();

            
            var types = db.Types.AsQueryable();

            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                types = types.Where(t => t.TypeName.Contains(searchTerm));
            }

            
            if (!string.IsNullOrEmpty(sortColumn))
            {
                switch (sortColumn)
                {
                    case "TypeName":
                        types = (sortDirection == "asc") ? types.OrderBy(t => t.TypeName) : types.OrderByDescending(t => t.TypeName);
                        break;
                    default:
                        types = (sortDirection == "asc") ? types.OrderBy(t => t.Path_Type) : types.OrderByDescending(t => t.Path_Type);
                        break;
                }
            }

            
            int totalItems = types.Count(); 
            types = types.Skip((page - 1) * pageSize).Take(pageSize);

            
            ViewBag.TotalItems = totalItems; 
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page; 
            ViewBag.SortColumn = sortColumn; 
            ViewBag.SortDirection = sortDirection; 
            ViewBag.SearchTerm = searchTerm; 

            return View(types.ToList());
        }
    }
}
