using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class SuppliersController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Tìm kiểm và hiển thị danh sách các nhà cung cấp 
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int page = 1,string searchValue ="")
        {
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
            Supplier model = new Supplier()
            {
                SupplierID = 0

            };
            return View("Edit",model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Sữa Đổi";
            Supplier model = DataService.GetSupplier(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Xác Nhận Xóa";
            var model = DataService.GetSupplier(id);

            if (model == null)
            {
                return RedirectToAction("Index");
            }
            if (Request.HttpMethod == "GET")
            { 
                return View(model);
            }
            else
            { 
                DataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
       
        }

        public ActionResult Save(Supplier data)
        {
            // Kiem tra tinh hop le cua du lieu
            if(string.IsNullOrEmpty(data.SupplierName))
            {
                ModelState.AddModelError("SupplierName", "Tên Nhà Cung cấp không được để trống ");
            }
            if(string.IsNullOrEmpty(data.ContactName))
            {
                ModelState.AddModelError("ContactName", "Tên Liên Lạc không được để trống");
            }

            if (string.IsNullOrWhiteSpace(data.Address))
                data.Address = "";

            if (string.IsNullOrWhiteSpace(data.PostalCode))
                data.PostalCode = "";

            if (!ModelState.IsValid)
            {
                if(data.SupplierID == 0)
                {
                    ViewBag.Title = "Bo Sung Nha Cung Cap";
                }
                else
                {
                    ViewBag.Title = "Cap Nhat Nha Cung Cap";
                }

                return View("Edit", data);
            }

            if(data.SupplierID == 0)
            {
               
                DataService.AddSupplier(data);
            }
            else
            {
                
                DataService.UpdateSupplier(data);
            }

            return RedirectToAction("Index", "Suppliers");
        }


    }
}