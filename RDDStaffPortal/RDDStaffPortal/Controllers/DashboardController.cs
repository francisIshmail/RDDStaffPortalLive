using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static RDDStaffPortal.DAL.Global;

namespace RDDStaffPortal.Controllers
{
    //[Authorize]
   

    public class DashboardController : Controller
    {
        // GET: Dashboard
        DashBoardDbOperation _DashDbOp = new DashBoardDbOperation();
        RDD_QuickLinksDBOperation _RDD_QuickOP = new RDD_QuickLinksDBOperation();
        ModulesDbOperation moduleDbOp = new ModulesDbOperation();
       
        public ActionResult Index()
        {
            return View();
        }
        [Route("CheckAuthorization")]
        public ActionResult CheckAuthrization(string url)
        {
            return Json(new {data= _DashDbOp.CheckAuthorization(User.Identity.Name,url) }, JsonRequestBehavior.AllowGet);
            
        }


        public ActionResult ErrorPage()
        {
            return View();
        }


        [Route("QuickLinkInsert")]
        public ActionResult QuickLinkInsert(RDD_QuickLinks Rdd_Q)
        {
            Rdd_Q.UserName = User.Identity.Name;
           
            return Json(_RDD_QuickOP.Save(Rdd_Q), JsonRequestBehavior.AllowGet);
        }
        [Route("DeleteActivity")]
        public ActionResult DeleteActivity(int QuickLinkid)
        {
           
            return Json(_RDD_QuickOP.DeleteActivity(QuickLinkid) , JsonRequestBehavior.AllowGet);
        }
        
        public  ActionResult GetRightSide()
        {
            return PartialView(_RDD_QuickOP.GetRightside(User.Identity.Name));
        }
        //[Route("GetProfileImg")]
        //public ActionResult GetProfileImg()
        //{
        //    return Json(_DashDbOp.GetProfilimg(User.Identity.Name));
        //}
        [Route("GetDatatable1")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetDatatable1()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

           data= _DashDbOp.GetData_Dash1(User.Identity.Name);          
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }


        [Route("GetDatatable2")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetDatatable2()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

            data = _DashDbOp.GetData_Dash2(User.Identity.Name,1);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }


        [Route("GetDatatable3")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetDatatable3()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

            data = _DashDbOp.GetData_Dash2(User.Identity.Name, 2);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }


        [Route("GetDatatable4")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetDatatable4()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

            data = _DashDbOp.GetData_Dash2(User.Identity.Name, 3);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }


        [Route("GetDatatable5")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetDatatable5()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

            data = _DashDbOp.GetData_Dash2(User.Identity.Name, 4);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }



