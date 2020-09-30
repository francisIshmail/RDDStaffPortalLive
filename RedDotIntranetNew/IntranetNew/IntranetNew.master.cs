using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class IntranetNew_IntranetNew : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (HttpContext.Current.User.Identity.Name == "" || myGlobal.loggedInUser()=="" )
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (HttpContext.Current.User.Identity.Name != "")
                {
                    DataSet DSMenu_Forms = null;
                    if (Session["DSMenu_Forms"] == null)
                    {
                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
                        DSMenu_Forms = Db.myGetDS(" EXEC getMenusAndFormsByUser '" + HttpContext.Current.User.Identity.Name + "'");
                        Session["DSMenu_Forms"] = DSMenu_Forms;
                    }
                    else
                    {
                        DSMenu_Forms = (DataSet) Session["DSMenu_Forms"];
                    }
                    if (DSMenu_Forms.Tables.Count > 0)
                    {
                        /// Here Show and Hide Menus for Login user Based on user Authorization
                        if (DSMenu_Forms.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < DSMenu_Forms.Tables[0].Rows.Count; i++)
                            {
                                string menuName = DSMenu_Forms.Tables[0].Rows[i][0].ToString();
                                if (menuName.Trim().ToLower() == "admin")
                                {
                                    LiAdmin.Visible = true;
                                    /// Here Show and Hide Forms under each Menu for Login user Based on user Authorization
                                    DataRow[] dtRow = DSMenu_Forms.Tables[1].Select(" MenuName='" + menuName + "'");
                                    if (dtRow.Length > 0)
                                    {
                                        foreach (DataRow row in dtRow)
                                        {
                                            if (row["FormURL"].ToString().ToLower() == "formsmaster.aspx")
                                            {
                                                ancFrmMaster.Visible = true;
                                                ancFrmMaster.InnerText=row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "authorization.aspx")
                                            {
                                                ancUserAuth.Visible = true;
                                                ancUserAuth.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "usercreationmembershipdatabase.aspx")
                                            {
                                                ancWebAdmin.Visible = true;
                                                ancWebAdmin.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "reportadmin.aspx")
                                            {
                                                ancReportAdmin.Visible = true;
                                                ancReportAdmin.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "customerstatus") /// special case for customer status
                                            {
                                                LiBPStatus.Visible = true;
                                            }
                                        }
                                    }
                                }

                                if (menuName.Trim().ToLower() == "funnel")
                                {
                                    DataRow[] dtRow = DSMenu_Forms.Tables[1].Select(" MenuName='" + menuName + "'");
                                    foreach (DataRow row in dtRow)
                                    {
                                        if (row["FormURL"].ToString().ToLower() == "funnelsetup.aspx")
                                        {
                                            ancFunnelSetup.Visible = true;
                                            ancFunnelSetup.InnerText = row["FormName"].ToString();
                                        }

                                        #region " Start : Daily Sales Report Menus Show /Hide "
                                        if (row["FormURL"].ToString().ToLower() == "dsrsetupmaster.aspx")
                                        {
                                            ancDSRSetup.Visible = true;
                                            ancDSRSetup.InnerText = row["FormName"].ToString();
                                        }
                                        if (row["FormURL"].ToString().ToLower() == "targetdesigmasternew.aspx")
                                        {
                                            ancReportReqAndTrget.Visible = true;
                                            ancReportReqAndTrget.InnerText = row["FormName"].ToString();
                                        }
                                        if (row["FormURL"].ToString().ToLower() == "modeofcallsetup.aspx")
                                        {
                                            ancModeOfCall.Visible = true;
                                            ancModeOfCall.InnerText = row["FormName"].ToString();
                                        }
                                        if (row["FormURL"].ToString().ToLower() == "viewreportnew.aspx")
                                        {
                                            ancReadReports.Visible = true;
                                            ancReadReports.InnerText = row["FormName"].ToString();
                                        }
                                        if (row["FormURL"].ToString().ToLower() == "dsr_rpt.aspx")
                                        {
                                            ancViewScoreCard.Visible = true;
                                            ancViewScoreCard.InnerText = row["FormName"].ToString();
                                        }
                                        if (row["FormURL"].ToString().ToLower() == "nextactionsetup.aspx")
                                        {
                                            ancNextAction.Visible = true;
                                            ancNextAction.InnerText = row["FormName"].ToString();
                                        }
                                        if (row["FormURL"].ToString().ToLower() == "dsrweekwise_rpt.aspx")
                                        {
                                            ancNewCustVisits.Visible = true;
                                            ancNewCustVisits.InnerText = row["FormName"].ToString();
                                        } 

                                        
                                        #endregion " End : Daily Sales Report Menus Show /Hide "
                                    
                                    }
                                }

                                if (menuName.Trim().ToLower() == "orders")
                                {
                                    LiWorkFlow.Visible = true;
                                    DataRow[] dtRow = DSMenu_Forms.Tables[1].Select(" MenuName='" + menuName + "'");
                                    foreach (DataRow row in dtRow)
                                    {
                                        if (row["FormURL"].ToString().ToLower() == "sapvieworderspo.aspx")
                                        {
                                            ancSAPPO.Visible = true;
                                            ancSAPPO.InnerText = row["FormName"].ToString();
                                        }
                                    }
                                }

                                if (menuName.Trim().ToLower() == "marketing")
                                {
                                    LiMarketnig.Visible = true;
                                    DataRow[] dtRow = DSMenu_Forms.Tables[1].Select(" MenuName='" + menuName + "'");
                                    foreach (DataRow row in dtRow)
                                    {
                                        if (row["FormURL"].ToString().ToLower() == "usercreation.aspx")
                                        {
                                            ancMrktingSetup.Visible = true;
                                            ancMrktingSetup.InnerText = row["FormName"].ToString();
                                        }
                                        if (row["FormURL"].ToString().ToLower() == "marketingplan-master.aspx")
                                        {
                                            ancMrketingPlan.Visible = true;
                                            ancMrketingPlan.InnerText = row["FormName"].ToString();
                                        }
                                    }
                                }

                                if (menuName.Trim().ToLower() == "reward")
                                {
                                    LiReward.Visible = true;
                                    /// Here Show and Hide Forms under each Menu for Login user Based on user Authorization
                                    DataRow[] dtRow = DSMenu_Forms.Tables[1].Select(" MenuName='" + menuName + "'");
                                    if (dtRow.Length > 0)
                                    {
                                        foreach (DataRow row in dtRow)
                                        {
                                            if (row["FormURL"].ToString().Trim().ToLower() == "rewardmastersetting.aspx")
                                            {
                                                ancRewardMasterSetting.Visible = true;
                                                ancRewardMasterSetting.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().Trim().ToLower() == "quarterlyrewardpercentage.aspx")
                                            {
                                                ancQrtlyRewardPercent.Visible = true;
                                                ancQrtlyRewardPercent.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().Trim().ToLower() == "recalculatereward.aspx")
                                            {
                                                ancRecalculateReward.Visible = true;
                                                ancRecalculateReward.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().Trim().ToLower() == "activaterewardforcountry.aspx")
                                            {
                                                ancActivateReward.Visible = true;
                                                ancActivateReward.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().Trim().ToLower() == "rewarduserauthorization.aspx")
                                            {
                                                ancRewardUserAuth.Visible = true;
                                                ancRewardUserAuth.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().Trim().ToLower() == "rewardledgerreport.aspx")
                                            {
                                                ancRewardLedgerRpt.Visible = true;
                                                ancRewardLedgerRpt.InnerText = row["FormName"].ToString();
                                            }
                                        }
                                    }
                                }
                                if (menuName.Trim().ToLower() == "targetandforecast")
                                {
                                    LiTarget.Visible = true;
                                    /// Here Show and Hide Forms under each Menu for Login user Based on user Authorization
                                    DataRow[] dtRow = DSMenu_Forms.Tables[1].Select(" MenuName='" + menuName + "'");
                                    if (dtRow.Length > 0)
                                    {
                                        foreach (DataRow row in dtRow)
                                        {
                                            if (row["FormURL"].ToString().ToLower() == "countrytargets.aspx")
                                            {
                                                ancCountryTrget.Visible = true;
                                                ancCountryTrget.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "designationmaster.aspx")
                                            {
                                                designationmaster.Visible = true;
                                                designationmaster.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "emailconfigurationsetting.aspx")
                                            {
                                                emailconfig.Visible = true;
                                                emailconfig.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "forecast.aspx")
                                            {
                                                forecast.Visible = true;
                                                forecast.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "salesemployeemaster.aspx")
                                            {
                                                salesemployeemaster.Visible = true;
                                                salesemployeemaster.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "salespersontargets.aspx")
                                            {
                                                salespersontarget.Visible = true;
                                                salespersontarget.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "salespersontargets_quarterly.aspx")
                                            {
                                                salepertarqur.Visible = true;
                                                salepertarqur.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "countrytargets_quarterly.aspx")
                                            {
                                                countrytarqur.Visible = true;
                                                countrytarqur.InnerText = row["FormName"].ToString();
                                            }
                                        }
                                    }
                                }

                                if (menuName.Trim().ToLower() == "sap")
                                {
                                    LiCustomerStatus.Visible = true;
                                    DataRow[] dtRow = DSMenu_Forms.Tables[1].Select(" MenuName='" + menuName + "'");
                                    if (dtRow.Length > 0)
                                    {
                                        foreach (DataRow row in dtRow)
                                        {
                                            if (row["FormURL"].ToString().ToLower() == "sapcredentials.aspx")
                                            {
                                                ancSAPCredentials.Visible = true;
                                                ancSAPCredentials.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "changebpstatus.aspx")
                                            {
                                                ancChageBPStatus.Visible = true;
                                                ancChageBPStatus.InnerText = row["FormName"].ToString();
                                            }
                                            else if (row["FormURL"].ToString().ToLower() == "pvsetup.aspx")
                                            {
                                                ancPVSetup.Visible = true;
                                                ancPVSetup.InnerText = row["FormName"].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #region Reports - Show / Hide

                        if (DSMenu_Forms.Tables.Count >= 3)
                        {
                            for (int i = 0; i < DSMenu_Forms.Tables[2].Rows.Count; i++)
                            {
                                string reportCategory = DSMenu_Forms.Tables[2].Rows[i][0].ToString();

                                if (reportCategory.ToUpper() == "FINANCE")
                                {
                                    ancFinanceRpt.Visible = true;
                                    DataRow[] dtRow = DSMenu_Forms.Tables[3].Select(" reportcategory='" + reportCategory + "'");
                                    foreach (DataRow row in dtRow)
                                    {
                                        if (row["reportType"].ToString().ToLower() == "consolidateddebtors")
                                        {
                                            ancConDebtors.Visible = true;
                                        }
                                        else if (row["reportType"].ToString().ToLower() == "consolidateddebtorsbybu")
                                        {
                                            ancConDebtorByBU.Visible = true;
                                        }
                                        else if (row["reportType"].ToString().ToLower() == "consolidateddebtorsexpanded")
                                        {
                                            ancConDebtorExpanded.Visible = true;
                                        }
                                        else if (row["reportType"].ToString().ToLower() == "consolidatedtrialbalance")
                                        {
                                            ancConTrialBalance.Visible = true;
                                        }
                                        else if (row["reportType"].ToString().ToLower() == "expenses")
                                        {
                                            ancExpenses.Visible = true;
                                        }
                                    }
                                }
                                else if (reportCategory.ToUpper() == "HR")
                                {
                                    ancHRReports.Visible = true;
                                    DataRow[] dtRow = DSMenu_Forms.Tables[3].Select(" reportcategory='" + reportCategory + "'");
                                    foreach (DataRow row in dtRow)
                                    {
                                        if (row["reportType"].ToString().ToLower() == "hr-forms")
                                        {
                                            ancHRForms.Visible = true;
                                        }
                                    }
                                }
                                else if (reportCategory.ToUpper() == "LOGISTICS")
                                {
                                    ancLogisticsRpt.Visible = true;
                                    DataRow[] dtRow = DSMenu_Forms.Tables[3].Select(" reportcategory='" + reportCategory + "'");
                                    foreach (DataRow row in dtRow)
                                    {
                                        if (row["reportType"].ToString().ToLower() == "automatedstocksheet")
                                        {
                                            ancStockSheet.Visible = true;
                                        }
                                        else if (row["reportType"].ToString().ToLower() == "inventorysheet")
                                        {
                                            ancInventorySheet.Visible = true;
                                        }
                                        else if (row["reportType"].ToString().ToLower() == "stockage")
                                        {
                                            ancStockAge.Visible = true;
                                        }
                                        else if (row["reportType"].ToString().ToLower() == "stocktrend")
                                        {
                                            ancstockTrend.Visible = true;
                                        }
                                    }
                                }
                                else if (reportCategory.ToUpper() == "MANAGEMENT")
                                {
                                    ancManagementRpt.Visible = true;
                                    DataRow[] dtRow = DSMenu_Forms.Tables[3].Select(" reportcategory='" + reportCategory + "'");
                                    foreach (DataRow row in dtRow)
                                    {
                                        if (row["reportType"].ToString().ToLower() == "weekly")
                                        {
                                            ancWeeklyGP.Visible = true;
                                        }
                                        else if (row["reportType"].ToString().ToLower() == "forecastvssales")
                                        {
                                            ancForecastSales.Visible = true;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                    }

                    #region"To show CCTV camera link"

                    string[] cctvusers = myGlobal.getAppSettingsDataForKey("CCTVAuthUsers").Split(';');
                    if (cctvusers.Contains(myGlobal.loggedInUser()))
                    {
                        ancCCTVCamera.Visible = true;
                    }
                    else
                    {
                        ancCCTVCamera.Visible = false;
                    }

                    #endregion

                }
                else
                {
                    LiAdmin.Visible = false;
                    LiReward.Visible = false;
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }

}
