using LiteCommerce.Admin.Models;
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
    public class CustomerController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer
        public ActionResult List(int page = 1, string searchValue = "", int id = 0)
        {

            int pageSize = 10;
            int rowCount = 0;

            List<Customer> customers = DataService.ListCustomers(page, pageSize, searchValue, out rowCount);
            Models.BasePaginationQueryResult model = new Models.CustomerPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = customers,

            };
            return View(model);
        }



        public ActionResult Add()

        {
            try
            {
                ViewBag.Title = "Them";
                Customer model = new Customer()
                {
                    CustomerID = 0
                };
                return View("Edit", model);
            }
            catch (Exception ex)
            {
                return Content(ex.Message + " : " + ex.StackTrace);
            }

        }


        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Cap Nhat";
            Customer model = DataService.GetCustomer(id);

            if (model == null)
            {
                return Redirect("Index");
            }

            return View(model);
        }

        public ActionResult GetId(int id)
        {
            Customer getID = DataService.GetCustomer(id);
            return RedirectToAction("Index", getID);
        }

        public ActionResult Delete(int id)
        {
            if (DataService.DeleteCustomer(id) == true)
            {

                SetAlert("Delete Customers success !", "success");
            }
            else
            {
                SetAlert("Delete Customers fail, Because of the associated constraints  !", "danger");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Save(Customer data)
        {
            /*if(string.IsNullOrWhiteSpace)*/
            if (string.IsNullOrEmpty(data.ContactName))
            {
                ModelState.AddModelError("ContactName", "Tên Liên Lạc không được để trống");
            }
            if (string.IsNullOrEmpty(data.CustomerName))
            {
                ModelState.AddModelError("CustomerName", "Tên Khách hàng không được trống");
            }

            if(string.IsNullOrEmpty(data.Address))
            {
                data.Address = "";
            }

            if (string.IsNullOrEmpty(data.PostalCode))
            {
                data.PostalCode = "";
            }


            if (!ModelState.IsValid)
            {
                if (data.CustomerID == 0)
                {
                    ViewBag.Title = "Bo Sung Nha Cung Cap";
                }
                else
                {
                    ViewBag.Title = "Cap nhat nha cung cap";
                }
                return View("Edit", data);
            }


            if (data.CustomerID == 0)
            {
                DataService.AddCustomer(data);
                SetAlert("Them Nha Cung Cap Thanh Cong","Success");
            }
            else
            {
                DataService.UpdateCustomer(data);
            }
            return RedirectToAction("Index", "Customer");
        }

    }

}