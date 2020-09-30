using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_Prealert : System.Web.UI.Page
{
    WMSclsPrealert prealert = new WMSclsPrealert();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "prealert";

            String boeid = "";
            String prealertID = "";
            if (string.IsNullOrEmpty(Request.QueryString["boeID"]) == false)
                boeid = Request.QueryString["boeID"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["prealertiD"]) == false)
                prealertID = Request.QueryString["prealertiD"].ToString();

            BindField(prealertID, boeid);
            bindgrid(prealertID, boeid);

        }
    }
    protected void btexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("List_Prealert.aspx");
    }

    protected void BindField(String prealertID, String boeid)
    {
     DataTable dt1 = new DataTable();

        if (String.IsNullOrEmpty(prealertID)==false )
        dt1 = prealert.prealert(prealertID);
         else if (String.IsNullOrEmpty(boeid) == false)
            dt1 = prealert.BIllofEntry(boeid);
        if (dt1.Rows.Count > 0)
        {
            tbPrealertID.Text = dt1.Rows[0]["prealert_id"].ToString();
            TbStatus.Text = dt1.Rows[0]["status"].ToString();

            if (dt1.Rows[0]["status"].ToString() == "DRAFT")
            {
                btconfirm.Enabled = true;
                btReceive.Enabled = false;
                bttally.Enabled = true;
                btsave.Enabled = true;
                TbBoe.ReadOnly = true;
            }
            else if (dt1.Rows[0]["status"].ToString() == "RECEIVED")
            {
                btconfirm.Enabled = false;
                btReceive.Enabled = false;
                bttally.Enabled = false;
                btsave.Enabled = false;
                TbBoe.ReadOnly = true;
                GvPrealertDetail.Columns[6].Visible = false;
                GvPrealertDetail.ShowFooter = false;
            }
            else if (dt1.Rows[0]["status"].ToString() == "CONFIRMED")
            {
                TbBoe.ReadOnly = false;
                btconfirm.Enabled = false;
                btReceive.Enabled = true;
                bttally.Enabled = true;
                btsave.Enabled = true;
            }
            TbBoe.Text = dt1.Rows[0]["boe_id"].ToString();
            TbCreateDate.Text = dt1.Rows[0]["creation_date"].ToString();


            if (int.Parse(dt1.Rows[0]["shipping_method"].ToString()) == 1)
                rbsea.Checked = true;

            else if (int.Parse(dt1.Rows[0]["shipping_method"].ToString()) == 2)
                rbair.Checked = true;

            else if (int.Parse(dt1.Rows[0]["shipping_method"].ToString()) == 3)
                rbland.Checked = true;
            if (int.Parse(dt1.Rows[0]["delivery_method"].ToString()) == 1)
                rbddu.Checked = true;

            else if (int.Parse(dt1.Rows[0]["delivery_method"].ToString()) == 2)
                rbfob.Checked = true;

            else if (int.Parse(dt1.Rows[0]["delivery_method"].ToString()) == 3)
                rbexword.Checked = true;

            else if (int.Parse(dt1.Rows[0]["delivery_method"].ToString()) == 4)
                rbcnf.Checked = true;

            tbShipRef.Text = dt1.Rows[0]["shipping_reference"].ToString();

            if (String.IsNullOrEmpty(prealertID) == false)
            {
                Tbeta.Text = dt1.Rows[0]["eta"].ToString();
                tbcomment.Text = dt1.Rows[0]["comments"].ToString();
            }
            else if (String.IsNullOrEmpty(boeid) == false)
            {
                Tbeta.Text = "";
                tbcomment.Text = "";
            }


            TbToal.Text = dt1.Rows[0]["total_quantity"].ToString();
            DPLsupplier.SelectedValue  = dt1.Rows[0]["supplier_id"].ToString();
            TbSupReference.Text = dt1.Rows[0]["supplier_reference"].ToString();
            DPLForwarder.SelectedValue = dt1.Rows[0]["freight_forwarder_id"].ToString();

        }
     

    }

    protected void bindgrid(String prealertID, String boeid)
    {
       
        DataTable dt1 = new DataTable();

        if (String.IsNullOrEmpty(prealertID) == false)
            dt1 = prealert.prealertgrid(prealertID);

        else if (String.IsNullOrEmpty(boeid) == false)
            dt1 = prealert.BIllofEntrygrid(boeid);
      

        int qty = 0;
        if (dt1.Rows.Count > 0)
        {

            foreach (DataRow row in dt1.Rows)
            {
                qty = qty + int.Parse(row["quantity"].ToString());
            }

            tbpart.Text = qty.ToString();

            GvPrealertDetail.DataSource = dt1;
            GvPrealertDetail.DataBind();
        }
        else
            ShowNoResultFound(dt1, GvPrealertDetail);
    }
    protected void Btnew_Click(object sender, EventArgs e)
    {
        
        
        Response.Redirect("stockItem.aspx?from=0");
    }
    protected void btconfirm_Click(object sender, EventArgs e)
    {
         
          prealert.ConFirmedPrealertStatus(int.Parse(tbPrealertID.Text.ToString()));
          String boeid = "";
          String prealertID = "";
          if (string.IsNullOrEmpty(Request.QueryString["boeID"]) == false)
              boeid = Request.QueryString["boeID"].ToString();
          if (string.IsNullOrEmpty(Request.QueryString["prealertiD"]) == false)
              prealertID = Request.QueryString["prealertiD"].ToString();

          BindField(prealertID, boeid);
          bindgrid(prealertID, boeid);
          lbmsg.Visible = true;
          lbmsg.Text = "The Prealert has been confirmed";
    }
    protected void btReceive_Click(object sender, EventArgs e)
    {
        if (TbBoe.Text != "")
        {
            WMSclsPrealert sd1 = new WMSclsPrealert();
            sd1.ReceivePrealertStatus(tbPrealertID.Text.ToString(), TbBoe.Text);
            sd1.insertbillOfEntry(tbPrealertID.Text.ToString());
            sd1.insertbillOfEntryDetails(tbPrealertID.Text.ToString());
          
            //Session["boeid"] = TbBoe.Text;
            Response.Redirect("ReceivedGoods.aspx?boeid=" + TbBoe.Text);
        }
        
    }
    protected void bttally_Click(object sender, EventArgs e)
    {

        Response.Redirect("Crystal_Prealert_Unstuffing.aspx?PrealertId=" + tbPrealertID.Text+"&from=0");
        //Response.Redirect("Crystal_PrealertReport.aspx?PrealertId=" + tbPrealertID.Text);
    }
    
    protected void TbBoe_TextChanged(object sender, EventArgs e)
    {
        if (TbBoe.Text.ToString () != "")
        btReceive.Enabled = true;
    }

    private void ShowNoResultFound(DataTable source, GridView gv)
    {

        DataColumnCollection dcc = null;
        dcc = source.Columns;

        // Cycle through colums resetting AllowDBNull
        DataColumn dc = null;
        foreach (DataColumn dc_loopVariable in dcc)
        {
            dc = dc_loopVariable;
            // Ignore ID column
            if (dc.AllowDBNull == false)
            {
                // Found a field where AllowDBNull is false so change it!
                dc.AllowDBNull = true;
            }

        }

        source.Rows.Add(source.NewRow());
        gv.DataSource = source;
        gv.DataBind();
        int columnsCount = gv.Columns.Count;
        gv.Rows[0].Cells.Clear();
        gv.Rows[0].Cells.Add(new TableCell());
        gv.Rows[0].Cells[0].ColumnSpan = columnsCount;
        gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        gv.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Blue;
        gv.Rows[0].Cells[0].Font.Bold = true;
        gv.Rows[0].Cells[0].Text = "Add New Details";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
          String quantity = ((TextBox)GvPrealertDetail.FooterRow.FindControl("TbQTY")).Text;
          if (String.IsNullOrEmpty(quantity) == true)
          {
              lbmsg.Text = "Please Enter numeric value for Quantity";
              lbmsg.Visible = true;
              
          }
          else
          {
              lbmsg.Visible = false;
              int prealert_id = int.Parse(tbPrealertID.Text.ToString());
              int stock_id = int.Parse(((DropDownList)GvPrealertDetail.FooterRow.FindControl("Dplpart")).SelectedValue);
              int COO = int.Parse(((DropDownList)GvPrealertDetail.FooterRow.FindControl("DllCoo")).SelectedValue);
              String PO_number = ((TextBox)GvPrealertDetail.FooterRow.FindControl("TbPO")).Text;
              int warehouse_id = int.Parse(((DropDownList)GvPrealertDetail.FooterRow.FindControl("DPLWeareHouse")).SelectedValue);
              int location_id = int.Parse(((DropDownList)GvPrealertDetail.FooterRow.FindControl("Dplocation")).SelectedValue);

              WMSclsPrealert prealert = new WMSclsPrealert();
              Boolean insert = prealert.insertPrealertDetails(prealert_id, stock_id, COO, int.Parse(quantity), PO_number, warehouse_id, location_id);

              DataTable dt1 = new DataTable();
              dt1 = prealert.prealertgrid(prealert_id.ToString());
              int qty = 0;
              foreach (DataRow row in dt1.Rows)
              {
                  qty = qty + int.Parse(row["quantity"].ToString());
              }

              tbpart.Text = qty.ToString();
              GvPrealertDetail.DataSource = dt1;
              GvPrealertDetail.DataBind();
          }
    }
    protected void GvPrealertDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       // int index = Convert.ToInt32(e.CommandArgument) % GvPrealertDetail.PageSize;
        var indexrow = e.CommandArgument;
        if (String.IsNullOrEmpty(indexrow.ToString()) == false)
        {
            int index = Convert.ToInt32(indexrow);
            String PrealertDetailID = ((HiddenField)GvPrealertDetail.Rows[index].FindControl("hdnPrealertDetailID")).Value;
            if (e.CommandName == "Deleting")
            {
              
                prealert.DeletePrealertDEtails(PrealertDetailID);
               
            }

            if (e.CommandName == "Editing")
            {
              
               DataTable dt =  prealert.EditPrealert(PrealertDetailID);

               trDetails.Visible = true;

               ddlPartNumber.SelectedValue = dt.Rows[0]["stock_id"].ToString();
               ddlLocation.SelectedValue = dt.Rows[0]["location_id"].ToString();
               ddlwarehouse.SelectedValue = dt.Rows[0]["warehouse_id"].ToString();
               ddlCountry.SelectedValue = dt.Rows[0]["coo"].ToString();
               txtPoNumber.Text = dt.Rows[0]["PO_number"].ToString();
               txtQty.Text = dt.Rows[0]["quantity"].ToString();
                //bindgrid(int.Parse(tbPrealertID.Text.ToString()));
            }
            
        }
    }

   
    protected void btnCancel_Click2(object sender, EventArgs e)
    {
        trDetails.Visible = false;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        String prealert_id = tbPrealertID.Text;
        String stock_id = ddlPartNumber.SelectedValue;
        String COO = ddlCountry.SelectedValue;
        String PO_number = txtPoNumber.Text;
        String warehouse_id = ddlwarehouse.SelectedValue;
        String location_id = ddlLocation.SelectedValue;
    }
}