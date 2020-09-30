using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Partners_RecalculateReward : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='RecalculateReward.aspx' and t1.IsActive=1");
                if (count > 0)
                {
                    ddlIsEligible.Attributes.Add("onchange", "confirmIsEligible();");
                    BindDatabaseDDL(ddlDatabase);
                    //select Name from sys.databases Where Name in ('SAPAE','SAPTZ','SAPUG','SAPKE','SAPZM')/
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Recalculate Reward Points");
                }
        }
    }

    private void BindDatabaseDDL(DropDownList ddlDatabase)
    {
        try
        {
            Db.LoadDDLsWithCon(ddlDatabase, "Select '--Select--' as Name, '00' as 'Name' Union select Name,Name from sys.databases Where Name in ('SAPAE','SAPTZ','SAPUG','SAPKE','SAPZM')", "Name", "Name", myGlobal.getAppSettingsDataForKey("tejSAP"));
            if (ddlDatabase.Items.Count > 0)
                ddlDatabase.SelectedIndex = 0;
            else
            {
                ddlDatabase.Items.Add("No Rows");
                ddlDatabase.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindDatabaseDDL():" + ex.Message;
        }
    }


    protected void BtnGetInvForRecalculate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlDatabase.SelectedIndex == 0 || ddlDatabase.SelectedItem.Value == "--Select--")
            {
                lblMsg.Text = "Please select database";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert(' Please select database'); </script>");
                return;
            }

            if ( rdARInvoice.Checked==false && rdARCreditNote.Checked==false )
            {
                lblMsg.Text = "Please select Document Type";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please select Document Type'); </script>");
                return;
            }

            if ( string.IsNullOrEmpty(txtDocumentNo.Text) )
            {
                lblMsg.Text = "Please enter Document No.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter document No.'); </script>");
                return;
            }

            string  tableName = "", ObjType="";
           
            if (rdARInvoice.Checked == true)
            {
                tableName = "INV";
                ObjType = "13";
            }
            else if (rdARCreditNote.Checked == true)
            {
                tableName = "RIN";
                ObjType = "14";
            }

            LblIsEligibleStatus.Text = "";

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS(" Exec RewardRecalculation_Checking " + txtDocumentNo.Text + ",'" + ddlDatabase.SelectedItem.Text + "','" + tableName + "'," + ObjType);
            if (DS.Tables.Count > 0)
            {
                DataTable TblValidation = DS.Tables[0];
                if (TblValidation.Rows.Count > 0)
                {
                    txtDocEntry.Text = "";
                    txtCardCode.Text = "";
                    txtCardName.Text = "";
                    txtDocTotal.Text = "";
                    txtDocStatus.Text = "";
                    txtDocDate.Text = "";
                    txtRewardPoints.Text = "";
                    BtnRecalculate.Enabled = false;
                    lblMsg.Text = TblValidation.Rows[0][0].ToString();
                    return;
                }
            }
            DS = Db.myGetDS(" Exec getdataforRewardRecalculation  '" + ddlDatabase.SelectedItem.Text + "'," + txtDocumentNo.Text + ",'"+ tableName +"'," + ObjType    );
            if (DS.Tables.Count > 0)
            {
                DataTable TblDocData = DS.Tables[0];
                if (TblDocData.Rows.Count > 0)
                {
                    txtDocEntry.Text = TblDocData.Rows[0]["DocEntry"].ToString();
                    txtCardCode.Text = TblDocData.Rows[0]["Cardcode"].ToString();
                    txtCardName.Text = TblDocData.Rows[0]["CardName"].ToString();
                    txtDocTotal.Text = TblDocData.Rows[0]["DocTotal"].ToString();
                    txtDocStatus.Text = TblDocData.Rows[0]["DocStatus"].ToString();
                    txtDocDate.Text = TblDocData.Rows[0]["DocDate"].ToString();
                    txtRewardPoints.Text = TblDocData.Rows[0]["DocRewardPoint"].ToString();
                    string IsEligible = TblDocData.Rows[0]["IsEligible"].ToString();
                    try
                    {
                        Session["IsRewardEligible"] = IsEligible;
                        LblIsEligibleStatus.Text = IsEligible;
                        if (IsEligible == "YES")
                        {
                            ddlIsEligible.SelectedIndex = 0;
                            ddlIsEligible.SelectedItem.Text = "YES";
                        }
                        else if (IsEligible == "NO")
                        {
                            ddlIsEligible.SelectedIndex = 1;
                            ddlIsEligible.SelectedItem.Text = "NO";
                        }
                    }
                    catch {
                    }
                    BtnRecalculate.Enabled = true;
                }
            }
            else
            {
                lblMsg.Text = "No data found for Document No:" + txtDocumentNo.Text + "  in   " + ddlDatabase.SelectedItem.Text;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnGetInvForRecalculate_Click():" + ex.Message;
        }
    }

    protected void BtnRecalculate_Click(object sender, EventArgs e)
    {
        try
        {

            string tableName = "", ObjType = "";

            if (rdARInvoice.Checked == true)
            {
                tableName = "INV";
                ObjType = "13";
            }
            else if (rdARCreditNote.Checked == true)
            {
                tableName = "RIN";
                ObjType = "14";
            }

            if (string.IsNullOrEmpty(txtDocEntry.Text))
            {
                lblMsg.Text = " Insufficient data for reward Recalculation : DocEntry is emplty ";
                return;
            }

            if (ddlIsEligible.SelectedItem.Text == "NO")
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int CountRec = Db.myExecuteScalar("Select count(*) From dbo.Reward_Points Where DBName='" + ddlDatabase.SelectedItem.Text + "' And DocEntry=" + txtDocEntry.Text + " And TransType=" + ObjType + " And ( RewardStatus in ('C', 'L') OR IsRedeemed = 1 OR IsPartiallyRedeemed = 1 OR IsExpired = 1 ) ");
                if (CountRec > 0)
                {
                    lblMsg.Text = "You can not change Eligibility for Reward of document becuase points are either Confirmed,Lost, Redeemed or Expired.";
                    return;
                }
            }
            else
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int CountRec = Db.myExecuteScalar("Select count(*) From dbo.Reward_Points Where DBName='" + ddlDatabase.SelectedItem.Text + "' And DocEntry=" + txtDocEntry.Text + " And TransType=" + ObjType + " And ( RewardStatus = 'L' OR IsRedeemed = 1 OR IsPartiallyRedeemed = 1 OR IsExpired = 1 ) ");
                if (CountRec > 0)
                {
                    lblMsg.Text = "You can not change Eligibility for Reward of document becuase points are either Confirmed,Lost, Redeemed or Expired.";
                    return;
                }
            }

            if (string.IsNullOrEmpty(tableName))
            {
                lblMsg.Text = " Insufficient data for reward Recalculation : TableName is emplty ";
                return;
            }
            if (string.IsNullOrEmpty(txtDocDate.Text))
            {
                lblMsg.Text = " Insufficient data for reward Recalculation : DocDate is emplty ";
                return;
            }
            if (string.IsNullOrEmpty(ObjType))
            {
                lblMsg.Text = " Insufficient data for reward Recalculation : ObjType is emplty ";
                return;
            }
            if (string.IsNullOrEmpty(txtCardCode.Text))
            {
                lblMsg.Text = " Insufficient data for reward Recalculation : CardCode is emplty ";
                return;
            }
            if (ddlDatabase.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = " Insufficient data for reward Recalculation : ObjType is emplty ";
                return;
            }

            string RewardRecalcString = " EXEC RecalculateRewardPoints " + txtDocEntry.Text + ",'" + ddlDatabase.SelectedItem.Text + "','" + tableName + "','" + txtDocDate.Text + "','" + ObjType + "','" + txtCardCode.Text + "','"+ddlIsEligible.SelectedItem.Text +"'"  ;

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS(RewardRecalcString);
            if (DS.Tables.Count > 0)
            {
                /// Write code to show successfull / Failed message and show reward points
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string RewardPoint = DS.Tables[0].Rows[0][0].ToString();
                    if (RewardPoint == "0" && ddlIsEligible.SelectedItem.Text=="NO" )  
                    {
                        txtRewardPoints.Text = RewardPoint;
                        lblMsg.Text = "Reward Points reversed and posted successfully.";
                        LblIsEligibleStatus.Text = "NO";
                    }
                    else if (RewardPoint != "-999")
                    {
                        txtRewardPoints.Text = RewardPoint;
                        lblMsg.Text = "( " + RewardPoint + " )  Reward Points calculated and posted successfully.";
                        LblIsEligibleStatus.Text = "YES";
                    }
                    else
                    {
                        lblMsg.Text = DS.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    }
                }
                else
                {
                    lblMsg.Text = " Something went wrong, Please try recalculate again.. ";
                }
            }
            else
            {
                lblMsg.Text = " Something went wrong, Please try recalculate again.. ";
            }

            #region
            //bool successFlag=false;
            //for (int i = 0; i < grvDocuments.Rows.Count; i++)
            //{
            //    lblMsg.Text = "Please wait..";
            //    GridViewRow row = grvDocuments.Rows[i];
            //    CheckBox chkDocNo = (CheckBox)row.FindControl("chkDocNo");
            //    if (chkDocNo.Checked == true)
            //    {
            //        Label lblDocEntry = (Label)row.FindControl("lblDocEntry");
            //        Label lblDocNum = (Label)row.FindControl("lblDocNum");
            //        Label lblDocDate = (Label)row.FindControl("lblDocDate");
            //        Label lblDocStatus = (Label)row.FindControl("lblDocStatus");
            //        Label lblCardCode = (Label)row.FindControl("lblCardCode");
            //        Label lblCardName = (Label)row.FindControl("lblCardName");
            //        Label lblObjType = (Label)row.FindControl("lblObjType");
            //        Label lblTableName = (Label)row.FindControl("lblTableName");
            //        Label lblDBName = (Label)row.FindControl("lblDBName");
            //        Label lblDocTotal = (Label)row.FindControl("lblDocTotal");
            //        Label lblMinInvValueForReward = (Label)row.FindControl("lblMinInvValueForReward");
            //        Label lblInvIsEligible = (Label)row.FindControl("lblInvIsEligible");

            //        string DocTotal = lblDocTotal.Text;
            //        string MinInvValueForReward = lblMinInvValueForReward.Text;
            //        if (!string.IsNullOrEmpty(DocTotal) && !string.IsNullOrEmpty(MinInvValueForReward))
            //        {
            //            if (Convert.ToDecimal(DocTotal.Trim()) >= Convert.ToDecimal(MinInvValueForReward.Trim()) && lblInvIsEligible.Text.Trim().ToLower()=="yes")
            //            {
            //                lblMsg.Text = "Please wait.. Recalculating reward points for Document # " + lblDocNum.Text;

            //                string CalcRewardProc = "  EXEC RecalculateRewardPoints   " + lblDocEntry.Text + ",'" + lblDBName.Text + "','" + lblTableName.Text + "','" + lblDocDate.Text + "','" + lblObjType.Text + "','" + lblCardCode.Text + "'";
            //                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            //                DataSet DS= Db.myGetDS(CalcRewardProc);
            //                if (DS.Tables.Count > 0)
            //                {
            //                    if (DS.Tables[0].Rows.Count > 0)
            //                    {
            //                        string RewardRecalcRetVal = DS.Tables[0].Rows[0][0].ToString();
            //                        if (!string.IsNullOrEmpty(RewardRecalcRetVal))
            //                        {
            //                            if (Convert.ToInt32(RewardRecalcRetVal) == -999) // Means Error occured in Reward Calculation.. check Error in dbo.Reward_ErrorLog Table
            //                            {
            //                                chkDocNo.Enabled = false;
            //                                chkDocNo.BackColor = System.Drawing.Color.Red;
            //                            }
            //                            else
            //                            {
            //                                successFlag = true;
            //                                chkDocNo.Enabled = false;
            //                                chkDocNo.BackColor = System.Drawing.Color.Green;  
            //                                lblMsg.Text = "Reward points calculated for Document # " + lblDocNum.Text;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                chkDocNo.Enabled = false;
            //                chkDocNo.BackColor = System.Drawing.Color.Yellow;
            //            }
            //        }
            //    }
            //}
            //if (successFlag == true)
            //{
            //    lblMsg.Text = "Reward Point Recalculated successfully";
            //}
            //BtnRecalculate.Enabled = false;
            //BtnGetInvForRecalculate.Enabled = true;
            #endregion

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnRecalculate_Click():" + ex.Message;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
          
            BtnRecalculate.Enabled = false;
            lblMsg.Text = "";
            txtDocumentNo.Text = "";
            rdARInvoice.Checked = true;
            try
            {
                ddlDatabase.SelectedIndex = 0;
                ddlDatabase.SelectedItem.Text = "--Select--";
            }
            catch { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnCancel_Click():" + ex.Message;
        }
    }
}