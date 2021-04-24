using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class SuppliersController : Controller
    {
        /// <summary>
        /// Tìm kiểm và hiển thị danh sách các nhà cung cấp 
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Index(int page = 1,string searchValue ="")
        {
            /*          int pageSize = 25;
                        int rowCount = 0;
                        List<Supplier> suppliers = DataService.ListSupplier(page, pageSize, searchValue, out rowCount);
                        int pageCount = rowCount / 25;
                        if (rowCount % pageSize > 0)
                            pageCount += 1;
                        ViewBag.Page = page;
                        ViewBag.RowCount = rowCount;
                        ViewBag.PageCount = pageCount;*/

            int pageSize = 10;
            int rowCount;
            List<Supplier> suppliers = DataService.ListSupplier(page, pageSize, searchValue, out rowCount);
            Models.BasePaginationQueryResult model = new Models.SupplierPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = suppliers

            };
            return View(model);
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
            return View();
        }

        public ActionResult Save()
        {
            return RedirectToAction("Index", "Suppliers");
        }
    }
}