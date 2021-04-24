using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View("Index");
        }

        public ActionResult Edit(string Id)
        {
            return View("Index");
        }

        public ActionResult Delete()
        {
            return RedirectToAction("Index", "Categories");
        }


        public ActionResult Save()
        {
            return RedirectToAction("Index", "Categories");
        }
    }
}