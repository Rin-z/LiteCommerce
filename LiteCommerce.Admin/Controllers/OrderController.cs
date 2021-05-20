using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string sstartDate, string sendDate, int page = 1, string searchValue = "", int status = 0)
        {
            DateTime startDate = new DateTime(), endDate = new DateTime();
            if (string.IsNullOrEmpty(sstartDate)) 
                startDate = new DateTime(1800, 11, 11); 
            else startDate = Convert.ToDateTime(sstartDate);
            if (string.IsNullOrWhiteSpace(sendDate)) 
                endDate = new DateTime(9999, 11, 11); 
            else endDate = Convert.ToDateTime(sendDate);

            int rowCount = 0;
            int pageSize = 10;
            var data = OrderService.ListOrders(page, pageSize, searchValue, status, startDate, endDate, out rowCount);

            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize != 0) pageCount += 1;

            Models.OrderPainatonQueryResult model = new Models.OrderPainatonQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data
            };
            //return Json(data, JsonRequestBehavior.AllowGet);
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            return View();
        }


        public ActionResult Add()
        {
            ViewBag.Title = "Bổ Sung";
            OrderEx model = new OrderEx()
            {
                OrderID = 0
            };
            return View(model);
        }


        public ActionResult Edit()
        {
            return View();
        }

    }
}