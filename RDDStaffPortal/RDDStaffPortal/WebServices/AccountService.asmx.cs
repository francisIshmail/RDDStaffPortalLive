using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Script.Services;


namespace RDDStaffPortal.WebServices
{
    /// <summary>
    /// Summary description for AccountService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AccountService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public LoginResponse Login(string username_emial, string password)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                /// if user has entered the login Email then get the UserName first then login;
                /// 
                string UserName = string.Empty; 
                if (username_emial.IndexOf("@") > 0)
                {
                    UserName = Membership.GetUserNameByEmail(username_emial);
                }
                else
                {
                    UserName = username_emial;
                }
                MembershipUser user = Membership.GetUser(UserName);
                if (user != null)
                {
                    if (Membership.ValidateUser(UserName, password))
                    {
                        FormsAuthentication.SetAuthCookie(UserName, false);
                        response.Success = true;
                        response.UserName = UserName;
                        response.Email = user.Email;
                    }
                }
                else
                {
                   response.Success = false;
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
            }
            return response;
        }

        [WebMethod]
        public bool SignOut()
        {
            try
            {
                FormsAuthentication.SignOut();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




    }



}
