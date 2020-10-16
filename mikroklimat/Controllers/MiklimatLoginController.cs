using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using mikroklimat.Models;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace mikroklimat.Controllers
{
    public class MiklimatLoginController : Controller
    {
        // GET: MiklimatLogin
        mikroklimat.Classes.Db_Connection mdb = new Classes.Db_Connection();

        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(user us)
        {
            if (User.Identity.IsAuthenticated)
                return Json(new { res = "2" }, JsonRequestBehavior.AllowGet);
            else
            {
                string uname = Classes.Crypto.Encrypt(us.username);
                string pass = Classes.Crypto.Encrypt(us.password);
                user u = mdb.mde.user.FirstOrDefault(x => x.username == uname && x.password == pass);
                if (u != null)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, u.username, DateTime.Now, DateTime.Now.AddMonths(1), true, "admin", FormsAuthentication.FormsCookiePath);

                    HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName);
                    if (ticket.IsPersistent)
                    {
                        ck.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(ck);
                    FormsAuthentication.SetAuthCookie(u.username, true);
                    FormsAuthentication.RedirectFromLoginPage(u.username, true);
                    return Json(new { res = "1" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { res = "0" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Forgot_Password(string usm)
        {
            usm = Classes.Crypto.Encrypt(usm);
            user us = mdb.mde.user.FirstOrDefault(x => x.username == usm || x.email == usm);
            if (us != null)
            {
                Classes.e_mail em = new Classes.e_mail(us);
                return Json(new { res = "1" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { res = "0" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MiRegister()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Check_UserName(string username)
        {
            username = Classes.Crypto.Encrypt(username);
            user us = mdb.mde.user.FirstOrDefault(x => x.username == username);
            if (us == null)
                return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Register(user us)
        {
            user u = new user();
            u.username = Classes.Crypto.Encrypt(us.username);
            u.name = Classes.Crypto.Encrypt(us.name);
            u.surname = Classes.Crypto.Encrypt(us.surname);
            u.password = Classes.Crypto.Encrypt(us.password);
            u.email = Classes.Crypto.Encrypt(us.email);

            mdb.mde.user.Add(u);
            mdb.mde.SaveChanges();
        }

        public ActionResult LogOut(string redirectUrl)
        {
            FormsAuthentication.SignOut();
            return Redirect(redirectUrl);
        }

    }
}