        [Route("GetDatatable6")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetDatatable6()
        {
            JsonResult result = new JsonResult();

            List<Datatables_Dash> data = new List<Datatables_Dash>();

            data = _DashDbOp.GetData_Dash2(User.Identity.Name, 5);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }

        [Route("GetPichart1")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetPichart1()
        
        {
            JsonResult result = new JsonResult();
            List<Pichart_Dash> data1 = new List<Pichart_Dash>();
            List<Pichart_Dash> data = new List<Pichart_Dash>();
            //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'
            data1 = _DashDbOp.GetPichart1(User.Identity.Name);
            List<string> bgarr = new List<string> { "#26d41d", "#ed7d31", "#a5a5a5", "#ffc000", "#4472c4" };
            Int32 i = 0;
            if (data1.Count != null)
            {
                while (i < data1.Count)
                {
                    data.Add(new Pichart_Dash
                    {
                        lblname = data1[i].lblname,
                        bgcolrs = data1[i].bgcolrs,
                        points = data1[i].points
                    });
                    i++;
                }

            }
            
            //data.Add(new Pichart_Dash
            //{
            //    lblname = "Active",
            //    bgcolrs = "#26d41d",
            //    points = 50
            //});
            //data.Add(new Pichart_Dash
            //{
            //    lblname = "Dormant",
            //    bgcolrs = "#ed7d31",
            //    points = 35
            //});
            //data.Add(new Pichart_Dash
            //{
            //    lblname = "Hard Block",
            //    bgcolrs = "#a5a5a5",
            //    points = 15
            //});
            //data.Add(new Pichart_Dash
            //{
            //    lblname = "Soft Block",
            //    bgcolrs = "#ffc000",
            //    points = 20
            //});
            ////"#26d41d", "#ed7d31", "#a5a5a5", "#ffc000", "#4472c4"
            //data.Add(new Pichart_Dash
            //{
            //    lblname = "Block List",
            //    bgcolrs = "#4472c4",
            //    points =7
            //});



            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }

        [Route("GetPichart2")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetPichart2()
        {
            JsonResult result = new JsonResult();
            List<Pichart_Dash> data1 = new List<Pichart_Dash>();
            List<Pichart_Dash> data = new List<Pichart_Dash>();
            //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'
            data1 = _DashDbOp.GetPichart2(User.Identity.Name);
            List<string> bgarr = new List<string> { "#26d41d", "#ed7d31", "#a5a5a5", "#ffc000", "#4472c4" };
            Int32 i = 0;
            if (data1.Count != null)
            {
                while (i < data1.Count)
                {
                    data.Add(new Pichart_Dash
                    {
                        lblname = data1[i].lblname,
                        bgcolrs = data1[i].bgcolrs,
                        points = data1[i].points
                    });
                    i++;
                }
               
            }
            
            //data.Add(new Pichart_Dash
            //{
            //    lblname = "Active",
            //    bgcolrs = "#26d41d",
            //    points = 50
            //});
            //data.Add(new Pichart_Dash
            //{
            //    lblname = "Dormant",
            //    bgcolrs = "#ed7d31",
            //    points = 35
            //});
            //data.Add(new Pichart_Dash
            //{
            //    lblname = "Hard Block",
            //    bgcolrs = "#a5a5a5",
            //    points = 15
            //});
            //data.Add(new Pichart_Dash
            //{
            //    lblname = "Soft Block",
            //    bgcolrs = "#ffc000",
            //    points = 20
            //});
            ////"#26d41d", "#ed7d31", "#a5a5a5", "#ffc000", "#4472c4"
            //data.Add(new Pichart_Dash
            //{
            //    lblname = "Block List",
            //    bgcolrs = "#4472c4",
            //    points =7
            //});



            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }

        [Route("GetCard1")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetCard1()
        {
            JsonResult result = new JsonResult();
            List<Card_Dash> data = new List<Card_Dash>();
           data= _DashDbOp.GetCard_Dash(User.Identity.Name);
             result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [Route("GetCard2")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetSecondCard()
        {
            JsonResult result = new JsonResult();
            List<SecondCard> data = new List<SecondCard>();
            data = _DashDbOp.GetSecondCard(User.Identity.Name);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [Route("GetRecModel")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetRecModSecondCard()
        {
            JsonResult result = new JsonResult();
            List<RDD_Model_tbl> data = new List<RDD_Model_tbl>();
            data = _DashDbOp.GetRecModel_tbl(User.Identity.Name);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return result;
        }


        [Route("GetPayModel")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetPayModCard()
        {
            JsonResult result = new JsonResult();
            List<RDD_Model_tbl> data = new List<RDD_Model_tbl>();
            data = _DashDbOp.GetPayModel_tbl(User.Identity.Name);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [Route("GetPiChartBank")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetPiChartBank()
        {
            JsonResult result = new JsonResult();
            List<Pichart_Dash> data = new List<Pichart_Dash>();
            data = _DashDbOp.GetBankBalPichart(User.Identity.Name);
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return result;
        }


        [Route("GetBarchart1")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
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
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetBarchart2()
        {
            JsonResult result = new JsonResult();

            List<Sales_BU> lst = new List<Sales_BU>();

            lst = _DashDbOp.GetSales_BU(User.Identity.Name);


            List<string> list = lst.AsEnumerable()
                           .Select(r => r.CompanyName)
                           .ToList();

            List<BarChart_Dash> data = new List<BarChart_Dash>();
            
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
        [Route("Getlinechart1")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult Getlinechart1()
        {
            JsonResult result = new JsonResult();

            List<Sales_BU> lst = new List<Sales_BU>();
            //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'


            lst = _DashDbOp.GetSalesSummery(User.Identity.Name);

            List<string> list = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };


            List<BarChart_Dash> data = new List<BarChart_Dash>();
            //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'
            List<decimal> numbers = lst.AsEnumerable()
                           .Select(r => r.Points1)
                           .ToList();

            data.Add(new BarChart_Dash
            {
                lbls = list,
                points = numbers

            });
            numbers = lst.AsEnumerable()
                          .Select(r => r.Points2)
                          .ToList();

            data.Add(new BarChart_Dash
            {

                points = numbers

            });
            numbers = lst.AsEnumerable()
                          .Select(r => r.Points3)
                          .ToList();

            data.Add(new BarChart_Dash
            {

                points = numbers

            });
            
            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }

        [Route("Getlinechart2")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult Getlinechart2()
        {
            JsonResult result = new JsonResult();

            List<Sales_BU> lst = new List<Sales_BU>();
            //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'


            lst = _DashDbOp.GetSalesSummery(User.Identity.Name);


            List<string> list = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };


            List<BarChart_Dash> data = new List<BarChart_Dash>();
            //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'
            List<decimal> numbers  = lst.AsEnumerable()
                          .Select(r => r.Points4)
                          .ToList();

            data.Add(new BarChart_Dash
            {
                lbls=list,
                points = numbers

            });
            numbers = lst.AsEnumerable()
                          .Select(r => r.Points5)
                          .ToList();

            data.Add(new BarChart_Dash
            {

                points = numbers

            });
            numbers = lst.AsEnumerable()
                          .Select(r => r.Points6)
                          .ToList();

            data.Add(new BarChart_Dash
            {

                points = numbers

            });

            result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

            return result;

        }

    }
}