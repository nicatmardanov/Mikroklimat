using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mikroklimat.Models;

namespace mikroklimat.Controllers
{
    public class MessageController : Controller
    {
        mikroklimat.Classes.Db_Connection db_c = new Classes.Db_Connection();
        // GET: Message
        [HttpPost]
        public void Send_Message(string name, string email, string message)
        {
            message message_inst = new message();
            message_inst.name = name;
            message_inst.email = email;
            message_inst.message1 = message;
            message_inst.date = DateTime.Now;


            db_c.mde.message.Add(message_inst);
            db_c.mde.SaveChanges();

        }

        [Authorize]
        public ActionResult Show_All_Messages()
        {
            return View(db_c.mde.message.ToList());
        }
        public PartialViewResult MessageModal()
        {
            return PartialView();
        }
    }
}