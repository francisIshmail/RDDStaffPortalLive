using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.Reports.Controllers
{
    [Authorize]
    public class WebReportsController : Controller
    {
        // GET: Reports/WebReports


        RDD_WebReportsDBOperation _ReptOp = new RDD_WebReportsDBOperation();
        public ActionResult Index()
        {
            return View();
        }


        [Route("GetWebReportMapData")]
        public ActionResult GetData(string Code=null)
        {
            JsonResult result = new JsonResult();
            try
            {
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

                if (startRec == 0)
                {
                    startRec = 1;
                }
                else
                {
                    startRec = (startRec / pageSize) + 1;
                }
                if (pageSize == -1)
                {
                    draw = "2";
                }

                List<RDD_WebReportsList> data = _ReptOp.GetRDD_WebReportList(pageSize, startRec, search,Code);

                int totalRecords = 0;
                if (data != null && data.Count != 0)
                {
                    totalRecords = data[0].TotalCount;
                }
                else
                    totalRecords = 0;


                if (!string.IsNullOrEmpty(search) &&
                   !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search
                    data = data.Where(p => p.reportTitle.ToString().ToString().ToLower().Contains(search.ToLower())).ToList();

                }

                int recFilter = totalRecords;// data.Count;


                //if (pageSize != -1)
                //{
                //    data = data.Skip(startRec).Take(pageSize).ToList();
                //}
                //else
                //{
                data = data.ToList();
                //}

                // Loading drop down lists.
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }



            return result;
        }

        [Route("GetCustMapWeb")]
        public ActionResult GetCustMapParentData(string CustCode)
        {
            return Json(_ReptOp.GetRDD_WebReportUserList(CustCode));

        }


        [Route("DeleteActivityWebReport")]
        public ActionResult DeleteActivity(string Username,int Code)
        {
            return Json(new { data = _ReptOp.DeleteActivity(Username, Code) }, JsonRequestBehavior.AllowGet);
        }


        [Route("SaveWebRep")]
        public ActionResult save(RDD_WebReportsUser WURep)
        {
            return Json(_ReptOp.Save(WURep), JsonRequestBehavior.AllowGet);
        }
    }
}