using Microsoft.Office.Interop.Excel;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.DataModels.Voucher;
using RDDStaffPortal.DAL.Voucher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.SAP.Controllers
{
    public class RDD_PVController : Controller
    {
        RDD_PV_VoucherDbOperation _RDDPVOp = new RDD_PV_VoucherDbOperation();
        // GET: SAP/RDD_PV
        public ActionResult Index()
        {
            return View();
        }

       

       
        [Route("ADDRDDPV")]
        public ActionResult ADDRDDPV(int PVId=-1)
        {
            RDD_PV PV = new RDD_PV();
            PV.EditFlag = false;
            PV.SaveFlag = false;
            PV.CreatedOn = System.DateTime.Now;
            PV.CreatedBy = User.Identity.Name;
            PV = _RDDPVOp.GetDropList(User.Identity.Name);
            if (PVId != -1)
            {
                PV = _RDDPVOp.GetData(User.Identity.Name, PVId, PV);
                PV.EditFlag = true;
            }          
            return PartialView("~/Areas/SAP/Views/RDD_PV/ADDRDDPV.cshtml", PV);
        }
        [HttpPost]
        public JsonResult UploadDoc(string type)
        {
            string fname = "";
            string strMappath = "";
            string _imgname = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    strMappath = "~/excelFileUpload/" + "PV/" + User.Identity.Name + "/" + type + "/";

                    if (!Directory.Exists(strMappath))
                    {
                        Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath(strMappath)));
                    }


                    //  Get all files from Request object  
                    System.Web.HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER" || Request.Browser.Browser.ToUpper() == "GOOGLE CHROME")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {

                            fname = file.FileName;

                            var _ext = System.IO.Path.GetExtension(fname);

                        }
                        // Get the complete folder path and store the file inside it.  
                        _imgname = System.IO.Path.Combine(Server.MapPath(strMappath), fname);
                        file.SaveAs(_imgname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(strMappath.ToString().Replace("~", "") + fname, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json(strMappath.ToString().Replace("~", "") + fname, JsonRequestBehavior.AllowGet);
            }

        }

        [Route("SAVERDDPV")]
        public ActionResult SaveRDDPV(RDD_PV RDDPV)
        {
           
            RDDPV.CreatedBy = User.Identity.Name;
            RDDPV.CreatedOn = System.DateTime.Now;
            if(RDDPV.EditFlag == true)
            {
                RDDPV.LastUpdatedBy = User.Identity.Name;
                RDDPV.LastUpdatedOn = System.DateTime.Now;
            }
          
            //RDDPV.CAapprovedOn = System.DateTime.Now;
            //RDDPV.CFOapprovedOn = System.DateTime.Now;
            //RDDPV.CMapprovedOn = System.DateTime.Now;
           
            //RDDPV.ClosedDate = System.DateTime.Now;
            
            return Json(_RDDPVOp.Save1(RDDPV), JsonRequestBehavior.AllowGet);
        }

        [Route ("GETRDDPV")]
        public ActionResult GetRDDPV(int pagesize,int pageno,string psearch)
        {
            List<RDD_PV> RPV = new List<RDD_PV>();           
            RPV = _RDDPVOp.GetALLDATA(User.Identity.Name,pagesize,pageno,psearch);
            return Json(new { data = RPV }, JsonRequestBehavior.AllowGet);
        }
        
    }
}