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
using RDDStaffPortal.DAL;

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
        Common commonMethods = new Common();

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
        public MembershipResponse CreateUserAccount(string UserName, string UserEmail, string quest, string ans, string rol)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            SendMail sendmail = new SendMail();

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
            //lblMsg.Text = "Successfully Registered : User '" + dealerDesiredID + "' , Login Information sent to user on EMail Id : '" + dealerEmail + "'  " + randomPassword

            membershipResponse.Message = "Successfully Registered : User '" + UserName + "' , Login Information sent to user on EMail Id : '" + UserEmail + "'";
            var mailformat = "<div>Warm Welcome to <b>Red Dot Distribution</b> Family.<br/><br/>";
            mailformat = mailformat + "<div>Dear  " + UserName + ",</div><br/>";
            mailformat = mailformat + "<div>Congratulations !!  You have been registered on Red Dot Distribution website. You can login to our site using the following credentials,</div><br/>";
           
            mailformat = mailformat + "<div>Login Name -<b> " + UserName + " </b><br/>Password - <b>" + randomPassword + "</b><br/><a href=https://app.reddotdistribution.com/Login.aspx > Click Here To Login </a></div><br/>";
     
            mailformat = mailformat + "<div>This is system generated password , We urged you to change the password at the earliest using the <b>Change Password</b> option in your profile.</div><br/>";
            mailformat = mailformat + "<div>Best Regards,<br/>Red Dot Distribution</div>";


            ////sending mail here to the new user 
            //myGlobal.sendMailToNewUser(rol, dealerDesiredID, randomPassword, dealerEmail);

            SendMail.Send(UserEmail, "", "Your have been registered on Red Dot Distribution portal", mailformat , true);


            return membershipResponse;
        }

        /// <summary>
        /// This methos is used to create new Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [WebMethod]
        public MembershipResponse CreateRole(string role)
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
        /// This methos is used to Delete existing Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [WebMethod]
        public MembershipResponse DeleteRole(string role)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            try
            {
                if (string.IsNullOrEmpty(role))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Please enter Role";
                    return membershipResponse;
                }

                if (Roles.RoleExists(role))
                {
                    string[] UsersInRole = Roles.GetUsersInRole(role);
                    if(UsersInRole.Length>0)
                    {
                        membershipResponse.Success = false; membershipResponse.Message = "Error! " + role + " is assigned to users, You can not delete it.";
                        return membershipResponse;
                    }
                    else
                    {
                        Roles.DeleteRole(role);
                        membershipResponse.Success = true; membershipResponse.Message = "Role '" + role.Trim() + "' Deleted Successfully";
                        return membershipResponse;
                    }
                }
                else
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Error! "+role+" does not exist in database to delete";
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
        public string[] GetRoles()
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
        /// This method is used to get all Roles from system.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string[] GetRolesForUser(string UserName)
        {
            try
            {
                return Roles.GetRolesForUser(UserName);
            }
            catch (Exception ex)
            {
                string[] msg = new string[] { ex.Message };
                return msg;
            }

        }


        /// <summary>
        /// This method is used to check LoggedIn User is in specific Role
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool IsUserInRole(string RoleName)
        {
            try
            {
                return Roles.IsUserInRole(RoleName);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// This methos is used to Add User to Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [WebMethod]
        public MembershipResponse AddUserToRole(string UserName, List<string> role)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            try
            {
                string k = "";
                int i = 0;
                while(role.Count>i )
                {
                    if (string.IsNullOrEmpty(UserName))
                    {
                        membershipResponse.Success = false; membershipResponse.Message = "Please enter Login UserName";
                        return membershipResponse;
                    }
                    if (string.IsNullOrEmpty(role[i]))
                    {
                        membershipResponse.Success = false; membershipResponse.Message = "Please enter Role";
                        return membershipResponse;
                    }

                    if (Membership.FindUsersByName(UserName) == null)
                    {
                        membershipResponse.Success = false; membershipResponse.Message = "Error! User Does not Exists in Database, refresh page for the latest state";
                        return membershipResponse;
                    }

                    if (!Roles.RoleExists(role[i]))
                    {
                        membershipResponse.Success = false; membershipResponse.Message = "Error! Role Does not Exists in Database, refresh page for the latest state";
                        return membershipResponse;
                    }

                    Roles.AddUserToRole(UserName, role[i]);

                    k= (i == 0) ?  role[i] : " , " +role[i] ;

                    
                   
                    i++;
                }
               
                membershipResponse.Success = true; membershipResponse.Message = "Successfully assigned role - '" + k + "' to user '" + UserName + "'.";
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
        public MembershipResponse RemoveUserFromRole(string UserName, string role)
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

        /// <summary>
        /// This method is used to verify if entered email address is registered in portal or not. returns TRUE if registered email else return FALSE.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        [WebMethod]
        public MembershipResponse ValidateEmail(string Email)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            try
            {
                string UserName = Membership.GetUserNameByEmail(Email);
                if(string.IsNullOrEmpty(UserName) )
                {
                    membershipResponse.Success = false; membershipResponse.Message ="Invalid Email Address.";
                }
                else
                {
                    membershipResponse.Success = true; membershipResponse.Message = "Valid Email Address";
                }
                return membershipResponse;

            }
            catch (Exception ex)
            {
                membershipResponse.Success = false; membershipResponse.Message = ex.Message;
                return membershipResponse;
            }
        }

        /// <summary>
        /// This method is used to chnage password from User Login.
        /// </summary>
        /// <param name="old_password"></param>
        /// <param name="new_password"></param>
        /// <param name="confirm_password"></param>
        /// <returns></returns>
        [WebMethod]
        public MembershipResponse ChangePassword(string old_password, string new_password, string confirm_password)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            try
            {
                if(string.IsNullOrEmpty(old_password))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Please enter old password.";
                    return membershipResponse;
                }
                else if (string.IsNullOrEmpty(new_password))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Please enter new password.";
                    return membershipResponse;
                }
                else if (string.IsNullOrEmpty(confirm_password))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Please enter confirm password.";
                    return membershipResponse;
                }
                else if ( new_password!=confirm_password)
                {
                    membershipResponse.Success = false; membershipResponse.Message = "New Password and confirm password must be same.";
                    return membershipResponse;
                }
                else
                {
                    MembershipUser membershipUser = Membership.Providers["AspNetSqlMembershipProvider"].GetUser(User.Identity.Name, false);
                    if (membershipUser != null)
                    {
                        if (membershipUser.IsLockedOut) //if is locked then unlock
                            membershipUser.UnlockUser();

                        if(membershipUser.ChangePassword(old_password, new_password))
                        {
                            membershipResponse.Success = true; membershipResponse.Message = "Password changed successfully.";
                            return membershipResponse;
                        }
                        else
                        {
                            membershipResponse.Success = false; membershipResponse.Message = "Failed to change password, Please try retry";
                            return membershipResponse;
                        }
                    }
                    else
                    {
                        membershipResponse.Success = false; membershipResponse.Message = "UserNotFound - Failed to change password, Please try retry";
                        return membershipResponse;
                    }
                }

            }
            catch (Exception ex)
            {
                membershipResponse.Success = false; membershipResponse.Message = ex.Message;
                return membershipResponse;
            }
        }

        [WebMethod]
        public MembershipResponse ResetPassword(string Email, string VerificationCode, string NewPassword)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Invalid email request to Reset Password.";
                    return membershipResponse;
                }
                else if (string.IsNullOrEmpty(VerificationCode))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "Password reset verification code can not be empty.";
                    return membershipResponse;
                }
                else if (string.IsNullOrEmpty(NewPassword))
                {
                    membershipResponse.Success = false; membershipResponse.Message = "New password can not be empty.";
                    return membershipResponse;
                }
                else
                {
                    MembershipUser membershipUser = Membership.GetUser(Email);
                    if(membershipUser!=null)
                    {
                        if (membershipUser.IsLockedOut) //if is locked then unlock
                            membershipUser.UnlockUser();

                        commonMethods.ResetPassword(Email, "BeforeResetPassword", VerificationCode);
                        if(membershipUser.ChangePassword("pass*123", NewPassword))
                        {
                            membershipResponse.Success = true; membershipResponse.Message = "Password changed successfully.";
                            commonMethods.ResetPassword(Email, "AfterResetPassword", VerificationCode);
                            return membershipResponse;
                        }
                        else
                        {
                            membershipResponse.Success = false; membershipResponse.Message = "Failed to change password, please retry";
                            return membershipResponse;
                        }
                    }
                    else
                    {
                        membershipResponse.Success = false; membershipResponse.Message = Email+" is not registered email in portal.";
                        return membershipResponse;
                    }

                }

            }
            catch (Exception ex)
            {
                membershipResponse.Success = false; membershipResponse.Message = ex.Message;
                return membershipResponse;
            }
        }

            public string GetErrorMessage(MembershipCreateStatus status)
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
