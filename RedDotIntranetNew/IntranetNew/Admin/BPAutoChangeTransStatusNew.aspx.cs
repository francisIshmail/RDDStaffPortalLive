using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Admin_BPAutoChangeTransStatusNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='BPAutoChangeTransStatusNew.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    DataSet DS = Db.myGetDS("select '--select--' as DBName union all select Name as DBName from sys.databases where name like 'SAP%' And name not in ('SAPMAU','SAPEPZ','SAPKY','sapReport','SAPTLY-TEST') And name not in ( select distinct DBName from BPStatus_AutoChangeConditions) ; exec BPStatus_GetTransStatusList ");
                    if (DS.Tables.Count > 0)
                    {
                        ddlDatabase.DataSource = DS.Tables[0];
                        ddlDatabase.DataTextField = "DBName";
                        ddlDatabase.DataValueField = "DBName";
                        ddlDatabase.DataBind();

                        if (DS.Tables.Count > 1)
                        {
                            GRVTransStatusList.DataSource = DS.Tables[1];
                            GRVTransStatusList.DataBind();

                            if (DS.Tables[1].Rows.Count == 0)
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
            if (string.IsNullOrEmpty(txtDormantDays.Text) || Convert.ToInt32(txtDormantDays.Text.Trim()) == 0)
            {
                lblMsg.Text = "Please enter days to make account Dormant";
                return;
            }

            // Soft hold validation
            if (string.IsNullOrEmpty(txtFromSoftholdDueDays.Text) || string.IsNullOrEmpty(txtToSoftholdDueDays.Text))
            {
                lblMsg.Text = "Please enter From Soft hold Due Days & To Soft hold Due Days";
                return;
            }
            if (Convert.ToInt32(txtFromSoftholdDueDays.Text) >= Convert.ToInt32(txtToSoftholdDueDays.Text))
            {
                lblMsg.Text = "To Soft hold Due Days must be greater than From Soft hold Due Days";
                return;
            }
           
            if (string.IsNullOrEmpty(txtSoftholdDueAmount.Text) || Convert.ToDecimal(txtSoftholdDueAmount.Text) <= 0)
            {
                lblMsg.Text = "Please enter Due amount greater than zero for Soft hold";
                return;
            }
            if (string.IsNullOrEmpty(txtSoftholdDuePercentage.Text) || Convert.ToDecimal(txtSoftholdDuePercentage.Text) <= 0)
            {
                lblMsg.Text = "Please enter Credit Limit percentage greater than zero for Soft hold";
                return;
            }

            // Hard hold validation
            if (string.IsNullOrEmpty(txtFromHardHoldDueDays.Text) || string.IsNullOrEmpty(txtToHardHoldDueDays.Text))
            {
                lblMsg.Text = "Please enter From Hard hold Due Days & To Hard hold Due Days";
                return;
            }
            if (Convert.ToInt32(txtFromHardHoldDueDays.Text) >= Convert.ToInt32(txtToHardHoldDueDays.Text))
            {
                lblMsg.Text = "To Hard hold Due Days must be greater than From Hard hold Due Days";
                return;
            }
            if (Convert.ToInt32(txtToSoftholdDueDays.Text) >= Convert.ToInt32(txtFromHardHoldDueDays.Text))
            {
                lblMsg.Text = "From Hardhold Due Days must be greater than To Softhold Due Days";
                return;
            }

            if (string.IsNullOrEmpty(txtHardholdDueAmount.Text) || Convert.ToDecimal(txtHardholdDueAmount.Text) <= 0)
            {
                lblMsg.Text = "Please enter Due amount greater than zero for Hard hold";
                return;
            }
            if (string.IsNullOrEmpty(txtHardholdDuePercentage.Text) || Convert.ToDecimal(txtHardholdDuePercentage.Text) <= 0)
            {
                lblMsg.Text = "Please enter Credit Limit percentage greater than zero for Hard hold";
                return;
            }

            // Blocked validation
            if (string.IsNullOrEmpty(txtFromBlockedDueDays.Text) || string.IsNullOrEmpty(txtToBlockedDueDays.Text))
            {
                lblMsg.Text = "Please enter From Blocked Due Days & To Blocked Due Days";
                return;
            }
            if (Convert.ToInt32(txtFromBlockedDueDays.Text) >= Convert.ToInt32(txtToBlockedDueDays.Text))
            {
                lblMsg.Text = "To Blocked Due Days must be greater than From Blocked Due Days";
                return;
            }
            if (Convert.ToInt32(txtToHardHoldDueDays.Text) >= Convert.ToInt32(txtFromBlockedDueDays.Text))
            {
                lblMsg.Text = "From Blocked Due Days must be greater then To Hardhold Due Days";
                return;
            }
            if (string.IsNullOrEmpty(txtBlockedDueAmount.Text) || Convert.ToDecimal(txtBlockedDueAmount.Text) <= 0)
            {
                lblMsg.Text = "Please enter Due amount greater than zero for Blocked";
                return;
            }
            if (string.IsNullOrEmpty(txtBlockedDuePercentage.Text) || Convert.ToDecimal(txtBlockedDuePercentage.Text) <= 0)
            {
                lblMsg.Text = "Please enter Credit Limit percentage greater than zero for Blocked";
                return;
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            if (BtnSave.Text == "Save")
            {
                int CountOfRec = Db.myExecuteScalar(" select count(*) as NoOfRec From BPStatus_AutoChangeConditions Where DBName='" + ddlDatabase.SelectedItem.Text + "' ");
                if (CountOfRec > 0)
                {
                    lblMsg.Text = "Record already exist for " + ddlDatabase.SelectedItem.Text + " database.";
                    return;
                }
            }

            if (BtnSave.Text == "Save")
            {
                string Sqlstr = "";
                // To save Dormant Conditions
                Sqlstr = " insert into BPStatus_AutoChangeConditions(DBName,Status,FromDueDays,ToDueDays,MinDueAmount,MinCLPercentage,CreatedBy,CreatedOn) Values ('" + ddlDatabase.SelectedItem.Text + "','D',0," + txtDormantDays.Text.Trim() + ",0,0,'" + myGlobal.loggedInUser() + "',GETDATE())";
                // To save Soft Hold Conditions
                Sqlstr = Sqlstr + " insert into BPStatus_AutoChangeConditions(DBName,Status,FromDueDays,ToDueDays,MinDueAmount,MinCLPercentage,CreatedBy,CreatedOn) Values ('" + ddlDatabase.SelectedItem.Text + "','S'," + txtFromSoftholdDueDays.Text.Trim() + "," + txtToSoftholdDueDays.Text.Trim() + "," + txtSoftholdDueAmount.Text.Trim() + "," + txtSoftholdDuePercentage.Text.Trim() + ",'" + myGlobal.loggedInUser() + "',GETDATE()) ; ";
                // To save Hard Hold Conditions
                Sqlstr = Sqlstr + " insert into BPStatus_AutoChangeConditions(DBName,Status,FromDueDays,ToDueDays,MinDueAmount,MinCLPercentage,CreatedBy,CreatedOn) Values ('" + ddlDatabase.SelectedItem.Text + "','H'," + txtFromHardHoldDueDays.Text.Trim() + "," + txtToHardHoldDueDays.Text.Trim() + "," + txtHardholdDueAmount.Text.Trim() + "," + txtHardholdDuePercentage.Text.Trim() + ",'" + myGlobal.loggedInUser() + "',GETDATE()) ; ";
                // To save Blocked Conditions
                Sqlstr = Sqlstr + " insert into BPStatus_AutoChangeConditions(DBName,Status,FromDueDays,ToDueDays,MinDueAmount,MinCLPercentage,CreatedBy,CreatedOn) Values ('" + ddlDatabase.SelectedItem.Text + "','B'," + txtFromBlockedDueDays.Text.Trim() + "," + txtToBlockedDueDays.Text.Trim() + "," + txtBlockedDueAmount.Text.Trim() + "," + txtBlockedDuePercentage.Text.Trim() + ",'" + myGlobal.loggedInUser() + "',GETDATE()) ; ";

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
                Sqlstr = " Update BPStatus_AutoChangeConditions Set  ToDueDays=" + txtDormantDays.Text.Trim() + ", LastUpdatedOn=GETDATE(),LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where DBName='" + ddlDatabase.SelectedItem.Text + "' And Status='D' ; ";

                // To save Soft hold Conditions
                Sqlstr = Sqlstr + " Update BPStatus_AutoChangeConditions Set  FromDueDays=" + txtFromSoftholdDueDays.Text.Trim() + ",ToDueDays=" + txtToSoftholdDueDays.Text.Trim() + ",MinDueAmount='" + txtSoftholdDueAmount.Text.Trim() + "',MinCLPercentage='" + txtSoftholdDuePercentage.Text.Trim() + "', LastUpdatedOn=GETDATE(),LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where DBName='" + ddlDatabase.SelectedItem.Text + "' And Status='S' ; ";

                // To save Hard hold Conditions
                Sqlstr = Sqlstr + " Update BPStatus_AutoChangeConditions Set  FromDueDays=" + txtFromHardHoldDueDays.Text.Trim() + ",ToDueDays=" + txtToHardHoldDueDays.Text.Trim() + ",MinDueAmount='" + txtHardholdDueAmount.Text.Trim() + "',MinCLPercentage='" + txtHardholdDuePercentage.Text.Trim() + "', LastUpdatedOn=GETDATE(),LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where DBName='" + ddlDatabase.SelectedItem.Text + "' And Status='H' ; ";

                // To save Blocked Conditions
                Sqlstr = Sqlstr + " Update BPStatus_AutoChangeConditions Set  FromDueDays=" + txtFromBlockedDueDays.Text.Trim() + ",ToDueDays=" + txtToBlockedDueDays.Text.Trim() + ",MinDueAmount='" + txtBlockedDueAmount.Text.Trim() + "',MinCLPercentage='" + txtBlockedDuePercentage.Text.Trim() + "', LastUpdatedOn=GETDATE(),LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where DBName='" + ddlDatabase.SelectedItem.Text + "' And Status='B' ; ";

                Db.myExecuteSQL(Sqlstr);

                lblMsg.Text = " Record updated successfully.";
                BtnSave.Text = "Save";
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

            //ViewState["dirState"] = DS.Tables[0];
            //ViewState["sortdr"] = "Asc"; 
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in BindGrid() :   "+ ex.Message;
        }
    }

    private void ClearControls()
    {
        try
        {
            txtDormantDays.Text = "";
            txtFromSoftholdDueDays.Text = "";
            txtToSoftholdDueDays.Text = "";
            txtSoftholdDueAmount.Text = "";
            txtSoftholdDuePercentage.Text = "";

            txtFromHardHoldDueDays.Text = "";
            txtToHardHoldDueDays.Text = "";
            txtHardholdDueAmount.Text = "";
            txtHardholdDuePercentage.Text = "";

            txtFromBlockedDueDays.Text = "";
            txtToBlockedDueDays.Text = "";
            txtBlockedDueAmount.Text = "";
            txtBlockedDuePercentage.Text = "";
           
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

                DataSet DS = Db.myGetDS("select * from BPStatus_AutoChangeConditions Where DBName='" + lblDBName.Text + "' ");
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
                            else if (status=="S")
                            {
                                txtFromSoftholdDueDays.Text = DS.Tables[0].Rows[i]["FromDueDays"].ToString();
                                txtToSoftholdDueDays.Text = DS.Tables[0].Rows[i]["ToDueDays"].ToString();
                                txtSoftholdDueAmount.Text = DS.Tables[0].Rows[i]["MinDueAmount"].ToString();
                                txtSoftholdDuePercentage.Text = DS.Tables[0].Rows[i]["MinCLPercentage"].ToString();
                            }
                            else if (status == "H")
                            {
                                txtFromHardHoldDueDays.Text = DS.Tables[0].Rows[i]["FromDueDays"].ToString();
                                txtToHardHoldDueDays.Text = DS.Tables[0].Rows[i]["ToDueDays"].ToString();
                                txtHardholdDueAmount.Text = DS.Tables[0].Rows[i]["MinDueAmount"].ToString();
                                txtHardholdDuePercentage.Text = DS.Tables[0].Rows[i]["MinCLPercentage"].ToString();
                            }
                            else if (status == "B")
                            {
                                txtFromBlockedDueDays.Text = DS.Tables[0].Rows[i]["FromDueDays"].ToString();
                                txtToBlockedDueDays.Text = DS.Tables[0].Rows[i]["ToDueDays"].ToString();
                                txtBlockedDueAmount.Text = DS.Tables[0].Rows[i]["MinDueAmount"].ToString();
                                txtBlockedDuePercentage.Text = DS.Tables[0].Rows[i]["MinCLPercentage"].ToString();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in SelectedIndexChanged : " + ex.Message;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSave.Text = "Save";
            ClearControls();
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in Cancel : " + ex.Message;
        }
    }

    //protected void GRVTransStatusList_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    try
    //    {
    //        DataTable dtrslt = (DataTable)ViewState["dirState"];
    //        if (dtrslt.Rows.Count > 0)
    //        {
    //            if (Convert.ToString(ViewState["sortdr"]) == "Asc")
    //            {
    //                dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
    //                ViewState["sortdr"] = "Desc";
    //            }
    //            else
    //            {
    //                dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
    //                ViewState["sortdr"] = "Asc";
    //            }
    //            GRVTransStatusList.DataSource = dtrslt;
    //            GRVTransStatusList.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = " Error occured in Sorting : " + ex.Message;
    //    }
    //}
}