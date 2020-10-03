<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="FunnelList.aspx.cs" Inherits="IntranetNew_Funnel_FunnelList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.3.min.js"></script>
 
 <script type="text/javascript">

     $(document).ready(function () {
         $(function () {
             $('[id*=ddlCountry]').multiselect({
                 includeSelectAllOption: true,
                 buttonWidth: '230px'
             });
         });

         $(function () {
             $('[id*=ddlBU]').multiselect({
                 includeSelectAllOption: true,
                 nonSelectedText: 'Select BU',
                 enableFiltering: true,
                 buttonWidth: '222px'

             });
         });

         $(function () {
             $('[id*=ddlDealStatus]').multiselect({
                 includeSelectAllOption: true,
                 nonSelectedText: 'Select Status',
                 buttonWidth: '222px'
             });
         });

         $(function () {

             $('[id*=ddlcustomer]').multiselect({
                 includeSelectAllOption: true,
                 nonSelectedText: 'Select Customer',
                 maxHeight: 350,

                 dropDown: true,
                 enableFiltering: true,
                 buttonWidth: '230px'

             });
         });

         $(function () {
             $('[id*=ddlQuoteMonth]').multiselect({
                 includeSelectAllOption: true,
                 buttonWidth: '112px'
             });
         });
         $(function () {
             $('[id*=ddlQuoteYear]').multiselect({
                 includeSelectAllOption: true,
                 buttonWidth: '112px'
             });
         });

         $(function () {
             $('[id*=ddlCloseMonth]').multiselect({
                 includeSelectAllOption: true,
                 buttonWidth: '110px'
             });
         });
         $(function () {
             $('[id*=ddlCloseYear]').multiselect({
                 includeSelectAllOption: true,
                 buttonWidth: '110px'
             });
         });

     });
</script>



<style type="text/css">
    td.column_style
    {
        border-left: 1px solid black;
        border-top: 1px solid black;
        padding-left: 4px;
         padding-right: 4px;
    }    
    
     th.column_style
    {
        border-left: 1px solid black;
        border-top: 1px solid black;
    }
      
</style>

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Funnel List </Lable>
    </td>
</tr>

<tr>
        <td width="50%">
           &nbsp;
        </td>
        <td width="50%">
        &nbsp;
        </td>
</tr>

<tr>
        <td width="100%" align="center">
        <asp:Label ID="lblMsg" runat="server" 
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />  &nbsp;&nbsp; 
        </td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlsetup" runat="server" Width="100%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="95%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>
        <td width="10%">
           &nbsp;
        </td>
        <td width="23%">
        &nbsp;
        </td>
         <td width="10%">
           &nbsp;
        </td>
        <td width="23%">
        &nbsp;
        </td>
         <td width="12%">
           &nbsp;
        </td>
        <td width="22%">
        &nbsp;
        </td>
    </tr>
    
     <tr>
        <td width="10%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Country  &nbsp; </label>  
        </td>
        <td width="23%">
            <asp:ListBox ID="ddlCountry" runat="server" SelectionMode="Multiple" 
                class="input-control" style="height:25px" Width="150px"  >
            </asp:ListBox>
        </td>
         <td width="10%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">BU  &nbsp; </label>  
        </td>
        <td width="23%">
             <asp:ListBox ID="ddlBU" runat="server" SelectionMode="Multiple" Width="150px"  >
            </asp:ListBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Status  &nbsp; </label>  
        </td>
        <td width="22%">
              <asp:ListBox ID="ddlDealStatus" runat="server" SelectionMode="Multiple" Width="100px"  >
              </asp:ListBox>
        </td>
    </tr>

    <tr>
        <td width="10%">
          <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Quote M/Y  &nbsp; </label>  
        </td>
        <td width="24%">
            <asp:ListBox ID="ddlQuoteMonth" runat="server" SelectionMode="Multiple"   >
                <asp:ListItem>JAN</asp:ListItem>
                <asp:ListItem>FEB</asp:ListItem>
                <asp:ListItem>MAR</asp:ListItem>
                <asp:ListItem>APR</asp:ListItem>
                <asp:ListItem>MAY</asp:ListItem>
                <asp:ListItem>JUN</asp:ListItem>
                <asp:ListItem>JUL</asp:ListItem>
                <asp:ListItem>AUG</asp:ListItem>
                <asp:ListItem>SEP</asp:ListItem>
                <asp:ListItem>OCT</asp:ListItem>
                <asp:ListItem>NOV</asp:ListItem>
                <asp:ListItem>DEC</asp:ListItem>
            </asp:ListBox>
             <asp:ListBox ID="ddlQuoteYear" runat="server" SelectionMode="Multiple"  >
            </asp:ListBox>
        </td>
        <td width="10%">
        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Closing M/Y  &nbsp; </label> 
        </td>
        <td width="23%">
                 <asp:ListBox ID="ddlCloseMonth" runat="server" SelectionMode="Multiple"   >
                     <asp:ListItem>JAN</asp:ListItem>
                    <asp:ListItem>FEB</asp:ListItem>
                    <asp:ListItem>MAR</asp:ListItem>
                    <asp:ListItem>APR</asp:ListItem>
                    <asp:ListItem>MAY</asp:ListItem>
                    <asp:ListItem>JUN</asp:ListItem>
                    <asp:ListItem>JUL</asp:ListItem>
                    <asp:ListItem>AUG</asp:ListItem>
                    <asp:ListItem>SEP</asp:ListItem>
                    <asp:ListItem>OCT</asp:ListItem>
                    <asp:ListItem>NOV</asp:ListItem>
                    <asp:ListItem>DEC</asp:ListItem>
                </asp:ListBox>
                 <asp:ListBox ID="ddlCloseYear" runat="server" SelectionMode="Multiple"  >
                </asp:ListBox>
        </td>
         <td width="12%">
            <%-- <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Customer  &nbsp; </label>  --%>
        </td>
        <td width="22%">
               <%--<asp:ListBox ID="ddlcustomer" runat="server" SelectionMode="Multiple" Enabled="false" class="input-control" style="height:25px" Width="150px" >
                </asp:ListBox>--%>
        </td>
     
    </tr>
    <tr>
    <td width="100%" colspan="6"> &nbsp;</td>
    </tr>
     <tr>
        <td width="100%" colspan="6" >

         &nbsp;&nbsp;  &nbsp; 
        
        <asp:Button ID="btnNewDeal" Text="New Deal" runat="server" class="btn btn-primary" 
                Font-Bold="true" Font-Size="Medium" 
                style="font-family: Raleway;height:38px;width:150px;" 
                onclick="btnNewDeal_Click" />

          &nbsp;&nbsp;  &nbsp;&nbsp;   
        
        <asp:Button ID="BtnSearch" Text="Search" runat="server" class="btn btn-info" 
                Font-Bold="true" Font-Size="Medium" 
                style="font-family: Raleway;height:38px;width:150px;" 
                onclick="BtnSearch_Click" />

         &nbsp;&nbsp;  &nbsp;&nbsp;   
        
        <asp:Button ID="BtnExportToExcel" Text="ExportToExcel" runat="server" Enabled="false"
                class="btn btn-success" Font-Bold="true" Font-Size="Medium"  
                style="font-family: Raleway;height:38px;width:150px;" 
                onclick="BtnExportToExcel_Click" />

       <%--  &nbsp;&nbsp;  &nbsp;&nbsp;   
          <asp:ImageButton ID="BtnAddNew" runat="server" AlternateText="Search" 
                ImageUrl="~/images/ADD_NEW.png" style="height:40px;width:150px" onclick="BtnAddNew_Click" 
                />

          &nbsp;&nbsp;  &nbsp;&nbsp;    
          <asp:ImageButton ID="BtnSearch1" runat="server" AlternateText="Search" 
                ImageUrl="~/images/RDD Search button.png" style="height:40px;width:150px" onclick="BtnSearch1_Click" 
                />
        &nbsp;&nbsp;  &nbsp;&nbsp;  
         <asp:ImageButton ID="BtnExportToExcel1" runat="server" AlternateText="ExportToExcel" Visible="false"
                ImageUrl="~/images/Excel_download.png" style="height:36px;width:150px" onclick="BtnExportToExcel1_Click" 
                />--%>
      
        </td>
    
    </tr>
    <tr>
    <td width="100%" colspan="6" > &nbsp;</td>
    </tr>
  </table>

