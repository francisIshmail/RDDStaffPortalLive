using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class IntranetNew_Partners_ActivateRewardforCountry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='ActivateRewardforCountry.aspx' and t1.IsActive=1");
                if (count > 0)
                {
                    lblMsg.Text = "";
                    BindGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Activate Reward For Country");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Page_Load()";
        }
    }

    private void BindGrid()
    {
        try
        {
            string SQL = @"Select isnull(R.ActivateRewardID, Row_Number() over ( Order By C.Country  ) ) ActivateRewardID 
		                                    ,isnull( R.CountryCode, C.CountryCode) CountryCode 
		                                    ,isnull( R.Country,C.Country) Country
		                                    ,isnull(R.IsRewardActive,0) IsRewardActive  

                                    From	Reward_ActivateforCountry R FULL JOIN	rddcountriesList C 
                                    ON R.CountryCode=C.CountryCode And R.Country=C.Country  Where  isnull( R.CountryCode, C.CountryCode) <>'SR'  ";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable TblCountries = Db.myGetDS(SQL).Tables[0];

            if (TblCountries.Rows.Count > 0)
            {
                grvActivateCountry.DataSource = TblCountries;
                grvActivateCountry.DataBind();
            }
            else
            {
                lblMsg.Text = "No data found....";
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindGrid()";
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Length=0;

            if (grvActivateCountry.Rows.Count > 0)
            {
                for (int i = 0; i < grvActivateCountry.Rows.Count; i++)
                {
                    string ActivateRewardID = grvActivateCountry.DataKeys[i].Value.ToString();
                    string CountryCode = (grvActivateCountry.Rows[i].FindControl("lblCountryCode") as Label).Text;
                    string Country = (grvActivateCountry.Rows[i].FindControl("lblCountry") as Label).Text;
                    bool IsRewardActivate = (grvActivateCountry.Rows[i].FindControl("chkIsActive") as CheckBox).Checked;
                    int IsActivate=0;
                    if(IsRewardActivate)
                    {
                        IsActivate=1;
                    }
                    strBuilder.Append("  if not exists ( select * from dbo.Reward_ActivateforCountry Where CountryCode='" + CountryCode + "')  Begin  ");
                    strBuilder.Append("   Insert into dbo.Reward_ActivateforCountry (CountryCode,Country,IsRewardActive) Values ( '" + CountryCode + "', '" + Country + "'," + IsActivate.ToString() + " ) ; End Else Begin ");
                    strBuilder.Append("  Update dbo.Reward_ActivateforCountry Set  Country = '" + Country + "' , IsRewardActive= " + IsActivate.ToString() + " Where CountryCode='" + CountryCode + "' ; End  ");
                }

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                Db.myExecuteSQL(strBuilder.ToString());

                lblMsg.Text = "Record Saved successfully";
                BindGrid();
            }
            else
            {
                lblMsg.Text = "No data found to Save";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnSave_Click() " + ex.Message;
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            Response.Redirect("Default.aspx");
        }
        catch (Exception ex )
        {
            lblMsg.Text = "Error occured in BtnCancel_Click() "+ex.Message;
        }
    }
   
    protected void grvActivateCountry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkIsRewardActive = (CheckBox)e.Row.FindControl("chkIsActive");  // Rev Forecast RR
                if (chkIsRewardActive.Checked == true)
                {
                    chkIsRewardActive.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "ERROR in DataBound() :" + ex.Message;
        }

    }
}