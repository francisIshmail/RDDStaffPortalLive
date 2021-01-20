using DocumentFormat.OpenXml.Drawing.Charts;
using Newtonsoft.Json;
using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.DataModels.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    public class WelcomeMessageController : Controller
    {
        RDD_Welcome_MessageDBOperation rDD_Welcome_MessageDB = new RDD_Welcome_MessageDBOperation();
        // GET: Admin/WelcomeMessage
        public ActionResult Index()
        {
            return View();
        }
        [Route("AddWelcome_Message")]
        public ActionResult AddWelcome_Message(int Welcome_id = -1)
        {
            RDD_Welcome_Message rDD_Welcome = new RDD_Welcome_Message();
            rDD_Welcome.Saveflag = false;
            rDD_Welcome.Loginid = User.Identity.Name;
            rDD_Welcome.Loginon = System.DateTime.Now;




            if (Welcome_id != -1)
            {

                rDD_Welcome = rDD_Welcome_MessageDB.GetData(Welcome_id);
            }
            else
            {
                rDD_Welcome.EditFlag = false;
                rDD_Welcome.Welcome_image1 = Server.MapPath("/Images/TempLogo/defaultimg.jpg");
                byte[] file;
                using (var stream = new FileStream(rDD_Welcome.Welcome_image1, FileMode.Open, FileAccess.Read))
                {
                    using (var reader1 = new BinaryReader(stream))
                    {
                        file = reader1.ReadBytes((int)stream.Length);
                    }
                }
                rDD_Welcome.Welcome_image = file;
            }

            return PartialView("~/Areas/Admin/Views/WelcomeMessage/ADDWelcome_Message.cshtml", rDD_Welcome);
        }

        public JsonResult Uploadfile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var _ext = Path.GetExtension(pic.FileName);
                    if (_ext.ToLower() != ".jpeg" && _ext.ToLower() != ".jpg" && _ext.ToLower() != ".png" && _ext.ToLower() != ".bmp")
                        return Json("InvalidError", JsonRequestBehavior.AllowGet);
                    var fileName = Path.GetFileName(pic.FileName);


                    // _imgname = DateTime.Now.ToString("ddMMyyyyhhmmss");
                    var _comPath = Server.MapPath("/Images/TempLogo/Temp") + "user_" + _imgname + "Abc" + _ext;
                    _imgname = "Tempuser_" + "Abc" + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    string[] files = System.IO.Directory.GetFiles(Server.MapPath("/Images/TempLogo"), "Temp" + "Abc" + ".*");
                    foreach (string f in files)
                    {
                        System.IO.File.Delete(f);
                    }

                    Session["FILE1"] = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    //if (img.Width > 400)
                    //    img.Resize(400, 400);
                    img.Save(_comPath);
                    // end resize
                }
            }

            return Json(_imgname, JsonRequestBehavior.AllowGet);
        }

        [Route("DeleteNotifyActivity")]
        public ActionResult DeleteNotifyActivity(int Welcome_id)
        {
            
            return Json(new { data = rDD_Welcome_MessageDB.DeleteActivity(Welcome_id) }, JsonRequestBehavior.AllowGet );
        }

        [Route("ReminderActivity")]
         public ActionResult ReminderActivity(RDD_Welcome_Message_User rdd)
        {
            rdd.UserName = User.Identity.Name;
            return Json(new { data = rDD_Welcome_MessageDB.InsertUserrActivity(rdd) },JsonRequestBehavior.AllowGet);                    
        }

       [Route("SaveWelcomeMessage")]
        public ActionResult SaveWelcomeMessage(RDD_Welcome_Message rDD_Welcome)
        {
            //if (rDD_Welcome.Welcome_image.Length>0 || rDD_Welcome.Welcome_image == null)
            //{
            //   rDD_Welcome.imgbool = false;
            //}
            //else
            //{
            //   rDD_Welcome.imgbool = true;
            //}
            string TempPath = (string)Session["FILE1"];
            
                if (TempPath != null && TempPath != "")
                {
                    rDD_Welcome.Welcome_image1 = TempPath;
                }
                else
                {
                    rDD_Welcome.Welcome_image1 = Server.MapPath("/Images/TempLogo/defaultimg.jpg");
                }
            rDD_Welcome.Loginon = System.DateTime.Now;
            rDD_Welcome.Loginid = User.Identity.Name;
          
            Session["FILE1"] = null;

            return Json(new { Data =rDD_Welcome_MessageDB.save(rDD_Welcome)}, JsonRequestBehavior.AllowGet);
        }
        [Route("GetWelcome_message")]
        public ActionResult GetWelcome_message(int pagesize,int pageno,string psearch)
        {
            return Json(new { data = rDD_Welcome_MessageDB.GetDataALL(User.Identity.Name,pagesize,pageno,psearch) }, JsonRequestBehavior.AllowGet);
        }

        [Route("GetNotify_message")]
        public ActionResult GetNotify_message()
        {
            return Json(new { data = rDD_Welcome_MessageDB.GetDataNotify(User.Identity.Name) }, JsonRequestBehavior.AllowGet);
        }
    }
}