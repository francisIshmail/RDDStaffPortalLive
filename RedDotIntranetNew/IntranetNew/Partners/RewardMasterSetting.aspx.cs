using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Partners_RewardMasterSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='RewardMasterSetting.aspx' and t1.IsActive=1");
                if (count > 0)
                {
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    DataSet DS = Db.myGetDS(" Select * From dbo.Reward_MasterSettings ");
                    if (DS.Tables.Count > 0)
                    {
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            lblMasterSettingId.Text = DS.Tables[0].Rows[0]["MasterSettingId"].ToString();
                            if (DS.Tables[0].Rows[0]["RewardAutoRenewal"] != null)
                            {
                                if (Convert.ToBoolean(DS.Tables[0].Rows[0]["RewardAutoRenewal"]) == true)
                                {
                                    chkAutoRenewal.Checked = true;
                                }
                                else
                                {
                                    chkAutoRenewal.Checked = false;
                                }
                            }
                            if (DS.Tables[0].Rows[0]["RewardSetupReminderDayOfQrtr"] != null)
                            {
                                txtRewardReminderdt.Text = DS.Tables[0].Rows[0]["RewardSetupReminderDayOfQrtr"].ToString();
                            }
                            if (DS.Tables[0].Rows[0]["ReminderEmailId"] != null)
                            {
                                txtReminderemialId.Text = DS.Tables[0].Rows[0]["ReminderEmailId"].ToString();
                            }
                            if (DS.Tables[0].Rows[0]["RewardMinimumInvoiceValue"] != null)
                            {
                                txtMinInvoiceRewardValue.Text = DS.Tables[0].Rows[0]["RewardMinimumInvoiceValue"].ToString();
                            }
                            if (DS.Tables[0].Rows[0]["RewardMultiplicationFactor"] != null)
                            {
                                txtMultiplicationFactor.Text = DS.Tables[0].Rows[0]["RewardMultiplicationFactor"].ToString();
                            }
                            if (DS.Tables[0].Rows[0]["RewardExpiresAfterMonth"] != null)
                            {
                                txtRewardPointsExpiresAfterMonth.Text = DS.Tables[0].Rows[0]["RewardExpiresAfterMonth"].ToString();
                            }
                            if (DS.Tables[0].Rows[0]["RoundingCeilingFloor"] != null)
                            {
                                if (Convert.ToBoolean(DS.Tables[0].Rows[0]["RoundingCeilingFloor"]) == true)
                                {
                                    rdceiling.Checked = true;
                                    rdFloor.Checked = false;
                                }
                                else
                                {
                                    rdceiling.Checked = false;
                                    rdFloor.Checked = true;
                                }
                            }
                            if (DS.Tables[0].Rows[0]["RewardCalcOnCost"] != null)
                            {
                                if (Convert.ToBoolean(DS.Tables[0].Rows[0]["RewardCalcOnCost"]) == true)
                                {
                                    rdcost.Checked = true;
                                    rdsales.Checked = false;
                                }
                                else
                                {
                                    rdcost.Checked = false;
                                    rdsales.Checked = true;
                                }
                            }
                            if (DS.Tables[0].Rows[0]["RewardCalcOnSellingPrice"] != null)
                            {
                                if (Convert.ToBoolean(DS.Tables[0].Rows[0]["RewardCalcOnSellingPrice"]) == true)
                                {
                                    rdsales.Checked = true;
                                    rdcost.Checked = false;
                                }
                                else
                                {
                                    rdsales.Checked = false;
                                    rdcost.Checked = true;
                                }
                            }

                            if (DS.Tables[0].Rows[0]["RewardConfirmAfterDays"] != null)
                            {
                                txtPointConfirmDays.Text = DS.Tables[0].Rows[0]["RewardConfirmAfterDays"].ToString();
                            }

                            if (DS.Tables[0].Rows[0]["UseInvoiceORSetUpDaysToConfirmReward"] != null)
                            {
                                try
                                {
                                    ddlUseINVORSETUP.SelectedItem.Text = DS.Tables[0].Rows[0]["UseInvoiceORSetUpDaysToConfirmReward"].ToString();
                                    ddlUseINVORSETUP.SelectedItem.Value = DS.Tables[0].Rows[0]["UseInvoiceORSetUpDaysToConfirmReward"].ToString();
                                }
                                catch { }
                            }

                            if (DS.Tables[0].Rows[0]["FinanceGraceDays"] != null)
                            {
                                txtFinanceGraceDays.Text = DS.Tables[0].Rows[0]["FinanceGraceDays"].ToString();
                            }

                            if (DS.Tables[0].Rows[0]["CustomerGraceDays"] != null)
                            {
                                txtcustGracePeriod.Text = DS.Tables[0].Rows[0]["CustomerGraceDays"].ToString();
                            }

                            if (DS.Tables[0].Rows[0]["RewardSubscriptionpoints"] != null)
                            {
                                txtRewardSubscriptionBonus.Text = DS.Tables[0].Rows[0]["RewardSubscriptionpoints"].ToString();
                            }

                            if (DS.Tables[0].Rows[0]["RewardConfirmBasedOnPayTerms"] != null)
                            {
                                if (Convert.ToBoolean(DS.Tables[0].Rows[0]["RewardConfirmBasedOnPayTerms"]) == true)
                                {
                                    chkPointConfirmOnPayTerm.Checked = true;
                                }
                                else
                                {
                                    chkPointConfirmOnPayTerm.Checked = false;
                                }
                            }
                            //if (DS.Tables[0].Rows[0]["RewardExpiresAfterMonth"] != null)
                            //{
                            //    txtPointsExpiresAftrMonth.Text = DS.Tables[0].Rows[0]["RewardExpiresAfterMonth"].ToString();
                            //}

                            if (DS.Tables[0].Rows[0]["RewardExpiryReminderFrequency"] != null)
                            {
                                try
                                {
                                    ddlRewardPointExpiryFrequency.SelectedItem.Text = DS.Tables[0].Rows[0]["RewardExpiryReminderFrequency"].ToString();
                                }
                                catch { }
                            }
                            if (DS.Tables[0].Rows[0]["RewardExpiresAfterQHY"] != null)
                            {
                                ddlRewardExpiry.SelectedItem.Text = DS.Tables[0].Rows[0]["RewardExpiresAfterQHY"].ToString();
                                ddlRewardExpiry.SelectedItem.Value = DS.Tables[0].Rows[0]["RewardExpiresAfterQHY"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Reward Master Setting");
                }
            }
            catch ( Exception ex )
            {
                lblMsg.Text = " Error in Page_Load :" + ex.Message;
            }
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (!string.IsNullOrEmpty(txtRewardReminderdt.Text) && Convert.ToInt32(txtRewardReminderdt.Text)>27)
            {
                lblMsg.Text = "Reward setup reminder day can't be greater than 27";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('  Reward setup reminder day can't be greater than 27 '); </script>");
                return;
            }

            if (!string.IsNullOrEmpty(txtRewardReminderdt.Text) && string.IsNullOrEmpty(txtReminderemialId.Text))
            {
                lblMsg.Text = "Please enter reminder emails";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter reminder emails '); </script>");
                return;
            }

            if ( string.IsNullOrEmpty(txtMultiplicationFactor.Text))
            {
                lblMsg.Text = "Please enter Reward point multiplication factor";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter Reward point multiplication factor'); </script>");
                return;
            }

            if (!string.IsNullOrEmpty(txtMultiplicationFactor.Text) && Convert.ToInt32(txtMultiplicationFactor.Text)<=0 )
            {
                lblMsg.Text = "Multiplication factor must be greater than zero";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Multiplication factor must be greater than zero'); </script>");
                return;
            }

            if (chkPointConfirmOnPayTerm.Checked == false && (  !string.IsNullOrEmpty(txtPointConfirmDays.Text) ||  Convert.ToInt32(txtMultiplicationFactor.Text)<=0 ) )
            {
                lblMsg.Text = "Either select an option Reward Points confirm based on payment Term Or Enter Reward Points confirm after days";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Either select an option Reward Points confirm based on payment Term Or Enter Reward Points confirm after days'); </script>");
                return;
            }

            //if (!string.IsNullOrEmpty(txtPointsExpiresAftrMonth.Text) && Convert.ToInt32(txtPointsExpiresAftrMonth.Text) <= 0)
            //{
            //    lblMsg.Text = "Reward Points Expire after Month must be greater than zero";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Reward Points Expire after Month must be greater than zero'); </script>");
            //    return;
            //}

            if (ddlRewardExpiry.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select Reward Points expity";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please select Reward Points expity'); </script>");
                return;
            }

            //if (string.IsNullOrEmpty(txtsendRewardPtsexpiryday.Text))
            //{
            //    lblMsg.Text = "Please enter Send reward points expiry email on day";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter Send reward points expiry email on day'); </script>");
            //    return;
            //}

            //if (Convert.ToInt32(txtsendRewardPtsexpiryday.Text) < 1 || Convert.ToInt32(txtsendRewardPtsexpiryday.Text) > 28)
            //{
            //    lblMsg.Text = "Send reward points expiry email on day must be between 1 to 28";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Send reward points expiry email on day must be between 1 to 28'); </script>");
            //    return;
            //}

            int RewardAutoRenewal=0, PointsConfirmedBasedOnPaymentTerms=0, RoundingToCeilingFloor=0, PointsCalcBasedOnCost = 0, PointsCalcBasedOnSales=0;
            if (chkAutoRenewal.Checked == true)
                RewardAutoRenewal = 1;
            if (chkPointConfirmOnPayTerm.Checked == true)
                PointsConfirmedBasedOnPaymentTerms = 1;
            if (rdceiling.Checked == true)
                RoundingToCeilingFloor = 1;
            if (rdcost.Checked == true)
                PointsCalcBasedOnCost = 1;
            if (rdsales.Checked == true)
                PointsCalcBasedOnSales = 1;

            string SQLstr = "  Update dbo.Reward_MasterSettings Set RewardAutoRenewal=" + RewardAutoRenewal.ToString();

            if (!string.IsNullOrEmpty(txtRewardReminderdt.Text))
            {
                SQLstr = SQLstr + ", RewardSetupReminderDayOfQrtr=" + txtRewardReminderdt.Text ;
            }
            if (!string.IsNullOrEmpty(txtReminderemialId.Text))
            {
                SQLstr = SQLstr + " , ReminderEmailId='" + txtReminderemialId.Text  +"'";
            }
            if (!string.IsNullOrEmpty(txtMinInvoiceRewardValue.Text))
            {
                SQLstr = SQLstr + ", RewardMinimumInvoiceValue='" + txtMinInvoiceRewardValue.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtMultiplicationFactor.Text))
            {
                SQLstr = SQLstr + ", RewardMultiplicationFactor=" + txtMultiplicationFactor.Text;
            }



            SQLstr = SQLstr + " , RoundingCeilingFloor=" + RoundingToCeilingFloor.ToString() + " , RewardCalcOnCost=" + PointsCalcBasedOnCost.ToString() + " , RewardCalcOnSellingPrice=" + PointsCalcBasedOnSales.ToString();

            //if (!string.IsNullOrEmpty(txtPointsExpiresAftrMonth.Text))
            //{
            //    SQLstr = SQLstr + " , RewardExpiresAfterMonth=" + txtPointsExpiresAftrMonth.Text;
            //}

            SQLstr = SQLstr + " , RewardConfirmBasedOnPayTerms = " + PointsConfirmedBasedOnPaymentTerms.ToString();

            if (!string.IsNullOrEmpty(txtPointConfirmDays.Text))
            {
                SQLstr = SQLstr + " , RewardConfirmAfterDays=" + txtPointConfirmDays.Text;
            }

            if (ddlUseINVORSETUP.SelectedItem.Text !="--Select--")
            {
                SQLstr = SQLstr + " , UseInvoiceORSetUpDaysToConfirmReward='" + ddlUseINVORSETUP.SelectedItem.Text  +"'";
            }

            if (!string.IsNullOrEmpty(txtFinanceGraceDays.Text))
            {
                SQLstr = SQLstr + " , FinanceGraceDays=" + txtFinanceGraceDays.Text;
            }

            if (!string.IsNullOrEmpty(txtcustGracePeriod.Text))
            {
                SQLstr = SQLstr + " , CustomerGraceDays=" + txtcustGracePeriod.Text;
            }

            if (!string.IsNullOrEmpty(txtRewardSubscriptionBonus.Text))
            {
                SQLstr = SQLstr + " , RewardSubscriptionpoints=" + txtRewardSubscriptionBonus.Text;
            }

            if (!string.IsNullOrEmpty(txtRewardPointsExpiresAfterMonth.Text))
            {
                SQLstr = SQLstr + " , RewardExpiresAfterMonth=" + txtRewardPointsExpiresAfterMonth.Text;
            }

            if (ddlRewardExpiry.SelectedItem.Text != "--Select--")
            {
                SQLstr = SQLstr + " , RewardExpiresAfterQHY='" + ddlRewardExpiry.SelectedItem.Text +"'";
            }

            if (ddlRewardPointExpiryFrequency.SelectedItem.Text !="--Select--")
            {
                try
                {
                    SQLstr = SQLstr + " , RewardExpiryReminderFrequency='" + ddlRewardPointExpiryFrequency.SelectedItem.Text +"'" ;
                }
                catch { }
            }

            SQLstr = SQLstr + ", LastUpdatedBy='" + myGlobal.loggedInUser() + "',LastUpdatedOn=getdate()  Where MasterSettingId=  " + lblMasterSettingId.Text;

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            Db.myExecuteSQL(SQLstr);
            lblMsg.Text = "Reward Points Master setting updated successfully";

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnSave :" + ex.Message;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Response.Redirect("Default.aspx");
    }

}