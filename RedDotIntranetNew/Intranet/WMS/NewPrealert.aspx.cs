using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class Intranet_WMS_NewPrealert : System.Web.UI.Page
{
    WMSclsPrealert prealert = new WMSclsPrealert();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt1 = new DataTable();
            dt1 = prealert.newprealertID();
            string newid = "";
            foreach (DataRow row in dt1.Rows)
            {
                tbPrealertID.Text = row["newid"].ToString();
                newid = row["newid"].ToString();
            }

            TbCreateDate.Text = DateTime.Now.ToString("d");
            TbStatus.Text = "DRAFT";

            //DataTable dt2 = new DataTable();
            //dt2 = prealert.SupplierList();
            //DPLsupplier.DataSource = dt2;
            //DPLsupplier.DataTextField = "supplier_name";
            //DPLsupplier.DataValueField = "supplier_id";
            //DPLsupplier.DataBind();


            //DataTable dt3 = new DataTable();
            //dt3 = prealert.FowarderList();

            //DPLForwarder.DataSource = dt3;
            //DPLForwarder.DataTextField = "freight_forwarder_name";
            //DPLForwarder.DataValueField = "freight_forwarder_id";
            //DPLForwarder.DataBind();


            GvPrealertDetail.Visible = false;

            bindgrid(newid);


        }
    }


    private void ShowNoResultFound( DataTable source, GridView gv)
    {

        DataColumnCollection dcc = null;
        dcc = source.Columns;

        // Cycle through colums resetting AllowDBNull
        DataColumn dc = null;
        foreach (DataColumn dc_loopVariable in dcc)
        {            dc = dc_loopVariable;
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

    protected void bindgrid(String  newid)
    {

        DataTable dt1 = prealert.prealertgrid(newid);
        if (dt1.Rows.Count > 0)
        {
            int qty = 0;

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




        protected void btexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("List_Prealert.aspx");
    }


        protected void btsave_Click(object sender, EventArgs e)
        {

            if (TbToal.Text != "" && TbSupReference.Text != "" && tbShipRef.Text != "" && Tbeta.Text != "")
            {
                GvPrealertDetail.Visible = true;


                string prealertid = tbPrealertID.Text.ToString();
                String Status = "DRAFT";
                String creatinDate = TbCreateDate.Text;
                string totQty = TbToal.Text.ToString();
                string supplier = DPLsupplier.SelectedValue.ToString();
                String SupReference = TbSupReference.Text;
                String boeid = "";

                int shipMod = 0;

                if (rbsea.Checked == true)
                    shipMod = 1;

                else if (rbair.Checked == true)
                    shipMod = 2;

                else if (rbland.Checked == true)
                    shipMod = 3;



                String ShipRef = tbShipRef.Text;

                String eta = FormatDate(Tbeta.Text);


                int delvMod = 0;
                if (rbddu.Checked == true)
                    delvMod = 1;

                else if (rbfob.Checked == true)
                    delvMod = 2;

                else if (rbexword.Checked == true)
                    delvMod = 3;

                else if (rbcnf.Checked == true)
                    delvMod = 4;

                String Fowarder = DPLForwarder.SelectedValue.ToString();

                String Comment = tbcomment.Text;

                WMSclsPrealert prealert = new WMSclsPrealert();
                String newprealertid = prealert.insertPrealert(prealertid, Status, boeid, creatinDate, shipMod, ShipRef, delvMod, eta, totQty, supplier, SupReference, Fowarder, Comment);
                tbPrealertID.Text = newprealertid;
                bindgrid(newprealertid);
                lbmsg.Visible = true;


            }
            else
            {
                lbmsg.Text = "Please make sure to fill all required fields";
                lbmsg.Visible = true;
            }
            
        
        
        
        }

        private String FormatDate(String _Date)
        {
            DateTime Dt = DateTime.Now;
            IFormatProvider mFomatter = new System.Globalization.CultureInfo("en-US");
            Dt = DateTime.Parse(_Date, mFomatter);
            return Dt.ToString("yyyy-MM-dd");
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
        protected void Btnew_Click(object sender, EventArgs e)
        {

            Response.Redirect("Addproduct.aspx");
        }
        protected void GvPrealertDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument) % GvPrealertDetail.PageSize;
            var indexrow = e.CommandArgument;
            if (String.IsNullOrEmpty(indexrow.ToString()) == false)
            {

                if (e.CommandName == "Deleting")
                {
                    String PrealertDetailID = ((HiddenField)GvPrealertDetail.Rows[index].FindControl("hdnPrealertDetailID")).Value;
                    prealert.DeletePrealertDEtails(PrealertDetailID);
                    bindgrid(tbPrealertID.Text.ToString());
                }
            }
        }
}