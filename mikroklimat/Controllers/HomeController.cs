using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mikroklimat.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult About()
        {
            return PartialView();
        }

        public PartialViewResult Projects()
        {
            mikroklimat.Classes.Db_Connection md = new Classes.Db_Connection();
            return PartialView(md.mde.project.OrderByDescending(x=>x.id).Take(3).ToList());
        }

        public PartialViewResult Send_Message()
        {
            return PartialView();
        }
    }
}