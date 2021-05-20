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
    public class ProductController : Controller
    {
        
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1, int category = 0, int supplier = 0, string searchValue = "" )
        {
            int rowCount = 0;
            int pageSize = 10;
            var model = ProductService.List(page, pageSize ,searchValue, category, supplier, out rowCount);

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.RowCount = rowCount;

            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
                pageCount += 1;
            ViewBag.PageCount = pageCount;
            
            return View(model);
        }



        /// <summary>
        /// Bổ Sung nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ Sung";
            ProductEX model = new ProductEX()
            {
                ProductID = 0
            };
            return View(model);
        }

        public ActionResult Edit(int id)
        {
           
            var model = ProductService.GetEx(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var modal = ProductService.Get(id);
            if(modal == null)
            {
                return View("Index");
            }
            else
            {
                ProductService.Delete(id);
                return RedirectToAction("Index", "Product");
            }

            
        }


        public ActionResult Details(int id)
        {
            var model = ProductService.GetEx(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }


        public ActionResult AddAttribues(int id)
        {
            int id1 = id;
            ViewBag.Title = "Them";
            ProductAttribute model = new ProductAttribute()
            {
                AttributeID = 0,
                ProductID = id1
            };
            return View("EditAttribues",model);
        }


        public ActionResult EditAttribues(int id)
        {
            ViewBag.Title = "Edit";
            ProductAttribute model = ProductService.GetAttribute(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        public ActionResult DeleteAttributes(long[] attributesIds, int id)
        {
            if (attributesIds == null || attributesIds.Length == 0)
                return RedirectToAction("Edit", new { id = id });
            ProductService.DeleteAttributes(attributesIds);
            return RedirectToAction("Edit", new { id = id });
        }

        


        public ActionResult AddGallery(int id)
        {
            ViewBag.Title = "Them";
            ProductGallery model = new ProductGallery()
            {
                ProductID = id,
                GalleryID = 0
            };
            return View("EditGallery", model);
        }


        public ActionResult EditGallery(int id)
        {
            ViewBag.Title = "Edit";
            ProductGallery model = ProductService.GetGallery(id);
            
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        public ActionResult DeleteGallery(long[] galleryIds, int id)
        {
            if (galleryIds == null || galleryIds.Length == 0)
                return RedirectToAction("Edit", new { id = id });
            ProductService.DeleteGalleries(galleryIds);
            return RedirectToAction("Edit", new { id = id });
        }


        public ActionResult SaveAttributes(ProductAttribute data)
        {

            //Kiểm Tra tính hợp lệ
            if (string.IsNullOrEmpty(data.AttributeName))
            {
                ModelState.AddModelError("AttributeName", "Error");
            }
            if (string.IsNullOrEmpty(data.AttributeValue))
            {
                ModelState.AddModelError("AttributeValue", "Error");
            }
            if (!ModelState.IsValid)
            {
                if (data.AttributeID == 0)
                {
                    ViewBag.Tittle = "Bổ Sung";
                }
                else
                {
                    ViewBag.Tittle = "Cập Nhật Nhà Cung Cấp";
                }
                return View("EditAttribues", data);
            }
            if (data.AttributeID == 0)
            {
                ProductService.AddAttribute(data);
            }
            else
            {
                ProductService.UpdateAttribute(data);
            }
            return RedirectToAction("Edit", new { id = data.ProductID });
        }



        public ActionResult SaveGallery(ProductGallery data)
        {
            //Kiểm Tra tính hợp lệ
            if (string.IsNullOrEmpty(data.Photo))
            {
                ModelState.AddModelError("Photo", "Error");
            }
            if (string.IsNullOrEmpty(data.Description))
            {
                data.Description = "";
            }
            if (!ModelState.IsValid)
            {
                if (data.GalleryID == 0)
                {
                    ViewBag.Tittle = "Bổ Sung";
                }
                else
                {
                    ViewBag.Tittle = "Cập Nhật Nhà Cung Cấp";
                }
                return View("EditGallery", data);
            }
            if (data.GalleryID == 0)
            {
                
                ProductService.AddGallery(data);
            }
            else
            {
                ProductService.UpdateGallery(data);
            }
            return RedirectToAction("Edit/"+data.ProductID+"", "Product");



        }



        public ActionResult SaveProduct(ProductEX data, HttpPostedFileBase uploadFile)
        {
            //Kiểm tra dữ liệu đầu vào
            if (data.CategoryID.ToString() == "0")
            {
                ModelState.AddModelError("CategoryID", "*");
            }

            if (data.SupplierID.ToString() == "0")
            {
                ModelState.AddModelError("SupplierID", "*");
            }

            if (string.IsNullOrEmpty(data.ProductName))
            {
                ModelState.AddModelError("ProductName", "**");
            }
            if (string.IsNullOrEmpty(data.Unit))
            {
                ModelState.AddModelError("QuantityPerUnit", "**");
            }

            
            if (string.IsNullOrEmpty(data.Price.ToString()))
            {
                ModelState.AddModelError("UnitPrice", "**");
            }
            if (string.IsNullOrEmpty(data.Photo))
            {
                data.Photo = "";
            }

          
            if (!ModelState.IsValid)
            {
                if (data.ProductID == 0)
                {
                    ViewBag.Title = "Create new a Product";
                }

                else
                {
                    ViewBag.Title = "Update a Product";
                }

                return View("Edit",data);
            }
            //: new update
            if (uploadFile != null)
            {
                //Tạo folder để upload ảnh lên Server
                string folder = Server.MapPath("~/Images/ProductUpload");

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
                data.Photo = fileName;
            }

            if (data.ProductID == 0)
            {
                ProductService.Add(data);
            }
            else
            {
                ProductService.Update(data);

            }

            return RedirectToAction("Index", "Product");
        }


        public ActionResult Input(Product data, HttpPostedFileBase uploadFile)
        {
            //Kiểm tra dữ liệu đầu vào
            if (data.CategoryID.ToString() == "0")
            {
                ModelState.AddModelError("CategoryID", "*");
            }

            if (data.SupplierID.ToString() == "0")
            {
                ModelState.AddModelError("SupplierID", "*");
            }

            if (string.IsNullOrEmpty(data.ProductName))
            {
                ModelState.AddModelError("ProductName", "**");
            }
            if (string.IsNullOrEmpty(data.Unit))
            {
                ModelState.AddModelError("QuantityPerUnit", "**");
            }


            if (string.IsNullOrEmpty(data.Price.ToString()))
            {
                ModelState.AddModelError("UnitPrice", "**");
            }
            if (string.IsNullOrEmpty(data.Photo))
            {
                data.Photo = "";
            }


            if (!ModelState.IsValid)
            {
                if (data.ProductID == 0)
                {
                    ViewBag.Title = "Create";
                }
                return View("Input", data);
            }
            //: new update
            if (uploadFile != null)
            {
                
                string folder = Server.MapPath("~/Images/ProductUpload");

               
                string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(uploadFile.FileName)}";
                

                string filePath = Path.Combine(folder, fileName);

                //Upload ảnh lên Server
                uploadFile.SaveAs(filePath);

                
                data.Photo = fileName;
            }
            if (data.ProductID == 0)
            {
                ProductService.Add(data);
                data.ProductID = ProductService.Add(data);
                
                

            }

            return RedirectToAction("Edit", new { id = data.ProductID });
        }

    }




}