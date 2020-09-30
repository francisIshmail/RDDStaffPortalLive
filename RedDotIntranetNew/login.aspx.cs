using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //string url = Request["ReturnUrl"];
                //if (!string.IsNullOrEmpty(User.Identity.Name))
                //{
                //    Response.Redirect("~/IntranetNew/Default.aspx", true);
                //}

                //if (Session["RDDCaptchaCode"] != null)
                //{
                //    Session.Remove("RDDCaptchaCode");
                //}
            }
            catch { }

            //Image ImgCaptcha = (Image)Login1.FindControl("imgCaptcha");
            //ImgCaptcha.ImageUrl = "CreateCaptcha.aspx?New=1";

            Label lblMathcaptch = (Label)Login1.FindControl("LblMathCaptch");
            lblMathcaptch.Text = GenerateNewExpression();

        }

    }

    private string GenerateNewExpression()
    {
        return this.GenerateRandomNumber(1, 7).ToString() + "+" + this.GenerateRandomNumber(8, 15).ToString();
    }

    private int GenerateRandomNumber(int min, int max)
    {
        return new Random().Next(min, max);
    }

    protected void onAuthenticate(object sender, AuthenticateEventArgs e)
    {

    }

    protected void onLoggedIn(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void onLoggingIn(object sender, EventArgs e)
    {
        FormsAuthentication.RedirectFromLoginPage("admin", false);
    }

    private void Clear()
    {
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
                #region "Performing validations for login user"
                    
                    TextBox txtCaptcha = (TextBox)Login1.FindControl("txtCaptchInput");
                    //if (Session["RDDCaptchaCode"] != null && txtCaptcha.Text == Session["RDDCaptchaCode"].ToString())
                    Label lblMathcaptch = (Label)Login1.FindControl("LblMathCaptch");
                    int captchaTotal = 0;

                    if (!string.IsNullOrEmpty(lblMathcaptch.Text))
                    {
                        string[] str = lblMathcaptch.Text.Split('+');
                        foreach (string s in str)
                        {
                            int num = 0;
                            bool isNumeric = int.TryParse(s.Trim(), out num);
                            if (num > 0)
                            {
                                captchaTotal = captchaTotal + num;
                            }
                        }
                    }
                    //if (Session["RDDCaptchaCode"] != null && txtCaptcha.Text == Session["RDDCaptchaCode"].ToString())
                    if (!string.IsNullOrEmpty(txtCaptcha.Text) && Convert.ToInt32(txtCaptcha.Text) == captchaTotal)
                    {
                        MembershipUser usr = Membership.GetUser(Login1.UserName);
                        if (usr != null)
                        {
                            if (usr.IsLockedOut)
                            {
                                Login1.FailureText = "Your account is Locked, Please send mail to IT team to unlock account.";
                                return;
                            }
                        }
                        else
                        {
                            Login1.FailureText = "User Name is wrong, Please enter correct User Name and try again";
                            return;
                        }

                        if (Membership.ValidateUser(Login1.UserName, Login1.Password))
                        {
                            e.Authenticated = true; //this is critical !!

                            string strRedirect = Request["ReturnUrl"];
                            string rls = myGlobal.loggedInRoleUserBased(Login1.UserName);

                            FormsAuthentication.SetAuthCookie(Login1.UserName, true);

                            #region "To Set the session TimeOut Based on Login User and Clear the Menus session"

                            try
                            {
                                if (Session["DSMenu_Forms"] != null)
                                {
                                    Session.Remove("DSMenu_Forms");
                                }
                            }
                            catch { }

                            #endregion

                            Session[RunningCache.UserID] = Login1.UserName;
                            if (!string.IsNullOrEmpty(strRedirect) && (strRedirect.Contains("MarketingPlan") || (strRedirect.Contains("BPStatus"))))
                            {
                                Response.Redirect(strRedirect, true);
                            }
                            else
                            {
                                Response.Redirect("~/IntranetNew/Default.aspx", true);
                            }
                        }
                        else
                        {
                            Login1.FailureText = "Please enter correct Password.";
                            txtCaptcha.Text = "";
                            //return;
                        }
                    }
                    else
                    {
                        Login1.FailureText = "Wrong answer, please enter correct answer.";
                        txtCaptcha.Text = "";
                    }
                #endregion
                    
        }
        catch (Exception Ex)
        {
            Message.Show(Page, Ex.Message);
        }

    }
   
    protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
    {
        if(Membership.ValidateUser(Login1.UserName, Login1.Password))
            FormsAuthentication.RedirectFromLoginPage(Login1.UserName,true);

    }




}