</asp:Panel>

  </td>
</tr>
   
<tr>
    <td width="100%" > &nbsp; </ td>
</tr>
   
<tr>
    <td width="100%">

    <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" 
                    Width="100%" CellPadding="4" BackColor="White" BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="1px" 
            onselectedindexchanged="Grid1_SelectedIndexChanged"   >
                        <Columns>
                            
                            <asp:CommandField ButtonType="Link" ControlStyle-CssClass="column_style" 
                                ControlStyle-ForeColor="Blue" ItemStyle-Width="3%" SelectText="Edit" 
                                ShowSelectButton="True">
                            <ControlStyle ForeColor="Blue" />
                            <ItemStyle CssClass="column_style" Width="3%" />
                            </asp:CommandField>

                            <asp:TemplateField HeaderText="ID" ItemStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:Label ID="lblfid" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("fid")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="BDM" ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBDM" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("bdm")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="QuoteID" ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblquoteID" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("quoteID")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="End User" 
                                ItemStyle-Width="12%">
                                <ItemTemplate>
                                    <asp:Label ID="lblendUser" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("endUser")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Reseller" 
                                ItemStyle-Width="13%">
                                <ItemTemplate>
                                    <asp:Label ID="lblresellerName" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("resellerName")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Country" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblcountry" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("country")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small"  HeaderText="BU" ItemStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBU" runat="server" Font-Size="Smaller" Text='<%#Eval("BU")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="GoodsDesc" 
                                ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblgoodsDesc" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("goodsDescr")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Quote Dt" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblquoteDt" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("quoteDate")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Close Dt" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCloseDt" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("CloseDt")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="DealStatus" ItemStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealStatus" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("DealStatus")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Cost" ItemStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCost" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("Cost","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Landed" ItemStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblLanded" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("Landed","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Value" ItemStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblvalue" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("value","{0:N2}")%>'></asp:Label> 
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Access" ItemStyle-Width="3%" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccess" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("Access")%>'></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                     <%--   "{0:N2}"
                            "{0:C}"
                     --%>
                        <FooterStyle BackColor="#f91d1d" ForeColor="#4e465b" />
                        <HeaderStyle BackColor="#f91d1d" Font-Bold="True" ForeColor="#FFFFCC" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#4e465b" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#4e465b" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                      <%--  #330099--%>
                    </asp:GridView>

    </td>
</tr>


</table>

 
</asp:Content>

