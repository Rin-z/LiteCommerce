using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Dinh nghia cac phep xy ly du lieu lien quan den mat hang 
    /// </summary>
    public interface IProductDAL
    {
        /// <summary>
        /// Lay Danh Sach Cua Product (tim kiem/loc du lieu , phan trang)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// /// <param name="CategoryId">Ma loai hang can lay (neu 0 lay tat ca cac loai hang)</param>
        /// /// <param name="SupplierId">Ma nha cung cap ( neu 0 lay tat ca cac nha cung cap)</param>
        /// <param name="searchValue">Ten mat hang can tim kiem</param>
        /// <returns></returns>
        List<Product> List(int page, int pageSize, string searchValue,int CategoryId,int SupplierId);




        /// <summary>
        /// lay thong tin 1 Product
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        Product Get(int productID);

        ProductEX GetEX(int productID);

        /// <summary>
        /// bo sung 1 Product,ham tra ve ma cua Product duoc bo sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);

                /// <summary>
        /// Dem so luong Product
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(int categoryId, int supplierId, string searchValue);

        /// <summary>
        /// cap nhat thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);

        /// <summary>
        /// xoa thong tin cua 1 nha cung cap
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        bool Delete(int productId);

        /// <summary>
        /// danh sach thuoc tinh cua mat hang
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductAttribute> ListAttributes(int productId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        ProductAttribute GetAttribute(long attributeID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long Addtrribute(ProductAttribute data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateAddtrribute(ProductAttribute data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        bool DeleAddtrribute(long attributeID);

        /// <summary>
        /// Danh Sach thu vien anh cua mat hang
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        List<ProductGallery> ListGalleries(int productId);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryID"></param>
        /// <returns></returns>
        ProductGallery GetGallery(long galleryID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddGallery(ProductGallery data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateGallery(ProductGallery data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryID"></param>
        /// <returns></returns>
        bool DeleteGallery(long galleryID);


    }
}
