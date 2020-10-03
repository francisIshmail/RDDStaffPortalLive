using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class IntranetNew_Admin_BPAutoChangeTransStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='BPAutoChangeTransStatus.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    DataSet DS = Db.myGetDS("select '--select--' as DBName union all select Name as DBName from sys.databases where name like 'SAP%' And name not in ('SAPMAU','SAPEPZ','SAPKY','sapReport','SAPTLY-TEST') And name not in ( select distinct DBName from BPStatusConditions) ; select '--select--' DueDays  union all select DueDays from BPStatusDueDays  ; exec BPStatus_GetTransStatusList ");

                    if (DS.Tables.Count > 0)
                    {
                        ddlDatabase.DataSource = DS.Tables[0];
                        ddlDatabase.DataTextField = "DBName";
                        ddlDatabase.DataValueField = "DBName";
                        ddlDatabase.DataBind();
                        if (DS.Tables.Count > 1)
                        {
                            ddlSoftholdDueDays.DataSource = DS.Tables[1];
                            ddlSoftholdDueDays.DataTextField = "DueDays";
                            ddlSoftholdDueDays.DataTextField = "DueDays";
                            ddlSoftholdDueDays.DataBind();

                            ddlHardholdDueDays.DataSource = DS.Tables[1];
                            ddlHardholdDueDays.DataTextField = "DueDays";
                            ddlHardholdDueDays.DataTextField = "DueDays";
                            ddlHardholdDueDays.DataBind();

                            ddlBlockedDueDays.DataSource = DS.Tables[1];
                            ddlBlockedDueDays.DataTextField = "DueDays";
                            ddlBlockedDueDays.DataTextField = "DueDays";
                            ddlBlockedDueDays.DataBind();
                        }

                        if (DS.Tables.Count > 2)
                        {
                            GRVTransStatusList.DataSource = DS.Tables[2];
                            GRVTransStatusList.DataBind();

                            if (DS.Tables[2].Rows.Count == 0)
                            {
                                pnlTransStatusList.Visible = false;
                            }
                            else
                            {
                                pnlTransStatusList.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Failed to bind dropdown lists";
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Auto Change Transactional Status");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Pageload - " + ex.Message; 
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlDatabase.SelectedItem.Text == "--select--")
            {
                lblMsg.Text = "Please select database";
                return;
            }
            if (string.IsNullOrEmpty(txtDormantDays.Text) || Convert.ToInt32(txtDormantDays.Text.Trim())==0)
            {
                lblMsg.Text = "Please enter days to make account Dormant";
                return;
            }
            if (ddlSoftholdDueDays.SelectedItem.Text=="--select--")
            {
                lblMsg.Text = "Please select Due Days to change status to Soft hold";
                return;
            }

            if (string.IsNullOrEmpty(txtSoftholdDueAmount.Text) || Convert.ToInt32(txtSoftholdDueAmount.Text) == 0)
            {
                lblMsg.Text = "Please enter Due amount greater than zero for Soft hold";
                return;
            }

            if (string.IsNullOrEmpty(txtSoftholdDuePercentage.Text) || Convert.ToInt32(txtSoftholdDuePercentage.Text) == 0)
            {
                lblMsg.Text = "Please enter Credit Limit percentage greater than zero for Soft hold";
                return;
            }

            if (ddlHardholdDueDays.SelectedItem.Text == "--select--")
            {
                lblMsg.Text = "Please select Due Days to change status to Hardhold";
                return;
            }
            else
            {
                if (ddlHardholdDueDays.SelectedItem.Text == ddlSoftholdDueDays.SelectedItem.Text)
                {
                    lblMsg.Text = "Hard hold Due days must be greater than Soft hold Due days";
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtHardholdDueAmount.Text) || Convert.ToInt32(txtHardholdDueAmount.Text) == 0)
            {
                lblMsg.Text = "Please enter Due amount greater than zero for Hard hold";
                return;
            }

            if (string.IsNullOrEmpty(txtHardholdDuePercentage.Text) || Convert.ToInt32(txtHardholdDuePercentage.Text) == 0)
            {
                lblMsg.Text = "Please enter Credit Limit percentage greater than zero for Hard hold";
                return;
            }

            if (ddlBlockedDueDays.SelectedItem.Text == "--select--")
            {
                lblMsg.Text = "Please select Due Days to change status to Blocked";
                return;
            }
            else
            {
                if (ddlBlockedDueDays.SelectedItem.Text == ddlSoftholdDueDays.SelectedItem.Text || ddlBlockedDueDays.SelectedItem.Text == ddlHardholdDueDays.SelectedItem.Text)
                {
                    lblMsg.Text = "Blocked Due days must be greater than Soft hold Due days and Hard hold Due days";
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtBlockedDueAmount.Text) || Convert.ToInt32(txtBlockedDueAmount.Text) == 0)
            {
                lblMsg.Text = "Please enter Due amount greater than zero for Blocked";
                return;
            }

            if (string.IsNullOrEmpty(txtBlockedDuePercentage.Text) || Convert.ToInt32(txtBlockedDuePercentage.Text) == 0)
            {
                lblMsg.Text = "Please enter Credit Limit percentage greater than zero for Blocked";
                return;
            }
            
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            if (BtnSave.Text == "Save")
            {
                int CountOfRec = Db.myExecuteScalar(" select count(*) as NoOfRec From BPStatusConditions Where DBName='"+ddlDatabase.SelectedItem.Text+"' ");
                if (CountOfRec > 0)
                {
                    lblMsg.Text = "Record already exist for " + ddlDatabase.SelectedItem.Text + " database.";
                    return;
                }
            }

            string[] softholdDueDays = ddlSoftholdDueDays.SelectedItem.Text.Split('-');
            string[] HardholdDueDays = ddlHardholdDueDays.SelectedItem.Text.Split('-');

             string FromBlockedDueDays = "", ToBlockedDueDays="";
            string [] BlockedDueDays=new string[2];
            if (ddlBlockedDueDays.SelectedItem.Text.Contains("+"))
            {
                BlockedDueDays[0] = ddlBlockedDueDays.SelectedItem.Text.Replace("+", "");
                FromBlockedDueDays = BlockedDueDays[0];
                ToBlockedDueDays = "99999";
            }
            else if (ddlBlockedDueDays.SelectedItem.Text.Contains("-"))
            {
                BlockedDueDays = ddlBlockedDueDays.SelectedItem.Text.Split('-');
                FromBlockedDueDays = BlockedDueDays[0];
                ToBlockedDueDays = BlockedDueDays[1];
            }
           
            if (BtnSave.Text == "Save")
            {
                string Sqlstr = "";
                // To save Dormant Conditions
                Sqlstr = " insert into BPStatusConditions(DBName,Status,FromDueDays,ToDueDays,MinDueAmount,MinCLPercentage,CreatedBy,CreatedOn) Values ('" + ddlDatabase.SelectedItem.Text + "','D',0," + txtDormantDays.Text.Trim() + ",0,0,'" + myGlobal.loggedInUser() + "',GETDATE())";
                // To save Soft Hold Conditions
                Sqlstr = Sqlstr + " insert into BPStatusConditions(DBName,Status,FromDueDays,ToDueDays,MinDueAmount,MinCLPercentage,CreatedBy,CreatedOn) Values ('" + ddlDatabase.SelectedItem.Text + "','S'," + softholdDueDays[0] + "," + softholdDueDays[1] + "," + txtSoftholdDueAmount.Text.Trim() + "," + txtSoftholdDuePercentage.Text.Trim() + ",'" + myGlobal.loggedInUser() + "',GETDATE()) ; ";
                // To save Hard Hold Conditions
                Sqlstr = Sqlstr + " insert into BPStatusConditions(DBName,Status,FromDueDays,ToDueDays,MinDueAmount,MinCLPercentage,CreatedBy,CreatedOn) Values ('" + ddlDatabase.SelectedItem.Text + "','H'," + HardholdDueDays[0] + "," + HardholdDueDays[1] + "," + txtHardholdDueAmount.Text.Trim() + "," + txtHardholdDuePercentage.Text.Trim() + ",'" + myGlobal.loggedInUser() + "',GETDATE()) ; ";
                // To save Blocked Conditions
                Sqlstr = Sqlstr + " insert into BPStatusConditions(DBName,Status,FromDueDays,ToDueDays,MinDueAmount,MinCLPercentage,CreatedBy,CreatedOn) Values ('" + ddlDatabase.SelectedItem.Text + "','B'," + FromBlockedDueDays + ","+ToBlockedDueDays+"," + txtBlockedDueAmount.Text.Trim() + "," + txtBlockedDuePercentage.Text.Trim() + ",'" + myGlobal.loggedInUser() + "',GETDATE()) ; ";

                Db.myExecuteSQL(Sqlstr);

                lblMsg.Text = " Record saved successfully.";
                try
                {
                    ddlDatabase.Items.Remove(ddlDatabase.SelectedItem.Text);
                }
                catch { }
            }
            else  /// UPDATE DATA
            {
                string Sqlstr = "";
                // To save Dormant Conditions
                Sqlstr = " Update BPStatusConditions Set  ToDueDays="+txtDormantDays.Text.Trim()+", LastUpdatedOn=GETDATE(),LastUpdatedBy='"+myGlobal.loggedInUser()+"'  Where DBName='" + ddlDatabase.SelectedItem.Text + "' And Status='D' ; ";

                // To save Soft hold Conditions
                Sqlstr = Sqlstr + " Update BPStatusConditions Set  FromDueDays=" + softholdDueDays[0] + ",ToDueDays=" + softholdDueDays[1] + ",MinDueAmount=" + txtSoftholdDueAmount.Text.Trim() + ",MinCLPercentage=" + txtSoftholdDuePercentage.Text.Trim() + ", LastUpdatedOn=GETDATE(),LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where DBName='" + ddlDatabase.SelectedItem.Text + "' And Status='S' ; ";
                
                // To save Hard hold Conditions
                Sqlstr = Sqlstr + " Update BPStatusConditions Set  FromDueDays=" + HardholdDueDays[0] + ",ToDueDays=" + HardholdDueDays[1] + ",MinDueAmount=" + txtHardholdDueAmount.Text.Trim() + ",MinCLPercentage=" + txtHardholdDuePercentage.Text.Trim() + ", LastUpdatedOn=GETDATE(),LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where DBName='" + ddlDatabase.SelectedItem.Text + "' And Status='H' ; ";

                // To save Blocked Conditions
                Sqlstr = Sqlstr + " Update BPStatusConditions Set  FromDueDays=" + FromBlockedDueDays + ",ToDueDays=" + ToBlockedDueDays + ",MinDueAmount=" + txtBlockedDueAmount.Text.Trim() + ",MinCLPercentage=" + txtBlockedDuePercentage.Text.Trim() + ", LastUpdatedOn=GETDATE(),LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where DBName='" + ddlDatabase.SelectedItem.Text + "' And Status='B' ; ";

                Db.myExecuteSQL(Sqlstr);

                lblMsg.Text = " Record updated successfully.";
            }

            BindGrid();
            ClearControls();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Failed to save/update data : " + ex.Message;
        }
    }

    private void BindGrid()
    {
        try
        {
              DataSet DS = Db.myGetDS(" exec BPStatus_GetTransStatusList ");
              GRVTransStatusList.DataSource = DS.Tables[0];
              GRVTransStatusList.DataBind();

              if (DS.Tables[0].Rows.Count == 0)
              {
                  pnlTransStatusList.Visible = false;
              }
              else
              {
                  pnlTransStatusList.Visible = true;
              }
        }
        catch (Exception ex)
        {
        }
    }


    private void ClearControls()
    {
        try
        {
            txtDormantDays.Text = "";
            txtSoftholdDueAmount.Text = "";
            txtSoftholdDuePercentage.Text = "";
            txtHardholdDueAmount.Text = "";
            txtHardholdDuePercentage.Text = "";
            txtBlockedDueAmount.Text = "";
            txtBlockedDuePercentage.Text = "";
            try
            {
                ddlSoftholdDueDays.SelectedItem.Text = "--select--";
                ddlSoftholdDueDays.SelectedItem.Value = "--select--";
            }
            catch { }
            try
            {
                ddlHardholdDueDays.SelectedItem.Text = "--select--";
                ddlHardholdDueDays.SelectedItem.Value = "--select--";
            }
            catch { }
            try
            {
                ddlBlockedDueDays.SelectedItem.Text = "--select--";
                ddlBlockedDueDays.SelectedItem.Value = "--select--";
            }
            catch { }
            try
            {
                ddlDatabase.SelectedItem.Text = "--select--";
                ddlDatabase.SelectedItem.Value = "--select--";
            }
            catch { }

        }
        catch (Exception ex)
        {
        }
    }


    protected void GRVTransStatusList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BtnSave.Text = "Update";
            lblMsg.Text = "";
            pnlTransStatusList.Visible = false;

            GridViewRow row = GRVTransStatusList.SelectedRow;
            lblMsg.Text = "";
            Label lblDBName = (Label)row.FindControl("lblDBName");
            if (!string.IsNullOrEmpty(lblDBName.Text))
            {
                ddlDatabase.SelectedItem.Text = lblDBName.Text;
                ddlDatabase.SelectedItem.Value = lblDBName.Text;

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                DataSet DS = Db.myGetDS("select * from BPStatusConditions Where DBName='" + lblDBName.Text + "' ");
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        string status = "", DueDays = "";
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            status = DS.Tables[0].Rows[i]["Status"].ToString();
                            if (status == "D")
                            {
                                txtDormantDays.Text = DS.Tables[0].Rows[i]["ToDueDays"].ToString();
                            }
                            else
                            {
                                if (DS.Tables[0].Rows[i]["ToDueDays"].ToString() == "99999")  // This means it is last entry 30+ days or 45+ days or 60 + days etc...
                                {
                                    DueDays = DS.Tables[0].Rows[i]["FromDueDays"].ToString() + "+";
                                }
                                else
                                {
                                    DueDays = DS.Tables[0].Rows[i]["FromDueDays"].ToString() + "-" + DS.Tables[0].Rows[i]["ToDueDays"].ToString();
                                }

                                if (status == "S") // SoftHold
                                {
                                    txtSoftholdDueAmount.Text = DS.Tables[0].Rows[i]["MinDueAmount"].ToString();
                                    txtSoftholdDuePercentage.Text = DS.Tables[0].Rows[i]["MinCLPercentage"].ToString();
                                    ddlSoftholdDueDays.SelectedItem.Text = DueDays;
                                    ddlSoftholdDueDays.SelectedItem.Value= DueDays;
                                }
                                else if (status == "H") // HardHold
                                {
                                    txtHardholdDueAmount.Text = DS.Tables[0].Rows[i]["MinDueAmount"].ToString();
                                    txtHardholdDuePercentage.Text = DS.Tables[0].Rows[i]["MinCLPercentage"].ToString();
                                    ddlHardholdDueDays.SelectedItem.Text = DueDays;
                                    ddlHardholdDueDays.SelectedItem.Value = DueDays;
                                }
                                else if (status == "B") // Blocked
                                {
                                    txtBlockedDueAmount.Text = DS.Tables[0].Rows[i]["MinDueAmount"].ToString();
                                    txtBlockedDuePercentage.Text = DS.Tables[0].Rows[i]["MinCLPercentage"].ToString();
                                    ddlBlockedDueDays.SelectedItem.Text = DueDays;
                                    ddlBlockedDueDays.SelectedItem.Value = DueDays;
                                }
                            }
                        }
                    }
                }
            }
        }
        catch  (Exception ex)
        {
            lblMsg.Text = "Error occured in SelectedIndexChanged : " + ex.Message;
        }
    }
}