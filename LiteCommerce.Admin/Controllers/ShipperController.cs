using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
/*    [Authorize(Roles = WebUserRoles.ACCOUNTANT)]*/
    [Authorize]
    public class ShipperController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            //Muốn sử dụng out bắt buộc phải chỉ định giá trị trước khi sử dụng
            List<Shipper> listOfShipper = DataService.ListShippers(page, pageSize, searchValue, out rowCount);
            var model = new Models.ShipperPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue= searchValue,
                Data = listOfShipper
            };
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Create new a Shipper";
                    Shipper newShipper = new Shipper()
                    {
                        ShipperId = 0 // do id tu sinh
                    };
                    return View(newShipper);
                }
                else
                {
                    ViewBag.Title = "Edit a Shipper";
                    Shipper editShipper = DataService.GetShipper(Convert.ToInt32(id));
                    if (editShipper == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(editShipper);
                }

            }
            catch (Exception ex)
            {
                // ko nen lam dieu nay => tranparent error
                return Content(ex.Message + " : " + ex.StackTrace);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipperIDs"></param>
        /// <returns></returns>
 
        public ActionResult Delete(int id = 0)
        {
            if (DataService.DeleteShipper(id) == true)
            {
                SetAlert("Delete shippers success !", "success");
            }
            else
            {
                SetAlert("Delete shippers fail, Because of the associated constraints  !", "danger");
            }

            return RedirectToAction("Index");
        }


        /// <summary>
        /// su dung ten lop lam tham so do cac fiel trung 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Shipper model)
        {   // lam that nho them try cacht

            // : Kiểm tra tính hợp lệ của dữ liệu được nhập
            if (string.IsNullOrEmpty(model.ShipperName))
            {
                ModelState.AddModelError("ShipperName", "*");
            }

            if (string.IsNullOrEmpty(model.Phone))
            {
                ModelState.AddModelError("Phone", "*");
            }

            if (!ModelState.IsValid)
            {
                if (model.ShipperId == 0)
                {
                    ViewBag.Title = "Create new a shipper";
                    SetAlert("Add new Shipper Fail , Check again!", "warning");
                }

                else
                {
                    ViewBag.Title = "Update a shipper";
                    SetAlert("Update Shipper Fail , Check again!", "warning");
                }

                return View(model);
            }

            //: Lưu dữ liệu vào DB
            if (model.ShipperId == 0)
            {
                SetAlert("Add new Shipper success !", "success");
                DataService.AddShipper(model);
            }
            else
            {
                SetAlert("Update Shipper success !", "success");
                DataService.UpdateShipper(model);
            }

            return RedirectToAction("Index");

        }


    }
}