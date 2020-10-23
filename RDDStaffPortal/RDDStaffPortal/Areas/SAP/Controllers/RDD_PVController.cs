using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.DataModels.Voucher;
using RDDStaffPortal.DAL.Voucher;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.SAP.Controllers
{
    [Authorize]
    public class RDD_PVController : Controller
    {
        RDD_PV_VoucherDbOperation _RDDPVOp = new RDD_PV_VoucherDbOperation();
        // GET: SAP/RDD_PV
        public ActionResult Index()
        {
            RDD_PV PV = new RDD_PV();
            PV = _RDDPVOp.GetDropList(User.Identity.Name);
            return View(PV);
        }
        [Route("DeleteRDDPV")]
        public ActionResult DeletePV(int PVId)
        {
            return Json(new {  data = _RDDPVOp.Delete1(PVId)}, JsonRequestBehavior.AllowGet);
        }

       
        [Route("ADDRDDPV")]
        public ActionResult ADDRDDPV(int PVId=-1)
        {
            RDD_PV PV = new RDD_PV();            
            PV = _RDDPVOp.GetDropList(User.Identity.Name);
            PV.EditFlag = false;
            PV.SaveFlag = false;
            PV.CreatedOn = System.DateTime.Now;
            PV.CreatedBy = User.Identity.Name;
            PV.IsDraft = true;
            if (PVId != -1)
            {
                PV = _RDDPVOp.GetData(User.Identity.Name, PVId, PV);
                PV.EditFlag = true;
            }
            else
            {
                string str = PV.RefNo + "_" + User.Identity.Name;

                string strMappath = "~/excelFileUpload/" + "PV/" + str + "/";
                string fullPath = Request.MapPath(strMappath);
                if (Directory.Exists(fullPath))
                {


                    string[] filePaths = Directory.GetFiles(fullPath);
                    foreach (string filePath in filePaths)
                        System.IO.File.Delete(filePath);

                    Directory.Delete(fullPath);
                }
            }       
            return PartialView("~/Areas/SAP/Views/RDD_PV/ADDRDDPV.cshtml", PV);
        }

        [Route("VIEWRDDPV")]
        public ActionResult VIEWRDDPV(int PVId)
        {
            RDD_PV PV = new RDD_PV();
            //PV = _RDDPVOp.GetDropList(User.Identity.Name);
            
            PV.IsDraft = true;
            
                PV = _RDDPVOp.GetData(User.Identity.Name, PVId, PV);
                PV.EditFlag = true;
            PV.CreatedBy = User.Identity.Name;
            
            return PartialView("~/Areas/SAP/Views/RDD_PV/VIEWRDDPV.cshtml", PV);
        }
        [HttpPost]
        public JsonResult UploadDoc(string Refno,string type)
        {
            string fname = "";
            string strMappath = "";
            string _imgname = string.Empty;
            string _imgname1 = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    string str = Refno + "_" + User.Identity.Name;
                    //strMappath = "~/excelFileUpload/" + "PV/" + User.Identity.Name + "/" + type + "/";
                    strMappath = "~/excelFileUpload/" + "PV/" +str+ "/";

                    if (!Directory.Exists(strMappath))
                    {
                        Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath(strMappath)));
                    }


                    //  Get all files from Request object  
                    System.Web.HttpFileCollectionBase files = Request.Files;
                    var _ext = "";
                    var fileName = "";
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
                             fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            _ext = System.IO.Path.GetExtension(fname).ToUpper();
                            if((_ext!=".JPG"&& _ext!=".PNG" && _ext!=".GIF" && _ext != ".PDF") && type== "Header")
                            {
                                return Json("Error occurred. Error details: Only Image Or Pdf", JsonRequestBehavior.AllowGet);
                            }

                        }

                        // Get the complete folder path and store the file inside it.
                        _imgname1 = fileName + "" + System.DateTime.Now.ToString("ddMMyyyyHHMMss") + "" + _ext;
                        _imgname = System.IO.Path.Combine(Server.MapPath(strMappath), _imgname1);
                        file.SaveAs(_imgname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(strMappath.ToString().Replace("~", "") + _imgname1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json(strMappath.ToString().Replace("~", "") + _imgname1, JsonRequestBehavior.AllowGet);
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
        [Route("ChangeVoucherStatus")]
        public ActionResult VoucherStatus(string DOC_Status,int PVID)
        {
            return Json(_RDDPVOp.ChangeVoucherStatus(DOC_Status, PVID), JsonRequestBehavior.AllowGet);
        }

        [Route ("GETRDDPV")]
        public ActionResult GetRDDPV(int pagesize,int pageno,string psearch,string SearchCon)
        {
            List<RDD_PV> RPV = new List<RDD_PV>();           
            RPV = _RDDPVOp.GetALLDATA(User.Identity.Name,pagesize,pageno,psearch,SearchCon);
            return Json(new { data = RPV }, JsonRequestBehavior.AllowGet);
        }
        [Route("GetVendor")]
        public ActionResult GetVendor(string DBName,string Vtype)
        {
            ContentResult ret = null;
            DataSet DS = _RDDPVOp.GetVendor(DBName,Vtype);
            
            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }

        [Route("GetBank")]
        public ActionResult GetBank(string DBName)
        {
            ContentResult ret = null;
            DataSet DS = _RDDPVOp.GetBank(DBName);

            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }


        [Route("GetVendorAgeing")]
        public ActionResult GetVendorDue(string DBName,string BP)
        {
            ContentResult ret = null;
            DataSet DS = _RDDPVOp.GetVendorAgeing(DBName,BP);

            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }


        [Route("GetVendorRef")]
        public ActionResult GetVendorRef(string Country)
        {
            ContentResult ret = null;
            DataSet DS = _RDDPVOp.GetRefNo(Country);
            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }

    }
}