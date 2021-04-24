using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 25;
            int rowCount = 0;
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


        public ActionResult Detail(string Id)
        {
            return View();
        }

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
            return RedirectToAction("Index", "Employees");
        }

        public ActionResult Save()
        {
            return RedirectToAction("Index", "Employees");
        }
    }
}