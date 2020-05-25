using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RDDStaffPortal.Areas.SAP.Controllers
{
  [Authorize]
    public class CustomerMergingController : Controller
    {
        // GET: CustomerMerging
        RDD_CustomerMergingDbOperation _CustMerg = new RDD_CustomerMergingDbOperation();
        public ActionResult Index()
        {
            return View();
        }
        [Route("SaveCustMap")]
        public ActionResult Save(RDD_customermapping Cust)
        {
            Cust.CreatedBy = User.Identity.Name;
            return Json( _CustMerg.save(Cust) , JsonRequestBehavior.AllowGet);
        }

        [Route("DeleteCustMap")]
        public ActionResult DeleCustMap(string code, string dbname, string typ)
        {
            return Json(new { data = _CustMerg.DeleteActivity(code, dbname, typ) });
        }

        [Route("GetCustMapParentData")]
        public ActionResult GetCustMapParentData(string ParentCode,string ParentDb)
        {
            return Json(_CustMerg.GetRDDCustMerParent(ParentCode, ParentDb));

        }
        [Route("GetCustMapData")]
        public ActionResult GetData(string Code)
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

              List<RDD_CustomerMerging> data = _CustMerg.GetRDDCustMergList(Code,pageSize,startRec,search);

                int totalRecords = 0;
                if (data != null && data.Count !=0)
                {
                   totalRecords = data[0].TOTAL;
                }
                else
                    totalRecords = 0;


                if (!string.IsNullOrEmpty(search) &&
                   !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search
                    data = data.Where(p => p.CardName.ToString().ToString().ToLower().Contains(search.ToLower()) ).ToList();

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
    }
}