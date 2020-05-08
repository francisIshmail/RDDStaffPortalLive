using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.Admin.Models;
using RDDStaffPortal.DAL.InitialSetup;
using RDDStaffPortal.DAL.DataModels;
using System.Web.Helpers;
using System.IO;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        TestDbOperation _TestDbOp = new TestDbOperation();
        RDD_Test_ImgDBOP _imgtest = new RDD_Test_ImgDBOP();
        public ActionResult Index()
        {
            return View();
        }
       public ActionResult RDD_image1()
        {
            return View();
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


                    _imgname = DateTime.Now.ToString("ddMMyyyyhhmmss");
                    var _comPath = Server.MapPath("/Images/TempLogo/Temp") + "Abc" + _ext;
                    _imgname = "user_" + _imgname + "Abc" + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    string[] files = System.IO.Directory.GetFiles(Server.MapPath("/Images/TempLogo"), "Temp" + "Abc" + ".*");
                    foreach (string f in files)
                    {
                        System.IO.File.Delete(f);
                    }

                    Session["FILE"] = _comPath;
                    Session["type"] = _ext;
                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                        img.Resize(200, 200);
                    img.Save(_comPath);
                    // end resize
                }
            }

            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

        [Route("Saveimg")]
        public ActionResult SaveImage(RDD_test_img Rdd)
        {
            string TempPath = (string)Session["FILE"];
            if (TempPath != null && TempPath != "")
            {
               Rdd.LogoPath = TempPath;
               Rdd.imgtyp = (string)Session["type"];
            }
            else 
            {
               Rdd.LogoPath = Server.MapPath("/Images/TempLogo/defaultimg.jpg");
               Rdd.imgtyp = ".jpg";
            }
            return Json(_imgtest.save1(Rdd));

        }
        [Route("GetTestList")]
        public ActionResult GetTestList()
        {
            // return Json(_TestDbOp.GetList(), JsonRequestBehavior.AllowGet);

            JsonResult result = new JsonResult();
            try
            {

                // Initialization.
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

                string CODE1 = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
                string DESCRIPTION1 = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();

                string IsDefault1 = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();


                if (pageSize == -1)
                {
                    draw = "2";
                }

                List<RDD_Test> data = _TestDbOp.GetList();
                // Total record count.
                int totalRecords = 0;
                if (data != null)
                {
                    totalRecords = data.Count;
                }
                else
                    totalRecords = 0;
                // Verification.
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search
                    data = data.Where(p => p.CODE.ToString().ToString().ToLower().Contains(search.ToLower()) ||
                                           p.DESCRIPTION.ToString().ToLower().Contains(search.ToLower()) ||
                                           p.IsDefault.ToString().ToLower().Contains(search.ToLower())
                                          ).ToList();

                }

                if (!string.IsNullOrEmpty(CODE1) &&
         !string.IsNullOrWhiteSpace(CODE1))
                {
                    data = data.Where(p => p.CODE.ToString().ToLower().Contains(CODE1.ToLower())
                                          ).ToList();
                }

                if (!string.IsNullOrEmpty(DESCRIPTION1) &&
          !string.IsNullOrWhiteSpace(DESCRIPTION1))
                {
                    data = data.Where(p => p.DESCRIPTION.ToString().ToLower().Contains(DESCRIPTION1.ToLower())
                                          ).ToList();
                }
                if (!string.IsNullOrEmpty(IsDefault1) &&
        !string.IsNullOrWhiteSpace(IsDefault1))
                {
                    data = data.Where(p => p.IsDefault.ToString().ToLower().Contains(IsDefault1.ToLower())
                                          ).ToList();
                }
                // Sorting.
                data = this.TestColumnWithOrder(order, orderDir, data);

                // Filter record count.
                int recFilter = data.Count;


                if (pageSize != -1)
                {
                    data = data.Skip(startRec).Take(pageSize).ToList();
                }
                else
                {
                    data = data.ToList();
                }

                // Loading drop down lists.
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);
            }
            
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Return info.
            return result;
        }

        private List<RDD_Test> TestColumnWithOrder(string order, string orderDir, List<RDD_Test> data)
        {
            // Initialization.
            List<RDD_Test> lst = new List<RDD_Test>();
            try
            {
                // Sorting
                switch (order)
                {
                    case "0":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CODE).ToList()
                                                                                                 : data.OrderBy(p => p.CODE).ToList();
                        break;
                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DESCRIPTION).ToList()
                                                                                                 : data.OrderBy(p => p.DESCRIPTION).ToList();
                        break;

                    default:
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.IsDefault).ToList()
                                                                                                  : data.OrderBy(p => p.IsDefault).ToList();
                        break;


                }
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;

        }

        [Route("DeleteFlag")]
        public ActionResult DeleteFlag(string Code)
        {
            return Json(new { deleteFlag = _TestDbOp.DeleteFlag(Code) }, JsonRequestBehavior.AllowGet);
        }
        [Route("SaveTest")]
        public ActionResult Save(RDD_Test Rtest)
        
        {
            return Json(_TestDbOp.save1(Rtest));
        }



    }
}