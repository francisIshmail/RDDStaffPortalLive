using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
public partial class Intranet_EVO_EditStockItemCSharp : System.Web.UI.Page
{  
    String constr;
    String DBCode;
    String query;
    String prefix;
    String constrEVO;
    String constrOB1;
    SqlConnection con;
    SqlConnection conEVO;
    SqlConnection conOB1;
    String ping;
    String whereclaues ;
    int partInTZ, partInTRI, partInKE, partInEPZ, partInUG;
    String category, manufacture, BU, PL, model, part, dashCategory, group, desc1, desc2, desc3, avgCost, grvcost ;
    Boolean active;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnUpdate.Attributes.Add("onClick", "return getConfirmation();");
        lblError.Text = "";
        lblSuccess.Text = "";
        constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString());
        if(IsPostBack != true) 
        {
            pnlItem.Enabled = false;
            pnlEditItem.Enabled = false;
            LoadCharacters();
        }
    }
    protected void btnConnect_Click(object sender, EventArgs e)
    {
        chkSeg2.Checked = false;
        chkSeg3.Checked = false;
        chkSeg4.Checked = false;
        chkSeg5.Checked = false;
        chkSeg6.Checked = false;

        try
        {
                    filldashboardcatagory();
                    fillcomboCategory();
        } 
        catch(Exception ex)
        {
                    lblError.Text = "Warning : while loading dashboard category there was some issue, make sure you select the dashboard category from the list. Continue anyways";
        }
        try
        {
                     LoadItem();
        } 
        catch(Exception ex)
        {
                    lblError.Text = "Can't load Web page" + ex.Message;
                    //MsgBox("Can't load Web page" & vbCrLf & ex.Message)
        }
    }

    private void LoadCharacters()
    {
         ddlCodeFilter.Items.Add("0");
        ddlCodeFilter.Items.Add("1");
        ddlCodeFilter.Items.Add("2");
        ddlCodeFilter.Items.Add("3");
        ddlCodeFilter.Items.Add("4");
        ddlCodeFilter.Items.Add("5");
        ddlCodeFilter.Items.Add("6");
        ddlCodeFilter.Items.Add("7");
        ddlCodeFilter.Items.Add("8");
        ddlCodeFilter.Items.Add("9");
        ddlCodeFilter.Items.Add("A");
        ddlCodeFilter.Items.Add("B");
        ddlCodeFilter.Items.Add("C");
        ddlCodeFilter.Items.Add("D");
        ddlCodeFilter.Items.Add("E");
        ddlCodeFilter.Items.Add("F");
        ddlCodeFilter.Items.Add("G");
        ddlCodeFilter.Items.Add("H");
        ddlCodeFilter.Items.Add("I");
        ddlCodeFilter.Items.Add("J");
        ddlCodeFilter.Items.Add("K");
        ddlCodeFilter.Items.Add("L");
        ddlCodeFilter.Items.Add("M");
        ddlCodeFilter.Items.Add("N");
        ddlCodeFilter.Items.Add("O");
        ddlCodeFilter.Items.Add("P");
        ddlCodeFilter.Items.Add("Q");
        ddlCodeFilter.Items.Add("R");
        ddlCodeFilter.Items.Add("S");
        ddlCodeFilter.Items.Add("T");
        ddlCodeFilter.Items.Add("U");
        ddlCodeFilter.Items.Add("V");
        ddlCodeFilter.Items.Add("W");
        ddlCodeFilter.Items.Add("X");
        ddlCodeFilter.Items.Add("Y");
        ddlCodeFilter.Items.Add("Z");
        ddlCodeFilter.Items.Add("Others");
    }

    protected void ddlSimpleCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkSeg2.Enabled = true;
        chkSeg2.Checked = false;
        chkSeg3.Enabled = false;
        chkSeg3.Checked = false;
        chkSeg4.Enabled = false;
        chkSeg4.Checked = false;
        chkSeg5.Enabled = false;
        chkSeg5.Checked = false;
        chkSeg6.Enabled = false;
        chkSeg6.Checked = false;
        chkNewModel.Enabled = false;
        chkNewPL.Enabled = false;
        cmbBUTRI.Enabled = false;
        cmbModelTRI.Enabled = false;
        cmbPLTRI.Enabled = false;

        if (!getItemExistence())
            return;

        try
        {
            FillDetails();
        }
        catch (Exception ex)
        {
            lblError.Text = "Warning : Few Fields could not be set according to database values , may be for inconsistent data in the database";
        }

        try
        {
            setCombos();
        } 
        catch(Exception ex)
        {
            lblError.Text = "Warning : Dropdowns could not be set according to database values , may be for inconsistent data in the database"; 
        }
    }

    private void LoadItem()
    {
        whereclaues = "";

        if (ddlCodeFilter.SelectedItem.Text == "Others")
            whereclaues = " WHERE lower(code) not like lower('0%') and lower(code) not like lower('1%') and lower(code) not like lower('2%') and lower(code) not like lower('3%') and lower(code) not like lower('4%') and lower(code) not like lower('5%') and lower(code) not like lower('6%') and lower(code) not like lower('7%') and lower(code) not like lower('8%') and lower(code) not like lower('9%') and lower(code) not like lower('A%') and lower(code) not like lower('B%') and lower(code) not like lower('C%') and lower(code) not like lower('D%') and lower(code) not like lower('E%') and lower(code) not like lower('F%') and lower(code) not like lower('G%') and lower(code) not like lower('H%') and lower(code) not like lower('I%') and lower(code) not like lower('J%') and lower(code) not like lower('K%') and lower(code) not like lower('L%') and lower(code) not like lower('M%') and lower(code) not like lower('N%') and lower(code) not like lower('O%') and lower(code) not like lower('P%') and lower(code) not like lower('Q%') and lower(code) not like lower('R%') and lower(code) not like lower('S%') and lower(code) not like lower('T%') and lower(code) not like lower('U%') and lower(code) not like lower('V%') and lower(code) not like lower('W%') and lower(code) not like lower('X%') and lower(code) not like lower('Y%') and lower(code) not like lower('Z%') ";
        else
            whereclaues = " WHERE lower(code) like lower('" + ddlCodeFilter.SelectedItem.Text + "%') ";

        query = "Select stockLink,code,csimplecode,itemgroup,iInvSegValue1ID ,iInvSegValue2ID ,iInvSegValue3ID ,iInvSegValue4ID ,iInvSegValue5ID ,iInvSegValue6ID ,iInvSegValue7ID,ulIIdashboardCategory,description_1,description_2,description_3,cExtDescription,AveUCst ,LatUCst ,LowUCst ,HigUCst,StdUCst,fItemLastGRVCost,ItemActive,ucIICreatedBy,udIICreationDate from stkitem " + whereclaues + " order by code";
        //query = "select * from stkitem where code like '%galaxy%'"

        Db.constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString());
        Session["TblItemsOfDB"] = Db.myGetDS(query).Tables[0];

        try
        {
            ddlSimpleCode.DataSource = (DataTable) Session["TblItemsOfDB"];
            ddlSimpleCode.DataTextField = "code";
            ddlSimpleCode.DataValueField = "code";
            ddlSimpleCode.DataBind();
            lblConMsg.Text = "Connected";
        }
         catch(Exception ex)
        {
             lblError.Text = "Error : Connection to database failed, kindly retry later";
             return;
        }

        //lblError.Visible = true


        if (ddlSimpleCode.Items.Count > 0) 
        {
            lblSimpleCodeCount.Text = "(" + ddlSimpleCode.Items.Count.ToString() + ") Items Found";

            if (!getItemExistence())
                return;

            try
            {
                FillDetails();
                btnConnect.Enabled = false;
            }
            catch (Exception ex)
            {
                lblError.Text = "Warning : Few Fields could not be set according to database values , may be for inconsistent data in the database";
            }

            try
            {

                setCombos();
            }
            catch (Exception ex)
            {
                lblError.Text = "Warning : Dropdowns could not be set according to database values , may be for inconsistent data in the database";
            }

            pnlItem.Enabled = true;
        }
        else
        {
            lblSimpleCodeCount.Text = "";
            lblSimpleCodeCount.Text = "(0) Items Found";
        }

    }
        
    protected void  ddlDB_SelectedIndexChanged(object sender, EventArgs e)
    {
        ResetAll();
    }
    protected void  ddlCodeFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ResetAll();
    }

    private Boolean  getItemExistenceOnUpdate()  //'work here
    {
        String tstr;
        lblError.Text = "Verifying Existence of new part no. in all dbs";

        try
        {
            SqlDataReader drd1;
            //tstr = "declare @tz int; declare @tri int; set @tz=0; set @tri=0; select top 1 @tz=stockLink  from [RedDotTanzania].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%'))) ; select top 1 @tri=stockLink from [Triangle].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%'))) ;select @tz as tz,@tri as tri ;"
            tstr = "declare @tz int; declare @tri int; set @tz=0; set @tri=0; select top 1 @tz=stockLink  from [RedDotTanzania].[dbo].stkitem where (lower(Code)<>lower('" + lblCode.Text + "')) and ((lower(Code)=lower('" + lblUniqueDesc.Text + "'))) ; select top 1 @tri=stockLink from [Triangle].[dbo].stkitem where (lower(Code)<>lower('" + lblCode.Text + "')) and ((lower(Code)=lower('" + lblUniqueDesc.Text + "'))) ;select @tz as tz,@tri as tri ;";
            Db.constr = myGlobal.getConnectionStringForDB("EVO");
            drd1 = Db.myGetReader(tstr);

            drd1.Read();
            partInTZ = Convert.ToInt32(drd1["tz"]);
            partInTRI = Convert.ToInt32(drd1["tri"]);
            drd1.Close();

            //---------------for OB1----------------

            //tstr = "declare @ke int; declare @epz int;declare @ug int;set @ke=0;set @epz=0;set @ug=0;select top 1 @ke=stockLink from [Red Dot Distribution Limited - Kenya].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%'))) ; select top 1 @epz=stockLink  from [RED DOT DISTRIBUTION EPZ LTD].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%'))) ;  select top 1 @ug=stockLink  from [UgandaKE].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%'))) ;  select @ke as ke,@epz as epz,@ug as ug ;"
            tstr = "declare @ke int; declare @epz int;declare @ug int;set @ke=0;set @epz=0;set @ug=0;select top 1 @ke=stockLink from [Red Dot Distribution Limited - Kenya].[dbo].stkitem where (lower(Code)<>lower('" + lblCode.Text + "')) and ((lower(Code)=lower('" + lblUniqueDesc.Text + "'))) ; select top 1 @epz=stockLink  from [RED DOT DISTRIBUTION EPZ LTD].[dbo].stkitem where (lower(Code)<>lower('" + lblCode.Text + "')) and ((lower(Code)=lower('" + lblUniqueDesc.Text + "'))) ;  select top 1 @ug=stockLink  from [UgandaKE].[dbo].stkitem where (lower(Code)<>lower('" + lblCode.Text + "')) and ((lower(Code)=lower('" + lblUniqueDesc.Text + "'))) ;  select @ke as ke,@epz as epz,@ug as ug ;";
            Db.constr = myGlobal.getConnectionStringForDB("OB1");
            drd1 = Db.myGetReader(tstr);

            drd1.Read();
            partInKE = Convert.ToInt32(drd1["ke"]);
            partInEPZ = Convert.ToInt32(drd1["epz"]);
            partInUG = Convert.ToInt32(drd1["ug"]);
            drd1.Close();
        }
        catch(Exception ex)
        {
            lblError.Text = "Error : Newly formed unique code could not be verified for it's existence in all the databases, " + ex.Message + " Please retry.";
            return true;  //as per the condition here we have to return true to stop here
        }   

        Boolean flg;
        String msg;
        flg = false;
        msg = "Error : Can't Update current Stock Item because exactly the same Part No. '" + lblUniqueDesc.Text + "' is already present in the databases : ";

        if (partInTZ > 0 && CheckStatusTZ.Checked)
         {   
            msg = msg + " TZ ,";
            flg = true;
         }

        if (partInTRI > 0 && CheckStatusDU.Checked)
        {
            msg = msg + " TRI ,";
            flg = true;
        }

        if (partInKE > 0 && CheckStatusKE.Checked)
        {
            msg = msg + " KE ,";
            flg = true;
        }
         
        if (partInEPZ > 0 && CheckStatusEPZ.Checked)
        {
            msg = msg + " EPZ ,";
            flg = true;
        }

        if (partInUG > 0 && CheckStatusUG.Checked)
        {
            msg = msg + " UG ,";
            flg = true;
        }

        msg = msg + " Please retry giving a different value in Simple Code field.";

        if( flg == true)
        {
            //'MsgBox(msg)
            lblError.Text = msg;
        }

        lblError.Text = "verified.";
        return flg;
        }

    private Boolean  getItemExistence()  //'work here
    {
        lblError.Text = "Verifying Existence of part no. in all dbs";
         String tstr;

        try
        {
            SqlDataReader drd1;
            tstr = "declare @tz int; declare @tri int; set @tz=0; set @tri=0; select top 1 @tz=stockLink  from [RedDotTanzania].[dbo].stkitem where (lower(Code)=lower('" + ddlSimpleCode.SelectedItem.Text + "')); select top 1 @tri=stockLink from [Triangle].[dbo].stkitem where (lower(Code)=lower('" + ddlSimpleCode.SelectedItem.Text + "'));select @tz as tz,@tri as tri ;";
            Db.constr = myGlobal.getConnectionStringForDB("EVO");
            drd1 = Db.myGetReader(tstr);
                
            drd1.Read();
            partInTZ = Convert.ToInt32(drd1["tz"]);
            partInTRI = Convert.ToInt32(drd1["tri"]);
            drd1.Close();

            //---------------for OB1----------------

            tstr = "declare @ke int; declare @epz int;declare @ug int;set @ke=0;set @epz=0;set @ug=0;select top 1 @ke=stockLink from [Red Dot Distribution Limited - Kenya].[dbo].stkitem where (lower(Code)=('" + ddlSimpleCode.SelectedItem.Text + "')); select top 1 @epz=stockLink  from [RED DOT DISTRIBUTION EPZ LTD].[dbo].stkitem where (lower(Code)=lower('" + ddlSimpleCode.SelectedItem.Text + "'));select top 1 @ug=stockLink  from [UgandaKE].[dbo].stkitem where (lower(Code)=lower('" + ddlSimpleCode.SelectedItem.Text + "'))select @ke as ke,@epz as epz,@ug as ug ;";
            Db.constr = myGlobal.getConnectionStringForDB("OB1");
            drd1 = Db.myGetReader(tstr);

           drd1.Read();
            partInKE = Convert.ToInt32(drd1["ke"]);
            partInEPZ = Convert.ToInt32(drd1["epz"]);
            partInUG = Convert.ToInt32(drd1["ug"]);
            drd1.Close();
        }
        catch(Exception ex)
        {
            lblError.Text = "Error : part no. could not be verified in all databases, " + ex.Message + " Please retry.";
            //ddlSimpleCode.SelectedIndex = 0;
            return false;
        }   

        if (partInTZ > 0)
        {
            CheckStatusTZ.Checked = true;
            CheckStatusTZ.ForeColor=Color.Green;
        }
        else
        {
            CheckStatusTZ.Checked = false;
            CheckStatusTZ.ForeColor = Color.Red;
        }

        if (partInTRI > 0)
        {
            CheckStatusDU.Checked = true;
            CheckStatusDU.ForeColor=Color.Green;
        }
        else
        {
            CheckStatusDU.Checked = false;
            CheckStatusDU.ForeColor = Color.Red;
        }

        if (partInKE > 0)
        {
            CheckStatusKE.Checked = true;
            CheckStatusKE.ForeColor=Color.Green;
        }
        else
        {
            CheckStatusKE.Checked = false;
            CheckStatusKE.ForeColor = Color.Red;
        }

        if (partInEPZ > 0)
        {
            CheckStatusEPZ.Checked = true;
            CheckStatusEPZ.ForeColor=Color.Green;
        }
        else
        {
            CheckStatusEPZ.Checked = false;
            CheckStatusEPZ.ForeColor = Color.Red;
        }

        if (partInUG > 0)
        {
            CheckStatusUG.Checked = true;
            CheckStatusUG.ForeColor=Color.Green;
        }
        else
        {
            CheckStatusUG.Checked = false;
            CheckStatusUG.ForeColor = Color.Red;
        }
        
        pnlEditItem.Enabled = true;

        lblError.Text = "verified.";

        return true;
    }

    private void ResetAll()
    {
        lblColumnCount.Text = "0";
        lblConMsg.Text = "Not Connected";
        btnConnect.Enabled = true;
        //lblError.Visible = false;
        pnlItem.Enabled = false;
        pnlEditItem.Enabled = false;
        lblCode.Text = "";
        lblUniqueDesc.Text = "";
        txtSimpleCode.Text = "";
        lblSimpleCodeCount.Text = "";
        chkSeg2.Checked = false;
        chkSeg3.Checked = false;
        chkSeg4.Checked = false;
        chkSeg5.Checked = false;
        chkSeg6.Checked = false;
        CheckStatusDU.Checked = false;
        CheckStatusEPZ.Checked = false;
        CheckStatusKE.Checked = false;
        CheckStatusTZ.Checked = false;
        CheckStatusUG.Checked = false;
        CheckStatusDU.Enabled = false;
        CheckStatusEPZ.Enabled = false;
        CheckStatusKE.Enabled = false;
        CheckStatusTZ.Enabled = false;
        CheckStatusUG.Enabled = false;

        CheckStatusTZ.ForeColor = Color.Black;
        CheckStatusDU.ForeColor = Color.Black;
        CheckStatusKE.ForeColor = Color.Black;
        CheckStatusEPZ.ForeColor =Color.Black;
        CheckStatusUG.ForeColor = Color.Black;

        ddlSimpleCode.Items.Clear();
        cmbBUTRI.Items.Clear();
        cmbCatTRI.Items.Clear();
        cmbManTRI.Items.Clear();
        cmbModelTRI.Items.Clear();
        cmbPartTRI.Items.Clear();
        cmbPLTRI.Items.Clear();
        cmbDashCategory.Items.Clear();
        cmbGroup.Items.Clear();
        txtDescription1.Text = "";
        txtDescription2.Text = "";
        txtDescription3.Text = "";
        txtavgCost.Text = "";
        txtgrvCost.Text = "";
        cmbIsActive.SelectedIndex = 0;
        txtModelTRI.Text = "";
        txtPLTRI.Text = "";
        txtModelTRI.Visible = false;
        txtPLTRI.Visible = false;
        cmbModelTRI.Visible = true;
        cmbPLTRI.Visible = true;
        chkNewModel.Checked = false;
        chkNewPL.Checked = false;

        }

     private void FillDetails()
     {
        DataTable dtt;
        
        //lblError.Text = ddlSimpleCode.SelectedItem.Text + " ------ " + ddlSimpleCode.SelectedValue.ToString()

        //query = "Select stockLink,code,csimplecode,itemgroup,iInvSegValue1ID ,iInvSegValue2ID ,iInvSegValue3ID ,iInvSegValue4ID ,iInvSegValue5ID ,iInvSegValue6ID ,iInvSegValue7ID,ulIIdashboardCategory,description_1,description_2,description_3,cExtDescription,AveUCst ,LatUCst ,LowUCst ,HigUCst,StdUCst,fItemLastGRVCost,ItemActive,ucIICreatedBy,udIICreationDate from stkitem where code='" + ddlSimpleCode.SelectedItem.Text + "' order by code"
        //Db.constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString())
        //dtt = Db.myGetDS(query).Tables(0)

        dtt = (DataTable) Session["TblItemsOfDB"];

    foreach (DataRow dr in dtt.Rows)
	{
            if( dr["code"].ToString() == ddlSimpleCode.SelectedItem.Text)
            {
                txtSimpleCode.Text = dr["csimplecode"].ToString();
                lblCode.Text = dr["code"].ToString();

                if (dr["iInvSegValue1ID"]!=DBNull.Value) 
                {
                    if (dr["iInvSegValue1ID"].ToString() != "0")
                        category = dr["iInvSegValue1ID"].ToString();
                    else
                        category = "-1";
                }


                if (dr["iInvSegValue2ID"]!=DBNull.Value) 
                {
                    if (dr["iInvSegValue2ID"].ToString() != "0")
                        manufacture = dr["iInvSegValue2ID"].ToString();
                    else
                        manufacture = "-1";
                }

                if (dr["iInvSegValue3ID"]!=DBNull.Value) 
                {
                    if (dr["iInvSegValue3ID"].ToString() != "0")
                        BU = dr["iInvSegValue3ID"].ToString();
                    else
                        BU = "-1";
                }

                if (dr["iInvSegValue5ID"]!=DBNull.Value) 
                {
                    if (dr["iInvSegValue5ID"].ToString() != "0")
                        PL = dr["iInvSegValue5ID"].ToString();
                    else
                        PL = "-1";
                }

                if (dr["iInvSegValue4ID"]!=DBNull.Value) 
                {
                    if (dr["iInvSegValue4ID"].ToString() != "0")
                        model = dr["iInvSegValue4ID"].ToString();
                    else
                        model = "-1";
                }

                if (dr["iInvSegValue6ID"]!=DBNull.Value) 
                {
                    if (dr["iInvSegValue6ID"].ToString() != "0")
                        part = dr["iInvSegValue6ID"].ToString();
                    else
                        part = "-1";
                }


                if (dr["ulIIdashboardCategory"]!=DBNull.Value) 
                    dashCategory = dr["ulIIdashboardCategory"].ToString();
                else
                    dashCategory = "-1";

                if (dr["Description_1"]!=DBNull.Value) 
                    desc1 = dr["Description_1"].ToString();
                else
                    desc1 = "-1";

                if (dr["Description_2"]!=DBNull.Value) 
                    desc2 = dr["Description_2"].ToString();
                else
                    desc2 = "-1";


                if (dr["Description_3"]!=DBNull.Value) 
                    desc3 = dr["Description_3"].ToString();
                else
                    desc3 = "-1";
                
                if (dr["AveUCst"]!=DBNull.Value) 
                    avgCost = dr["AveUCst"].ToString();
                else
                    avgCost = "-1";

                if (dr["LatUCst"]!=DBNull.Value) 
                    grvcost = dr["LatUCst"].ToString();
                else
                    grvcost = "-1";

               if (dr["ItemGroup"]!=DBNull.Value) 
                    group = dr["ItemGroup"].ToString();
                else
                    group = "-1";

               if (dr["ItemActive"] != DBNull.Value)
                   active = Convert.ToBoolean(dr["ItemActive"]);
               else
                   active = false;

               break;
            }
    }

        load_groups();
        txtDescription1.Text = desc1;
        txtDescription2.Text = desc2;
        txtDescription3.Text = desc3;
        txtavgCost.Text = avgCost;
        txtgrvCost.Text = grvcost;
        if (active == true)
            cmbIsActive.SelectedIndex = 0;
        else
            cmbIsActive.SelectedIndex = 1;

        }

     
    private void setCombos()
    {
        //Setting Dropdowns.......


        if(cmbDashCategory.Items.Count > 0)
        {
            ListItem tt;
            if (dashCategory != "-1")
            {
                tt = cmbDashCategory.Items.FindByText(dashCategory);

                if (tt != null)
                {
                    cmbDashCategory.ClearSelection();
                    cmbDashCategory.Items.FindByText(dashCategory).Selected = true;
                }
            }
        }

        if (cmbCatTRI.Items.Count > 0)
        {
            if(category != "-1")
            {
                cmbCatTRI.ClearSelection();
                cmbCatTRI.Items.FindByValue(category).Selected = true;
                
                //cmbCatTRI.Items.FindByValue(category).Selected = true;
                //cmbCatTRI.SelectedValue = category;
              
                //ListItem lst;
                //cmbCatTRI.ClearSelection();
                //lst = cmbCatTRI.Items.FindByValue(category);
                //cmbCatTRI.Items.FindByText(lst.Text).Selected = true;
                
                //manLoad()
            }
        }

        if (cmbManTRI.Items.Count > 0)
        {
            if (manufacture != "-1")
            {
                cmbManTRI.ClearSelection();
                cmbManTRI.Items.FindByValue(manufacture).Selected = true;
                //cmbManTRI.SelectedValue = manufacture;
                
                //buLoad()
                chkSeg2.Checked = true;
                chkSeg2.Enabled = true;
                cmbManTRI.Enabled = true;

                chkSeg3.Enabled = true;

            }
        }

        if (cmbBUTRI.Items.Count > 0)
        {
            if(BU != "-1")
            {
                cmbBUTRI.ClearSelection();
                cmbBUTRI.Items.FindByValue(BU).Selected = true;
                //cmbBUTRI.SelectedValue = BU;
                
                //plLoad()
                chkSeg3.Checked = true;
                chkSeg3.Enabled = true;
                cmbBUTRI.Enabled = true;

                chkSeg4.Enabled = true;
                
            }
        }

        if (cmbPLTRI.Items.Count > 0)
        {
            if(model != "-1")
            {
                cmbPLTRI.ClearSelection();
                cmbPLTRI.Items.FindByValue(model).Selected = true;
                //cmbPLTRI.SelectedValue = model;
                
                //modelLoad()
                chkSeg4.Checked = true;
                chkSeg4.Enabled = true;
                cmbPLTRI.Enabled = true;
                chkNewPL.Enabled = true;

                chkSeg5.Enabled = true;

            }
        }

        if (cmbModelTRI.Items.Count > 0)
        {
            if( PL != "-1")
            {
                cmbModelTRI.ClearSelection();
                cmbModelTRI.Items.FindByValue(PL).Selected = true;
                //cmbModelTRI.SelectedValue = PL;
                
                //partLoad()
                chkSeg5.Checked = true;
                chkSeg5.Enabled = true;
                cmbModelTRI.Enabled = true;
                chkNewModel.Enabled = true;
            }
        }

        if (cmbPartTRI.Items.Count > 0)
        {
            if( part != "-1")
            {
                cmbPartTRI.ClearSelection();
                cmbPartTRI.Items.FindByValue(part).Selected = true;
                
                //cmbPartTRI.SelectedValue = part;
                chkSeg6.Checked = true;
                chkSeg6.Enabled = true;
                cmbPartTRI.Enabled = true;
            }
        }

    }

    private void load_groups()
    {
        query = "select * from dbo.GrpTbl";
        Db.LoadDDLsWithCon(cmbGroup, query, "StGroup", "idGrpTbl", constr);
        if (cmbGroup.Items.Count > 0) 
        {
     
           if (group != "")
                cmbGroup.Items.FindByText(group).Selected = true;
        }
    }

    private void filldashboardcatagory()
    {
        query = "select * from tej.[dbo].tblDashboardCategory order by dashboardCategoryName";
        Db.LoadDDLsWithCon(cmbDashCategory, query, "dashboardCategoryName", "autoindex", myGlobal.getConnectionStringForDB("EVO"));
        
        if(cmbDashCategory.Items.Count > 0)
            lblColumnCount.Text = "(" + cmbDashCategory.Items.Count.ToString() + ")";
    }

     private void fillcomboCategory()
     {
         try
         {
             lblError.Text = "";
            constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString());
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=1 order by cValue";
            Db.LoadDDLsWithCon(cmbCatTRI, query, "cValue", "idInvSegValue", constr);
            cmbCatTRI.Enabled = true;

            if (cmbCatTRI.Items.Count > 0)
                manLoad();
            else
            {
                cmbManTRI.Items.Clear();
                cmbBUTRI.Items.Clear();
                cmbPLTRI.Items.Clear();
                cmbModelTRI.Items.Clear();
                cmbPartTRI.Items.Clear();
            }
         }
        catch(Exception ex)
        {
            lblError.Text = "Error :" + ex.Message;
            cmbManTRI.Items.Clear();
            cmbBUTRI.Items.Clear();
            cmbPLTRI.Items.Clear();
            cmbModelTRI.Items.Clear();
            cmbPartTRI.Items.Clear();
        }
     }

    private void manLoad()
    {
        try
       {
            //query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=2 and idInvSegValue in (select distinct(iInvSegValue2ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "') order by cValue"
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=2 order by cValue";
            Db.LoadDDLsWithCon(cmbManTRI, query, "cValue", "idInvSegValue", constr);
            if (cmbManTRI.Items.Count > 0)
                buLoad();
            else
            {
                cmbBUTRI.Items.Clear();
                cmbPLTRI.Items.Clear();
                cmbModelTRI.Items.Clear();
                cmbPartTRI.Items.Clear();
            }
        }
        catch(Exception ex)
        {
            lblError.Text = "Error :" + ex.Message;
            cmbBUTRI.Items.Clear();
            cmbPLTRI.Items.Clear();
            cmbModelTRI.Items.Clear();
            cmbPartTRI.Items.Clear();
        }
    }

    private void buLoad()
    {
        try
        {
            //quert = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=3 and idInvSegValue in (select distinct(iInvSegValue3ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' ) order by cValue"
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=3 order by cValue";
            Db.LoadDDLsWithCon(cmbBUTRI, query, "cValue", "idInvSegValue", constr);
            if (cmbBUTRI.Items.Count > 0) 
                plLoad();
            else
            {
                cmbPLTRI.Items.Clear();
                cmbModelTRI.Items.Clear();
                cmbPartTRI.Items.Clear();
            }
        }
        catch(Exception ex)
        {
            lblError.Text = "Error :" + ex.Message;
            cmbPLTRI.Items.Clear();
            cmbModelTRI.Items.Clear();
            cmbPartTRI.Items.Clear();
        }
    }

    private void plLoad()
    {
        try
        {
            //quert = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=5 and idInvSegValue in (select distinct(iInvSegValue4ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' and iInvSegValue3ID='" & cmbBUTRI.SelectedValue & "' ) order by cValue"
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=5 order by cValue";
            Db.LoadDDLsWithCon(cmbPLTRI, query, "cValue", "idInvSegValue", constr);
            if (cmbPLTRI.Items.Count > 0)
                modelLoad();
            else
            {
                cmbModelTRI.Items.Clear();
                cmbPartTRI.Items.Clear();
            }
        }
        catch(Exception ex)
        {
            lblError.Text = "Error :" + ex.Message;
            cmbModelTRI.Items.Clear();
            cmbPartTRI.Items.Clear();
        }
    }

    private void modelLoad()
    {
        try
        {
            //quert = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=4 and idInvSegValue in (select distinct(iInvSegValue5ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' and iInvSegValue3ID='" & cmbBUTRI.SelectedValue & "'and iInvSegValue4ID='" & cmbPLTRI.SelectedValue & " ') order by cValue"
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=4 order by cValue";
            Db.LoadDDLsWithCon(cmbModelTRI, query, "cValue", "idInvSegValue", constr);
            if (cmbModelTRI.Items.Count > 0)
                partLoad();
            else
                cmbPartTRI.Items.Clear();
            
       }
        catch(Exception ex)
        {
            lblError.Text = "Error :" + ex.Message;
            cmbPartTRI.Items.Clear();
            }
    }
    private void partLoad()
    {
        try
        {
            //quert = "select idInvSegValue,cValue from dbo._etblInvSegValue where idInvSegValue in (select distinct(iInvSegValue6ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' and iInvSegValue3ID='" & cmbBUTRI.SelectedValue & "'and iInvSegValue4ID='" & cmbPLTRI.SelectedValue & "' and iInvSegValue5ID='" & cmbModelTRI.SelectedValue & "'  ) order by cValue"
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=6 order by cValue";
            Db.LoadDDLsWithCon(cmbPartTRI, query, "cVAlue", "idInvSegValue", constr);
        }
        catch(Exception ex)
        {
            lblError.Text = "Error :" + ex.Message;
         }
    }

    protected void chkSeg2_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSeg2.Checked == true)
        {
            chkSeg3.Enabled = true;
            //chkSeg4.Enabled = true;
            //chkSeg5.Enabled = true;
            //chkSeg6.Enabled = true;
            cmbManTRI.Enabled = true;
            //cmbBUTRI.Enabled = false;
            //cmbPLTRI.Enabled = false;
            //cmbModelTRI.Enabled = false;
            cmbPartTRI.Enabled = false;
        }
        else
        {

            chkSeg3.Enabled = false;
            chkSeg4.Enabled = false;
            chkSeg5.Enabled = false;
            chkSeg6.Enabled = false;
            cmbManTRI.Enabled = false;
            cmbBUTRI.Enabled = false;
            cmbPLTRI.Enabled = false;
            cmbModelTRI.Enabled = false;
            cmbPartTRI.Enabled = false;
            chkSeg3.Checked = false;
            chkSeg4.Checked = false;
            chkSeg5.Checked = false;
            chkSeg6.Checked = false;

            chkNewModel.Enabled = false;
            chkNewPL.Enabled = false;

            chkNewModel.Checked = false;
            chkNewPL.Checked = false;
            handleModelFields(false);
            handlePLFields(false);
        }
    }
    protected void chkSeg3_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSeg3.Checked == true)
        {

        chkSeg4.Enabled = true;
            //chkSeg5.Enabled = true;
            //chkSeg6.Enabled = true;
            cmbBUTRI.Enabled = true;
            //cmbPLTRI.Enabled = false;
            //cmbModelTRI.Enabled = false;
            cmbPartTRI.Enabled = false;
        }
        else
        {
            chkSeg4.Enabled = false;
            chkSeg5.Enabled = false;
            chkSeg6.Enabled = false;
            cmbBUTRI.Enabled = false;
            cmbPLTRI.Enabled = false;
            cmbModelTRI.Enabled = false;
            cmbPartTRI.Enabled = false;
            chkSeg4.Checked = false;
            chkSeg5.Checked = false;
            chkSeg6.Checked = false;

            chkNewModel.Enabled = false;
            chkNewPL.Enabled = false;

            chkNewModel.Checked = false;
            chkNewPL.Checked = false;
            handleModelFields(false);
            handlePLFields(false);

        }
    }
    protected void chkSeg4_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSeg4.Checked == true)
        {
             chkSeg5.Enabled = true;
            chkSeg6.Enabled = true;
            cmbPLTRI.Enabled = true;
            txtPLTRI.Enabled = true;
            cmbModelTRI.Enabled = false;
            cmbPartTRI.Enabled = false;

            chkNewPL.Enabled = true;

        }
        else
        {
            chkSeg5.Enabled = false;
            chkSeg6.Enabled = false;
            cmbPLTRI.Enabled = false;
            txtPLTRI.Enabled = false;
            cmbModelTRI.Enabled = false;
            cmbPartTRI.Enabled = false;
            chkSeg5.Checked = false;
            chkSeg6.Checked = false;

            chkNewModel.Enabled = false;
            chkNewPL.Enabled = false;

            chkNewModel.Checked = false;
            chkNewPL.Checked = false;
            handleModelFields(false);
            handlePLFields(false);
        }
    }
    protected void chkSeg5_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSeg5.Checked == true)
        {

        chkSeg5.Enabled = true;
            chkSeg6.Enabled = true;
            cmbPartTRI.Enabled = false;

            if (chkNewPL.Checked == true)
            {
                cmbModelTRI.Enabled = false;
                cmbModelTRI.Visible = false;
                txtModelTRI.Visible = true;
                txtModelTRI.Enabled = true;
                chkNewModel.Enabled = false;
                chkNewModel.Checked = true;
            }
            else
            {
                cmbModelTRI.Enabled = true;
                cmbModelTRI.Visible = true;
                txtModelTRI.Visible = false;
                txtModelTRI.Enabled = true;
                chkNewModel.Enabled = true;
                chkNewModel.Checked = false;
            }
            
        }
        else
        {
            chkSeg6.Enabled = false;
            cmbModelTRI.Enabled = false;
            txtModelTRI.Enabled = false;
            cmbPartTRI.Enabled = false;
            chkSeg6.Checked = false;

            chkNewModel.Enabled = false;

            chkNewModel.Checked = false;
            handleModelFields(false);
            
        }
    }
    protected void chkSeg6_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSeg6.Checked == true)
          cmbPartTRI.Enabled = true;
        else
          cmbPartTRI.Enabled = false;
    }
    protected void chkNewPL_CheckedChanged(object sender, EventArgs e)
    {
        handlePLFields(chkNewPL.Checked);
    }
    protected void chkNewModel_CheckedChanged(object sender, EventArgs e)
    {
        handleModelFields(chkNewModel.Checked);
    }

    private void handleModelFields(Boolean flg)
    {
        if( flg == true)
        {
            cmbModelTRI.Visible = false;
            txtModelTRI.Visible = true;
            txtModelTRI.Enabled = true;
        }
        else
        {
                cmbModelTRI.Visible = true;
                txtModelTRI.Visible = false;
                txtModelTRI.Enabled = false;
        }
    }

    private void handlePLFields(Boolean flg)
    {
        if( flg == true)
        {
            cmbPLTRI.Visible = false;
            txtPLTRI.Visible = true;
            txtPLTRI.Enabled = true;
            chkSeg5.Checked = false;
            cmbModelTRI.Enabled = false;
            txtModelTRI.Enabled = false;
            chkNewModel.Enabled = false;
            chkNewModel.Checked = false;
        }
        else
        {
            cmbPLTRI.Visible = true;
            txtPLTRI.Visible = false;
            txtPLTRI.Enabled = false;
            if(chkSeg5.Checked == true)
            {
                cmbModelTRI.Enabled = true;
                cmbModelTRI.Visible = true;
                txtModelTRI.Visible = false;
                txtModelTRI.Enabled = false;
                chkNewModel.Enabled = true;
                chkNewModel.Checked = false;
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "Update begins..";

            if (chkNewPL.Checked == true && chkSeg4.Checked == true)
            {
                if(txtPLTRI.Text == "")
                {
                    lblError.Text = "Error!! Product Line Name Can't Be Left Empty, Please Enter Product Line value Or Select From Available list of Product Line";
                    //Message.Show(this, lblError.Text);
                    return;
                }
            }

            if (chkNewModel.Checked == true && chkSeg5.Checked == true)
            {
                if(txtModelTRI.Text == "")
                {
                    lblError.Text = "Error!! Model Name Can't Be Left Empty, Please Enter Model Name Or Select From Available list of Models";
                    //Message.Show(this, lblError.Text);
                    return;
                }
            }

            if (txtSimpleCode.Text == "" || cmbGroup.SelectedItem.Text == "")
            {
                lblError.Text = "One or more fields left blank!!! Either Simple Code or Group is left blank. Make sure these are not blank.";
                //Message.Show(this, lblError.Text);
                return;
            }

            if (cmbCatTRI.SelectedItem.Text == "")
            {
                lblError.Text = "One or more fields left blank!!!  Error! Select Category from segments to proceed.";
                //Message.Show(this, lblError.Text);
                return;
            }


            if (!Util.isValidDecimalNumber(txtavgCost.Text))
            {
                lblError.Text = "Invalid Value! Field Average Cost, Please supply a valid numeric value.";
                //Message.Show(this, lblError.Text);
                return;
            }

            if (Convert.ToDouble(txtavgCost.Text)<= 0)
            {
                lblError.Text = "Invalid Value! Field Average Cost, Please supply a valid numeric value greater than Zero";
                //Message.Show(this, lblError.Text);
                return;
            }


            if (!Util.isValidDecimalNumber(txtgrvCost.Text))
            {
                lblError.Text = "Invalid Value! Field GRV Cost , Please supply a valid numeric value.";
                //Message.Show(this, lblError.Text);
                return;
            }

            if (Convert.ToDouble(txtgrvCost.Text) <= 0)
            {
                lblError.Text = "Invalid Value! Field GRV Cost, Please supply a valid numeric value greater than Zero";
                //Message.Show(this, lblError.Text);
                return;
            }

            lblUniqueDesc.Text = txtSimpleCode.Text;

                //---verify connections---------------------------

            SqlCommand rsAddnewCmd=new SqlCommand();

            constrEVO = myGlobal.getConnectionStringForDB("EVO");
            constrOB1 = myGlobal.getConnectionStringForDB("OB1");
           
            Boolean conFlagEvo, conFlagOB1;
            conFlagEvo = false;
            conFlagOB1 = false;
            try
            {
                conEVO = new SqlConnection();
                conEVO.ConnectionString = constrEVO;
                conEVO.Open();
                conFlagEvo = true;
            }
            catch(Exception ex)
            {
                conFlagEvo = false;
            }

            try
            {
                conOB1 = new SqlConnection();
                conOB1.ConnectionString = constrOB1;
                conOB1.Open();
                conFlagOB1 = true;
            }
            catch(Exception ex)
            {
                conFlagOB1 = false;
            }

            if (conFlagEvo == false || conFlagOB1 == false)
            {
                if (conEVO.State == ConnectionState.Open)
                    conEVO.Close();

                if (conOB1.State == ConnectionState.Open)
                    conOB1.Close();
                
                lblError.Text = "Severs connection failed...Error! One of the server connection failed, So Part no. not be updated in any database , Retry little later";
                //Message.Show(this, lblError.Text);
                return;
            }

            //------------------------------

            lblSuccess.Text = "Please wait while AddOnce Stocks is creating your stock Item...";

            if(chkSeg1.Checked == true && cmbCatTRI.Text != "")
                lblUniqueDesc.Text = lblUniqueDesc.Text + "/" + cmbCatTRI.SelectedItem.Text;
            
            if(chkSeg2.Checked = true && cmbManTRI.Text != "")
                lblUniqueDesc.Text = lblUniqueDesc.Text + "/" + cmbManTRI.SelectedItem.Text;
            
            if(chkSeg3.Checked = true && cmbBUTRI.Text != "")
                lblUniqueDesc.Text = lblUniqueDesc.Text + "/" + cmbBUTRI.SelectedItem.Text;
            
            if(chkNewPL.Checked == false) 
            {
                if(chkSeg4.Checked == true && cmbPLTRI.Text != "")
                    lblUniqueDesc.Text = lblUniqueDesc.Text + "/" + cmbPLTRI.SelectedItem.Text;
            }
            
            if(chkNewPL.Checked == true)
            {
                if(chkSeg4.Checked == true && txtPLTRI.Text != "")
                    lblUniqueDesc.Text = lblUniqueDesc.Text + "/" + txtPLTRI.Text.ToUpper();
                
            }

            if(chkNewModel.Checked == false)
            {
                if(chkSeg5.Checked == true && cmbModelTRI.Text !="")
                    lblUniqueDesc.Text = lblUniqueDesc.Text + "/" + cmbModelTRI.SelectedItem.Text;
            }
           
            if(chkNewModel.Checked== true)
            {
                if(chkSeg5.Checked == true && txtModelTRI.Text !="")
                    lblUniqueDesc.Text = lblUniqueDesc.Text + "/" + txtModelTRI.Text.ToUpper();
            }

            if(chkSeg6.Checked == true && cmbPartTRI.Text !="")
                lblUniqueDesc.Text = lblUniqueDesc.Text + "/" + cmbPartTRI.SelectedItem.Text;
            


            //check for the existence of stkitem in the database

            partInTZ = partInTRI = partInKE = partInEPZ = partInUG = 0;

            if(getItemExistenceOnUpdate())
                return;

            rsAddnewCmd.CommandType = CommandType.StoredProcedure;

            if(chkSeg1.Checked== true && cmbCatTRI.Text != "")
                rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue1", SqlDbType.VarChar, 50)).Value = cmbCatTRI.SelectedItem.Text;
            else
                rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue1", SqlDbType.VarChar, 50)).Value = "";
            

            if(chkSeg2.Checked== true && cmbManTRI.Text != "")
                rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue2", SqlDbType.VarChar, 50)).Value = cmbManTRI.SelectedItem.Text;
            else
                rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue2", SqlDbType.VarChar, 50)).Value = "";

            if(chkSeg3.Checked == true && cmbBUTRI.Text !="")
                rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue3", SqlDbType.VarChar, 50)).Value = cmbBUTRI.SelectedItem.Text;
            else
                rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue3", SqlDbType.VarChar, 50)).Value = "";

            if(chkSeg4.Checked == true && cmbPLTRI.Text !="")
            {
                if(chkNewPL.Checked == false)
                    rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue4", SqlDbType.VarChar, 50)).Value = cmbPLTRI.SelectedItem.Text;
                else
                    rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue4", SqlDbType.VarChar, 50)).Value = txtPLTRI.Text.ToUpper();
            }
            else
                rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue4", SqlDbType.VarChar, 50)).Value = "";

            if(chkSeg5.Checked == true && cmbModelTRI.Text !="")
            {
                if(chkNewModel.Checked == false)
                    rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue5", SqlDbType.VarChar, 50)).Value = cmbModelTRI.SelectedItem.Text;
                else
                    rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue5", SqlDbType.VarChar, 50)).Value = txtModelTRI.Text.ToUpper();
            }
            else
                rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue5", SqlDbType.VarChar, 50)).Value = "";
            

            if(chkSeg6.Checked == true && cmbPartTRI.Text != "")
                rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue6", SqlDbType.VarChar, 50)).Value = cmbPartTRI.SelectedItem.Text;
            else
                rsAddnewCmd.Parameters.Add(new SqlParameter("@cvalue6", SqlDbType.VarChar, 50)).Value = "";



            rsAddnewCmd.Parameters.Add(new SqlParameter("@OldCode", SqlDbType.VarChar, 255)).Value = lblCode.Text;

            rsAddnewCmd.Parameters.Add(new SqlParameter("@cSimpleCode", SqlDbType.VarChar, 50)).Value = txtSimpleCode.Text;

            rsAddnewCmd.Parameters.Add(new SqlParameter("@code", SqlDbType.VarChar, 255)).Value = lblUniqueDesc.Text;

            rsAddnewCmd.Parameters.Add(new SqlParameter("@cExtDescription", SqlDbType.VarChar, 255)).Value = lblUniqueDesc.Text;

            rsAddnewCmd.Parameters.Add(new SqlParameter("@ItemGroup", SqlDbType.VarChar, 50)).Value = cmbGroup.SelectedItem.Text;

            rsAddnewCmd.Parameters.Add(new SqlParameter("@Description_1", SqlDbType.VarChar, 50)).Value = txtDescription1.Text;

            rsAddnewCmd.Parameters.Add(new SqlParameter("@Description_2", SqlDbType.VarChar, 50)).Value = txtDescription2.Text;

            rsAddnewCmd.Parameters.Add(new SqlParameter("@Description_3", SqlDbType.VarChar, 50)).Value = txtDescription3.Text;

            rsAddnewCmd.Parameters.Add(new SqlParameter("@AveUCst", SqlDbType.Float, 50)).Value = Convert.ToDouble(txtavgCost.Text);

            rsAddnewCmd.Parameters.Add(new SqlParameter("@LatUCst", SqlDbType.Float, 50)).Value = Convert.ToDouble(txtgrvCost.Text);

            rsAddnewCmd.Parameters.Add(new SqlParameter("@LowUCst", SqlDbType.Float, 50)).Value = Convert.ToDouble(txtavgCost.Text);

            rsAddnewCmd.Parameters.Add(new SqlParameter("@HigUCst", SqlDbType.Float, 50)).Value = Convert.ToDouble(txtavgCost.Text);

            rsAddnewCmd.Parameters.Add(new SqlParameter("@StdUCst", SqlDbType.Float, 50)).Value = Convert.ToDouble(txtavgCost.Text);

            rsAddnewCmd.Parameters.Add(new SqlParameter("@ItemActive", SqlDbType.Bit, 50)).Value = Convert.ToBoolean(cmbIsActive.SelectedItem.Text);

            rsAddnewCmd.Parameters.Add(new SqlParameter("@fItemLastGRVCost", SqlDbType.Float)).Value = Convert.ToDouble(txtgrvCost.Text);

            rsAddnewCmd.Parameters.Add(new SqlParameter("@ulIIdashboardCategory", SqlDbType.VarChar, 100)).Value = cmbDashCategory.SelectedItem.Text;

            if(chkSeg1.Checked == true && cmbCatTRI.Text!="")
                rsAddnewCmd.Parameters.Add("@iInvSegValue1ID", SqlDbType.Int).Value = cmbCatTRI.SelectedValue;
            else
                rsAddnewCmd.Parameters.Add("@iInvSegValue1ID", SqlDbType.Int).Value = 0;

            if(chkSeg2.Checked == true && cmbManTRI.Text!="")
                rsAddnewCmd.Parameters.Add("@iInvSegValue2ID", SqlDbType.Int).Value = cmbManTRI.SelectedValue;
            else
                rsAddnewCmd.Parameters.Add("@iInvSegValue2ID", SqlDbType.Int).Value = 0;

            if(chkSeg3.Checked == true && cmbBUTRI.Text!="")
                rsAddnewCmd.Parameters.Add("@iInvSegValue3ID", SqlDbType.Int).Value = cmbBUTRI.SelectedValue;
            else
                rsAddnewCmd.Parameters.Add("@iInvSegValue3ID", SqlDbType.Int).Value = 0;

            if(chkNewPL.Checked == false)
            {
                if(chkSeg4.Checked == true && cmbPLTRI.Text!="")
                    rsAddnewCmd.Parameters.Add("@iInvSegValue4ID", SqlDbType.Int).Value = cmbPLTRI.SelectedValue;
                else
                    rsAddnewCmd.Parameters.Add("@iInvSegValue4ID", SqlDbType.Int).Value = 0;
            }

            if(chkNewPL.Checked == true)
            {
                if(chkSeg4.Checked == true && txtPLTRI.Text !="")
                    rsAddnewCmd.Parameters.Add("@iInvSegValue4ID", SqlDbType.Int).Value = 0;
            }

            if(chkNewModel.Checked == false)
            {
                if(chkSeg5.Checked == true && cmbModelTRI.Text!="")
                    rsAddnewCmd.Parameters.Add("@iInvSegValue5ID", SqlDbType.Int).Value = cmbModelTRI.SelectedValue;
                else
                    rsAddnewCmd.Parameters.Add("@iInvSegValue5ID", SqlDbType.Int).Value = 0;
            }

            if(chkNewModel.Checked == true)
            {
                if(chkSeg5.Checked == true && txtModelTRI.Text !="")
                    rsAddnewCmd.Parameters.Add("@iInvSegValue5ID", SqlDbType.Int).Value = 0;
            }

            if(chkSeg6.Checked == true && cmbPartTRI.Text!="")
                rsAddnewCmd.Parameters.Add("@iInvSegValue6ID", SqlDbType.Int).Value = cmbPartTRI.SelectedValue;
            else
                rsAddnewCmd.Parameters.Add("@iInvSegValue6ID", SqlDbType.Int).Value = 0;

            rsAddnewCmd.Parameters.Add(new SqlParameter("@createdBy", SqlDbType.VarChar, 100)).Value = lblVersionNo.Text;

            if(CheckStatusKE.Checked)
                rsAddnewCmd.Parameters.Add("@createInKE", SqlDbType.Int).Value = 1;
            else
                rsAddnewCmd.Parameters.Add("@createInKE", SqlDbType.Int).Value = 0;

            if(CheckStatusDU.Checked)
                rsAddnewCmd.Parameters.Add("@createInDU", SqlDbType.Int).Value = 1;
            else
                rsAddnewCmd.Parameters.Add("@createInDU", SqlDbType.Int).Value = 0;

            if(CheckStatusEPZ.Checked)
                rsAddnewCmd.Parameters.Add("@createInEPZ", SqlDbType.Int).Value = 1;
            else
                rsAddnewCmd.Parameters.Add("@createInEPZ", SqlDbType.Int).Value = 0;

            if(CheckStatusTZ.Checked)
                rsAddnewCmd.Parameters.Add("@createInTZ", SqlDbType.Int).Value = 1;
            else
                rsAddnewCmd.Parameters.Add("@createInTZ", SqlDbType.Int).Value = 0;

            if(CheckStatusUG.Checked)
                rsAddnewCmd.Parameters.Add("@createInUG", SqlDbType.Int).Value = 1;
            else
                rsAddnewCmd.Parameters.Add("@createInUG", SqlDbType.Int).Value = 0;

            rsAddnewCmd.Parameters.Add(new SqlParameter("@resultKE", SqlDbType.Int)).Direction = ParameterDirection.Output;
            rsAddnewCmd.Parameters.Add(new SqlParameter("@resultDU", SqlDbType.Int)).Direction = ParameterDirection.Output;
            rsAddnewCmd.Parameters.Add(new SqlParameter("@resultEPZ", SqlDbType.Int)).Direction =ParameterDirection.Output;
            rsAddnewCmd.Parameters.Add(new SqlParameter("@resultTZ", SqlDbType.Int)).Direction = ParameterDirection.Output;
            rsAddnewCmd.Parameters.Add(new SqlParameter("@resultUG", SqlDbType.Int)).Direction = ParameterDirection.Output;

            int resKE,resDU,resEPZ,resTZ,resUG;
            
            resKE = 0;
            resDU = 0;
            resEPZ = 0;
            resTZ = 0;
            resUG = 0;


            ////'''''''''''''''''''''''''''''''''''''call again''OB1'''''''''

            rsAddnewCmd.Connection = conOB1;
            rsAddnewCmd.CommandTimeout = 600;
            rsAddnewCmd.CommandText = "tej.[dbo].[EditStkItemMain-EPZ-KE-UG]";
            rsAddnewCmd.ExecuteNonQuery();

            resKE = Convert.ToInt32(rsAddnewCmd.Parameters["@resultKE"].Value);
            resEPZ = Convert.ToInt32(rsAddnewCmd.Parameters["@resultEPZ"].Value);
            resUG = Convert.ToInt32(rsAddnewCmd.Parameters["@resultUG"].Value);

            if (conOB1.State == ConnectionState.Open)
                conOB1.Close();
            

            ////'''''''''''''''''''''''''''''''''''''call again''''''EVO'''''
            if( resKE == 1 && resEPZ == 1 && resUG == 1)  //If ke,Epz got created then only go for TZ, DU
            {
                rsAddnewCmd.Connection = conEVO;
                rsAddnewCmd.CommandTimeout = 600;
                rsAddnewCmd.CommandText = "tej.[dbo].[EditStkItemMain-TRI-TZ]";
                rsAddnewCmd.ExecuteNonQuery();


                resTZ = Convert.ToInt32(rsAddnewCmd.Parameters["@resultTZ"].Value);
                resDU = Convert.ToInt32(rsAddnewCmd.Parameters["@resultDU"].Value);

                if (conEVO.State == ConnectionState.Open)
                    conEVO.Close();

            }


            //////'''''''''''''''''''''''''''''''''''''call again'''''''''''

            if( resTZ == 1 && resDU == 1 && resKE == 1 && resEPZ == 1 && resUG == 1)
            {
                lblSuccess.Text = "Stock Item: " + "'" + lblUniqueDesc.Text + "'" + " Updated successfully for all the warehouses in selected countries/databases ";
                lblSuccess.ForeColor = Color.Green;
                //Message.Show(Me, lblSuccess.Text)
                ResetAll();
            }
            else
            {
                lblSuccess.Text = "Stock Item: " + "'" + lblUniqueDesc.Text + "'" + " could not be Updated due to error occured in one of the countries/databases. Retry or Please consult database administrator.";
                lblSuccess.ForeColor = Color.Red;
                //Message.Show(Me, lblSuccess.Text)
            }
        }
        catch(Exception ex)
        {
                 lblError.Text = "Error : " + ex.Message;
        }
    }
   
}