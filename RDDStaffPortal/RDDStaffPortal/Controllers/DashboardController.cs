using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        DashBoardDbOperation _DashDbOp = new DashBoardDbOperation();
        public ActionResult Index()
        {
            return View();
        }
        [Route("GetDatatable1")]
        public ActionResult GetDatatable1()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

           data= _DashDbOp.GetData_Dash1(User.Identity.Name);          
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }


        [Route("GetDatatable2")]
        public ActionResult GetDatatable2()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

            data = _DashDbOp.GetData_Dash2(User.Identity.Name,1);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }


        [Route("GetDatatable3")]
        public ActionResult GetDatatable3()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

            data = _DashDbOp.GetData_Dash2(User.Identity.Name, 2);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }


        [Route("GetDatatable4")]
        public ActionResult GetDatatable4()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

            data = _DashDbOp.GetData_Dash2(User.Identity.Name, 3);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }


        [Route("GetDatatable5")]
        public ActionResult GetDatatable5()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

            data = _DashDbOp.GetData_Dash2(User.Identity.Name, 4);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }



        [Route("GetDatatable6")]
        public ActionResult GetDatatable6()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

            data = _DashDbOp.GetData_Dash2(User.Identity.Name, 5);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }

        [Route("GetPichart")]
        public ActionResult GetPichart()
        {
            JsonResult result = new JsonResult();

            List<Pichart_Dash> data = new List<Pichart_Dash>();
            //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'
            data.Add(new Pichart_Dash
            {
                lblname = "Active",
                bgcolrs = "#26d41d",
                points = 50
            });
            data.Add(new Pichart_Dash
            {
                lblname = "Dormant",
                bgcolrs = "#ed7d31",
                points = 35
            });
            data.Add(new Pichart_Dash
            {
                lblname = "Hard Block",
                bgcolrs = "#a5a5a5",
                points = 15
            });
            data.Add(new Pichart_Dash
            {
                lblname = "Soft Block",
                bgcolrs = "#ffc000",
                points = 20
            });
            //"#26d41d", "#ed7d31", "#a5a5a5", "#ffc000", "#4472c4"
            data.Add(new Pichart_Dash
            {
                lblname = "Block List",
                bgcolrs = "#4472c4",
                points =7
            });



            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }

        [Route("GetCard1")]
        public ActionResult GetCard1()
        {
            JsonResult result = new JsonResult();
            List<Card_Dash> data = new List<Card_Dash>();
           data= _DashDbOp.GetCard_Dash(User.Identity.Name);
             result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [Route("GetBarchart1")]
        public ActionResult GetBarchart1()
        {
            JsonResult result = new JsonResult();
            List<Sales_BU> lst = new List<Sales_BU>();

            lst = _DashDbOp.GetSales_BU(User.Identity.Name);


            List<string> list = lst.AsEnumerable()
                           .Select(r => r.CompanyName)
                           .ToList();

            List<BarChart_Dash> data = new List<BarChart_Dash>();
            //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'
            List<decimal> numbers =  lst.AsEnumerable()
                           .Select(r => r.Points1)
                           .ToList();

            data.Add(new BarChart_Dash
            {
                lbls=list,
                points =numbers
                                            
            }) ;
            numbers = lst.AsEnumerable()
                           .Select(r => r.Points2)
                           .ToList();
            data.Add(new BarChart_Dash
            {               
                points = numbers,
              
            });
            
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }



        [Route("GetBarchart2")]
        public ActionResult GetBarchart2()
        {
            JsonResult result = new JsonResult();

            List<Sales_BU> lst = new List<Sales_BU>();

            lst = _DashDbOp.GetSales_BU(User.Identity.Name);


            List<string> list = lst.AsEnumerable()
                           .Select(r => r.CompanyName)
                           .ToList();

            List<BarChart_Dash> data = new List<BarChart_Dash>();
            //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'
            List<decimal> numbers = lst.AsEnumerable()
                          .Select(r => r.Points3)
                          .ToList();
            data.Add(new BarChart_Dash
            {
                lbls=list,
                points = numbers,

            });
            numbers = lst.AsEnumerable()
                        .Select(r => r.Points4)
                        .ToList();
            data.Add(new BarChart_Dash
            {
                points = numbers,

            });
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }


        [Route("Getlinechart2")]
        public ActionResult Getlinechart2()
        {
            JsonResult result = new JsonResult();

            List<BarChart_Dash> data = new List<BarChart_Dash>();
            //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'

            List<decimal> numbers = new List<decimal>(12) { 30, 45, 45, 68, 69, 90, 100, 158, 177, 200, 245, 256 };
            data.Add(new BarChart_Dash
            {
                points = numbers,
            });
            numbers = new List<decimal>(12) { 10, 20, 55, 75, 80, 48, 59, 55, 23, 107, 60, 87 };
            data.Add(new BarChart_Dash
            {
                points = numbers,

            });
            numbers = new List<decimal>(12) { 5, 6, 44, 55, 34, 67, 59, 65, 87, 11, 50, 67 };
            data.Add(new BarChart_Dash
            {
                points = numbers,

            });
            numbers = new List<decimal>(12) { 10, 30, 58, 79, 90, 105, 117, 160, 185, 210, 185, 194 };
            data.Add(new BarChart_Dash
            {
                points = numbers,

            });
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }

    }
}