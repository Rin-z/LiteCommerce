using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Bổ Sung nhà cung cấp
        /// </summary>
        /// <returns></returns>

        public ActionResult Add()
        {
            ViewBag.Title = "Bổ Sung";
            return View("Edit");
        }

        public ActionResult Edit(string Id)
        {
            ViewBag.Title = "Sữa Đổi";
            return View();
        }

        public ActionResult Delete(string Id)
        {
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Save()
        {
            return RedirectToAction("Index", "Product");
        }
    }


}