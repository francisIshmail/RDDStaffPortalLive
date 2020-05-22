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
                    if(string.IsNullOrEmpty(UserName))
                    {
                        username_emial = username_emial.ToLower().Replace("@reddot.co.tz", "@reddotdistribution.com");
                        UserName = Membership.GetUserNameByEmail(username_emial);
                    }
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

        /// <summary>
        /// This Method is used to create new account for user
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserEmail"></param>
        /// <param name="quest"></param>
        /// <param name="ans"></param>
        /// <param name="rol"></param>
        /// <returns></returns>
        [WebMethod]
        private MembershipResponse CreateUserAccount(string UserName, string UserEmail, string quest, string ans, string rol)
        {
            MembershipResponse membershipResponse = new MembershipResponse();

            membershipResponse.Success = false;

            string randomPassword = Membership.GeneratePassword(12, 1);
            try
            {
                MembershipCreateStatus sts;
                MembershipUser newUser = Membership.CreateUser(UserName, randomPassword, UserEmail, quest, ans, true, out sts);
                membershipResponse.Success = true;
            }
            catch (MembershipCreateUserException e1)
            {
                membershipResponse.Success = false;
                membershipResponse.Message = GetErrorMessage(e1.StatusCode);
                return membershipResponse;
            }
            catch (HttpException e1)
            {
                membershipResponse.Success = false;
                membershipResponse.Message = e1.Message;
                return membershipResponse;
            }
            try
            {
                Roles.AddUserToRole(UserName, rol);
                membershipResponse.Success = true;
            }
            catch (Exception e2)
            {
                membershipResponse.Success = false;
                membershipResponse.Message = e2.Message;
                return membershipResponse;
            }

            //temprorary close this later , open below three lines
            //lblMsg.Text = "Successfully Registered : User '" + dealerDesiredID + "' , Login Information sent to user on EMail Id : '" + dealerEmail + "'  " + randomPassword;

            membershipResponse.Message = "Successfully Registered : User '" + UserName + "' , Login Information sent to user on EMail Id : '" + UserEmail + "'";

            ////sending mail here to the new user 
            //myGlobal.sendMailToNewUser(rol, dealerDesiredID, randomPassword, dealerEmail);

            return membershipResponse;
        }

        /// <summary>
        /// This methos is used to create new Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [WebMethod]
        private MembershipResponse CreateRole(string role)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            try
            {
                if(string.IsNullOrEmpty(role))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Please enter Role";
                    return membershipResponse;
                }

                if (!Roles.RoleExists(role))
                {
                    Roles.CreateRole(role.Trim());
                    membershipResponse.Success = true; membershipResponse.Message = "Role '" + role.Trim() + "' Created Successfully";
                    return membershipResponse;
                }
                else
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Error! Role Already Exists in Database";
                    return membershipResponse;
                }
            }
            catch (Exception ex)
            {
                membershipResponse.Success = false; membershipResponse.Message = ex.Message;
                return membershipResponse;
            }
        }

        /// <summary>
        /// This method is used to get all Roles from system.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        private string[] GetRoles()
        {
            try
            {
                return Roles.GetAllRoles();
            }
            catch (Exception ex)
            {
                string[] msg = new string[] { ex.Message };
                return msg;
            }
            
        }

        /// <summary>
        /// This methos is used to Add User to Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [WebMethod]
        private MembershipResponse AddUserToRole(string UserName, string role)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Please enter Login UserName";
                    return membershipResponse;
                }
                if (string.IsNullOrEmpty(role))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Please enter Role";
                    return membershipResponse;
                }

                if (Membership.FindUsersByName(UserName) == null)
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Error! User Does not Exists in Database, refresh page for the latest state";
                    return membershipResponse;
                }

                if (!Roles.RoleExists(role))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Error! Role Does not Exists in Database, refresh page for the latest state";
                    return membershipResponse;
                }

                Roles.AddUserToRole(UserName, role);
                membershipResponse.Success = true; membershipResponse.Message = "Successfully assigned role - '" + role + "' to user '" + UserName + "'.";
                return membershipResponse;

            }
            catch (Exception ex)
            {
                membershipResponse.Success = false; membershipResponse.Message = ex.Message;
                return membershipResponse;
            }
        }


        /// <summary>
        /// This methos is used to Remove Role from User
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [WebMethod]
        private MembershipResponse RemoveUserFromRole(string UserName, string role)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Please enter Login UserName";
                    return membershipResponse;
                }
                if (string.IsNullOrEmpty(role))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Please enter Role";
                    return membershipResponse;
                }

                if (Membership.FindUsersByName(UserName) == null)
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Error! User Does not Exists in Database, refresh page for the latest state";
                    return membershipResponse;
                }

                if (!Roles.RoleExists(role))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Error! Role Does not Exists in Database, refresh page for the latest state";
                    return membershipResponse;
                }

                Roles.RemoveUserFromRole(UserName, role);
                membershipResponse.Success = true; membershipResponse.Message = "Role '" +  role + "' un-assigned to user '" + UserName + "' Successfully";
                return membershipResponse;

            }
            catch (Exception ex)
            {
                membershipResponse.Success = false; membershipResponse.Message = ex.Message;
                return membershipResponse;
            }
        }



        private string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Login ID already exists. Please enter a different Login ID.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A Login ID for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The Login ID provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }



    }



}
