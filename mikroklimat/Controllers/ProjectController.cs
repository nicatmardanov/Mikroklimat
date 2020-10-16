using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using mikroklimat.Models;

namespace mikroklimat.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project

        public ActionResult Index()
        {
            mikroklimat.Classes.Db_Connection db_c = new Classes.Db_Connection();
            return View(db_c.mde.project.OrderByDescending(x=>x.id).ToList());
        }

 

        [Authorize]
        public ActionResult Add_Project()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add_Project(string name, string[] pics, string[] pic_ext)
        {
            mikroklimat.Classes.Db_Connection db_c = new Classes.Db_Connection();
            project pr = new project();
            pr.name = name;
            pr.add_date = DateTime.Now;
            user add_us = db_c.mde.user.ToList().FirstOrDefault(x => x.username == User.Identity.Name);
            pr.add_user = add_us.id;

            db_c.mde.project.Add(pr);
            db_c.mde.SaveChanges();

            if (pics!=null)
            {
                for (int i = 0; i < pics.Length; i++)
                {
                    string data = @pics[i];
                    var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                    var binData = Convert.FromBase64String(base64Data);

                    using (var stream = new MemoryStream(binData))
                    {
                        Bitmap img = new Bitmap(stream);
                        string name_path = "/Content/img/large_" + pr.id + "_" + Guid.NewGuid() + pic_ext[i];
                        img.Save(Server.MapPath(name_path));

                        //Bitmap bm = new Bitmap(stream);
                        img = new Bitmap(stream);
                        mikroklimat.Classes.imgCrop icrop = new mikroklimat.Classes.imgCrop(ref img);
                        string s_name_path = "/Content/img/small_" + pr.id + "_" + Guid.NewGuid() + pic_ext[i];
                        //Image new_img = bm;
                        img.Save(Server.MapPath(s_name_path));

                        image db_img = new image();
                        db_img.path = name_path;
                        db_img.small_img_path = s_name_path;
                        db_img.project_id = pr.id;

                        db_c.mde.image.Add(db_img);
                        db_c.mde.SaveChanges();
                    }
                }
            }
            return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);
        }



        public JsonResult Project_Pics(int id)
        {
            mikroklimat.Classes.Db_Connection db_c = new Classes.Db_Connection();
            db_c.mde.Configuration.ProxyCreationEnabled = false;
            return Json(new { pics = db_c.mde.image.Where(x => x.project_id == id).ToList() }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Project_Modal()
        {
            return PartialView();
        }
    }
}