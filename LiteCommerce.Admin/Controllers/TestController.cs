using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LiteCommerce.Admin.Controllers
{
    public class TestController : Controller
    {
        


        // GET: Test
        public ActionResult Index()
        {
            
            string connectionString = "server=.;user id = sa;password=123;database=LiteCommerceDB";
            var dal = new LiteCommerce.DataLayers.SQLServer.SupplierDAL(connectionString);

            Product data = new Product()
            {
                ProductID=78,
                ProductName = "Desk",
                SupplierID =2,
                CategoryID=2,
                Unit= "10 boxes x 20 bags",
                Price ="18.00",
                Photo = "http://dummyimage.com/250x250.png/5fa2dd/ffffff&text=Product"
            };

            return Json(dal.List(1,5, "B"), JsonRequestBehavior.AllowGet);

        }
    }
}