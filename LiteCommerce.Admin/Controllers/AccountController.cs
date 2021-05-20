using LiteCommerce.Admin.Codes;
using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    public class AccountController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string userName = "",string password="")
        {
            if(Request.HttpMethod =="POST")
            {
                var account = AccountService.Authorize(userName,CryptHelper.Md5(password));
                if(account == null)
                {
                    ModelState.AddModelError("", "Thong Tin Dang nhap Sai");
                    return View();
                }
                /*return Json(account);*/

                /////ghi nhan phien dang nhap
                FormsAuthentication.SetAuthCookie(CookieHelper.AccountToCookieString(account), false);

                ///quay ve trang chu
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult ChangePassWord(string accountId ="", string oldPassword = "", string newPassword = "",  string confirmPassword = "")
        {

            if(newPassword == oldPassword && newPassword == confirmPassword)
            {
               
                ModelState.AddModelError("warning","mat khau moi khong duoc trung voi mat khau cu");
            }

            else if(newPassword == confirmPassword)
            {
                if (AccountService.ChangePassword(accountId, CryptHelper.Md5(oldPassword), CryptHelper.Md5(newPassword)))
                {
                 
                    ModelState.AddModelError("success","Thanh Cong");
                }
                else
                {
              
                    ModelState.AddModelError("warning","Mat khau cu khong dung");
                }
            }
            else
            {
              
                ModelState.AddModelError("warning", "Xac nhan mat khau khong chinh xac");
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");    
        }
    }
}