using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        // GET: Employees

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount;
            List<Employee> employees = DataService.ListEmployees(page, pageSize, searchValue, out rowCount);
            Models.BasePaginationQueryResult model = new Models.EmployeePaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = employees
            };
            return View(model);
        }


        public ActionResult Detail(int id)
        {
            ViewBag.Title = "Thong Tin Nhan Vien";
            Employee model = DataService.GetEmployee(id);
            if (model == null)
            {
                RedirectToAction("Index");
            }
            if (Request.HttpMethod == "GET")
            {
                return View(model);
            }
            return View();
        }

        public ActionResult Add()
        {
            ViewBag.Title = "Bổ Sung";
            return View("Edit");
        }

        /// <summary>
        /// Input
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
                    ViewBag.Title = "Create new a Employee";
                    Employee newEmployee = new Employee()
                    {
                        EmployeeID = 0 // do id tu sinh
                    };
                    return View(newEmployee);
                }
                else
                {
                    ViewBag.Title = "Edit a Employee";
                    Employee editEmployee = DataService.GetEmployee(Convert.ToInt32(id));
                    if (editEmployee == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(editEmployee);
                }

            }
            catch (Exception ex)
            {
                // ko nen lam dieu nay => tranparent error
                return Content(ex.Message + " : " + ex.StackTrace);
            }
        }

        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Xác Nhận Xóa";
            var model = DataService.GetEmployee(id);

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
                DataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Input(Employee model, HttpPostedFileBase uploadFile, string detail, string updateProfile)
        {   // lam that nho them try catch


            if (string.IsNullOrEmpty(model.FirstName))
            {
                ModelState.AddModelError("FirstName", "**");
            }

            if (string.IsNullOrEmpty(model.LastName))
            {
                ModelState.AddModelError("LastName", "**");
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError("Email", "**");
            }

            if ((model.BirthDate) == null)
            {
                ModelState.AddModelError("BirthDate", "**");
            }


  

    
            if (string.IsNullOrEmpty(model.Notes))
            {
                model.Notes = "";
            }
            if (string.IsNullOrEmpty(model.Photo))
            {
                model.Photo = "";
            }




            if (!ModelState.IsValid)
            {

                if (model.EmployeeID == 0)
                {
                    ViewBag.Title = "Create new a Employee";
                    
                }

                else
                {
                    ViewBag.Title = "Update a Employee";
                    
                }
                return View(model);
            }


            if (uploadFile != null)
            {
                //Tạo folder để upload ảnh lên Server
                string folder = Server.MapPath("~/Images/Upload");

                // string fileName = uploadFile.FileName;  => dễ bị trùng khi client chọn lại
                // => Solution : Sinh fileName theo giây phút để tránh trùng lặp
                //c1 : Cách ghép chuổi fileName
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(uploadFile.FileName)}";
                //cach 2
                // string fileName1 = string.Format("{0}{1}" ,DateTime.Now.Ticks,Path.GetExtension(uploadFile.FileName));

                string filePath = Path.Combine(folder, fileName);

                //Upload ảnh lên Server
                uploadFile.SaveAs(filePath);

                //  return content|redirectoaction()
                // return Json (model, JsonRequestBehavior.AllowGet);

                //Gán dữ liệu cho PhotoPath
                model.Photo = fileName;
            }

            if (model.EmployeeID == 0)
            {
                DataService.AddEmployee(model);
                
            }
            else if (detail == "detail")
            {
                DataService.UpdateEmployee(model);
               
                return RedirectToAction("Index", "Account", new { id = @model.EmployeeID });
            }
            else if (updateProfile == "updateProfile")
            {
                DataService.UpdateEmployee(model);
               
                return RedirectToAction("Index", "Account", new { id = @model.EmployeeID });
            }
            else
            {
                
                DataService.UpdateEmployee(model);
            }
            return RedirectToAction("Index");

        }
    }
}