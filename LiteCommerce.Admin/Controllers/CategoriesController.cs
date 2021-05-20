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
    public class CategoriesController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult List(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            //Muốn sử dụng out bắt buộc phải chỉ định giá trị trước khi sử dụng
            List<Category> listOfCategories = DataService.ListCategories(page, pageSize, searchValue, out rowCount);
            var model = new Models.CategoryPaginationQueryResult
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfCategories
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
                    ViewBag.Title = "Tạo Mới Loại Hàng";
                    Category newCategory = new Category()
                    {
                        CategoryID = 0 // do id tu sinh
                    };
                    return View(newCategory);
                }
                else
                {
                    ViewBag.Title = "Cập Nhật Loại Hàng";
                    Category editCategory = DataService.GetCategory(Convert.ToInt32(id));
                    if (editCategory == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(editCategory);
                }

            }
            catch (Exception ex)
            {
                // ko nen lam dieu nay => tranparent error
                return Content(ex.Message + " : " + ex.StackTrace);
            }
        }


        /// <summary>
        /// su dung ten lop lam tham so do cac fiel trung 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Category model)
        {   // lam that nho them try catch

            // : Kiểm tra tính hợp lệ của dữ liệu được nhập
            if (string.IsNullOrEmpty(model.CategoryName))
            {
                ModelState.AddModelError("CategoryName", "*");
            }

            if (string.IsNullOrEmpty(model.Description))
            {
                model.Description = "";
            }

            if (!ModelState.IsValid)
            {

                if (model.CategoryID == 0)
                {
                    ViewBag.Title = "Create new a Category";
                    SetAlert("Add new Category Fail , Check again!", "warning");
                }

                else
                {
                    ViewBag.Title = "Update a Category";
                    SetAlert("Update Category Fail , Check again!", "warning");
                }
                return View(model);
            }

            //: Lưu dữ liệu vào DB
            if (model.CategoryID == 0)
            {
                DataService.AddCategory(model);
                SetAlert("Add new Category success !", "success");
            }
            else
            {
                DataService.UpdateCategory(model);
                SetAlert("Update Category success !", "success");
            }
            return RedirectToAction("Index");

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryIDs"></param>
        /// <returns></returns>

        public ActionResult Delete(int id)
        {
            if (DataService.DeleteCategory(id) == true)
            {
                SetAlert("Delete Categories success !", "success");
            }
            else
            {
                SetAlert("Delete Countries fail, Because of the associated constraints  !", "danger");
            }
            return RedirectToAction("Index");
        }
    }
}