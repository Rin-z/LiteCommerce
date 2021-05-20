using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
     public static class SelectListHelper
    {
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in DataService.ListCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }
            return list;
        }
        public static List<SelectListItem> Cities()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in DataService.ListCities())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CityName,
                    Text = item.CityName
                });
            }

            return list;
        }

        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var item in DataService.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName
                });
            }

            return list;
        }


        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListOfSupplier())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName
                });
            }

            return list;
        }

        public static List<SelectListItem> Products()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in ProductService.List())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.ProductID.ToString(),
                    Text = item.ProductName
                });
            }

            return list;
        }


        public static List<SelectListItem> OrderStatus()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach(var item in OrderService.ListOrderStatus())
            {
                listItems.Add(new SelectListItem()
                {
                    Value = item.Status.ToString(),
                    Text = item.Description
                });
            }
            return listItems;
        }

    }
}