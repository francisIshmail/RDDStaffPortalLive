using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.InitialSetup;
using RDDStaffPortal.Models;
using RDDStaffPortal.WebServices;
using static RDDStaffPortal.DAL.CommonFunction;
using static RDDStaffPortal.DAL.Global;

namespace RDDStaffPortal.Controllers
{
    [OverrideAuthorization]
    public class AccountController : Controller
    {
        // GET: Account
        ModulesDbOperation moduleDbOp = new ModulesDbOperation();
        public ActionResult Index()
        {
            return View();
        }
        //Login method of login view
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        ///  Login Method of account controller
        /// </summary>
        /// <param name="login"></param>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(Login login, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                AccountService accountService = new AccountService();
                var response = accountService.Login(login.UserName, login.UserPassword);
                if (response.Success)
                {
                    var ab = moduleDbOp.GetProfilimg(login.UserName);
                    if (ab == null)
                    {
                        string ImagePath1 = Server.MapPath("/Images/TempLogo/defaultimg.jpg");
                        byte[] file;
                        using (var stream = new FileStream(ImagePath1, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader1 = new BinaryReader(stream))
                            {
                                file = reader1.ReadBytes((int)stream.Length);
                            }
                        }
                        ab = Convert.ToBase64String(file);
                    }
                     Session["LoginName"] = ab;

                    var urlToRemove = Url.Action("Index", "Dashboard");
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid UserName and Password");
                }

            }
            return View();
        }
        public ActionResult Keepalive()
        {

            //Session.Timeout = Session.Timeout + 20;
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SignOut()
        {
            // Rename AcountLogin to LoginService in Live
            AccountService accountService = new AccountService();
            bool response = accountService.SignOut();
            if (response)
            {
                Session.Abandon();
                Session.Clear();
            }
            // FormsAuthentication.SignOut();
            return RedirectToAction("/Login", "Account");
        }

        [Route("ChangePassword")]
        [HttpGet]
        public ActionResult ChangePass(string opass, string npass, string cpass)
        {
            AccountService accountService = new AccountService();
            var response = accountService.ChangePassword(opass, npass, cpass);
            return Json(new { data =response },JsonRequestBehavior.AllowGet);
        }
        [Route("ForgetPas")]
        public ActionResult ForgetPass(string email)
        {
            AccountService accountService = new AccountService();
           
            var response = accountService.ValidateEmail(email);
            if (response.Success == true)
            {
                List<Outcls> str = new List<Outcls>();
                str = moduleDbOp.Ret_Code_ForgetPass(email);
                if (str[0].Outtf == true)
                {
                    string mailFormat = "<p class='MsoNormal'><br>" +
                                                        "Dear <a href = 'mailto:" + email + "' target = '_blank' > " + email + "</a><br>" +
                                                        "<br>" +
                                                        " Please click on below link to reset your password<br>" +
                                                        "<br>" +
                                                        "URL Link: <a href = 'http://localhost:28975/ResetPwd?E=" + email + "&amp;VC=" + str[0].Responsemsg + "' target = '_blank' > http://localhost:28975/ResetPwd?E=" + email + "&amp;VC=" + str[0].Responsemsg + "</a> <br>" +
                                                        "<br>" +
                                                        "This is one time password reset link and valid for 24 hours.<br>" +
                                                        "<br>" +
                                                        "Best Regards,<br>" +
                                                        "Red Dot Distribution </p> ";
                    var k = SendMail.Send(email, "", "Reset your account password", mailFormat, true);
                    if (k != "Mail Sent Succcessfully")
                    {
                        response.Success = false;
                        response.Message = k;
                    }
                }
                else
                {
                    response.Success = str[0].Outtf;
                    response.Message = str[0].Responsemsg;

                }
                
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [Route("ResetPwd")]
        public ActionResult ResetPwd()
      {
            try
            {
                string E = Request.QueryString["E"].ToString();
                string VC = Request.QueryString["VC"].ToString();
                string i = moduleDbOp.Ret_Code(E, VC);
                if (i != "Found Record")
                {
                    Session["Errormsg"] = i;
                    return RedirectToAction("ErrorPage", "Account");
                }
            }
            catch (Exception)
            {

                Session["Errormsg"] = "Error Occur";
                return RedirectToAction("ErrorPage", "Account");
            }

           

            return View();

        }
       
        [Route("ResetPassword")]
        public ActionResult ResetPassword(string Passd,string email,string code)
        {
            
           
            AccountService accountService = new AccountService();
           var k= accountService.ResetPassword(email, code, Passd);
            string str = "SuccessPage";
            if (k.Success == true)
            {

                string mailFormat = "<p class='MsoNormal'><br>" +
                                                        "Dear <a href = 'mailto:" + email + "' target = '_blank'>" + email + " </a> <br>" +
                                                        "<br>" +
                                                        " We are happy to inform you that your password has been changed successfully for <b><a href='mailto: " + email + "' target='_blank'>" + email + " </a> </b><br>" +
                                                        "<br>" +
                                                        "Go ahead and login with your new password using below link <br>"+
                                                        "<br>"+
                                                        "URL Link: <a href ='https://app.reddotdistribution.com/Account/Login' target ='_blank'>https://app.reddotdistribution.com/Account/Login</a><br>" +
                                                        "<br>"+
                                                        "If you did this, you can safely disregard this email.<br>"+
                                                        "<br>"+
                                                        "If you didn't do this, please contact <span style='color:#2F5496'><a href='mailto:Helpdesk@reddotdistribution.com' target='_blank'>Helpdesk@reddotdistribution.com</a> </span><br>"+
                                                        "<br>"+
                                                       "<br>"+
                                                        "<br>" +
                                                        "Best Regards,<br>" +
                                                        "Red Dot Distribution </p> ";
                var k1 = SendMail.Send(email, "", "Password Changed Successfully.", mailFormat, true);
                if (k1 != "Mail Sent Succcessfully")
                {
                    k.Success = false;
                    Session["Errormsg"] = k.Message;
                }
               


                
            }
            else
            {
                Session["Errormsg"] = k.Message;
                str = "ErrorPage";
            }
            return Json(k,JsonRequestBehavior.AllowGet);

        } 
        public ActionResult ErrorPage()
        {
            return View();
        }

        public ActionResult SuccessPage()
        {
            return View();
        }

       
        [ChildActionOnly]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetMenuTreeMenu()
        {
          
            //if (User.Identity.Name == "")
            //{
            //    return RedirectToAction("/Login", "Account");

            //}
            return PartialView(moduleDbOp.GetModuleList2(User.Identity.Name, "U"));

        }
       
        [HttpGet]
        [ChildActionOnly]
        
        public ActionResult GetFirtsDashBoard()
        {
           
            return PartialView(moduleDbOp.GetFirstDashBoards(User.Identity.Name));
        }
       
       

        [ChildActionOnly]
       
        public ActionResult GetDashBoardView()
        {
        //    if (User.Identity.Name == "")
        //    {
        //        return RedirectToAction("/Login", "Account");

        //    }
            return PartialView(moduleDbOp.GetDashBoarMain(User.Identity.Name,"U"));
        }


    }
}