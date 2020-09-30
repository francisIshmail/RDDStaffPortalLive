<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="salespersontargets_quarterly.aspx.cs" Inherits="IntranetNew_Targets_salespersontargets_quarterly" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="Javascript" type="text/javascript">
    function isNumberKey(evt) {
        debugger;
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 46) {
            return true;
        } else {
            alert("Enter only numbers");
            return false;
        }
    }

    function getConfirmation() {
        return confirm(' Email notification will be sent to management. Are you sure you want to save SalesPerson Targets ?');
    }

    </script>
<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" colspan="2" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway; " >Sales Person Targets(Quarterly)  </Lable>
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
        <td width="80%" align="center" colspan="2" >
        <asp:Label ID="lblMsg" runat="server" 
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; margin-left:10% " />
        </td>
</tr>

<tr>
<td width="80%" align="center" colspan="2">

<asp:Panel ID="pnlForms" runat="server" Width="70%"   BorderWidth="1px" BorderColor="Red" EnableTheming="true">
<fieldset style="border-width:1px; border-color:Red">
<table width="100%" align="center" cellpadding="3px" cellspacing="3px" >
      <tr>
        <td width="15%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Year  &nbsp; </label>  
        </td>
        <td width="15%">
                  <asp:DropDownList ID="ddlyear" runat="server" 
                      style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  
                      AutoPostBack="True" onselectedindexchanged="ddlyear_SelectedIndexChanged" > </asp:DropDownList>
       
        
        </td>
         <td width="15%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Quarter  &nbsp; </label>  
        </td>
        <td width="15%">
             
             <asp:DropDownList ID="ddlQuarter" runat="server" 
                 style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  
                 AutoPostBack="True" 
                 onselectedindexchanged="ddlQuarter_SelectedIndexChanged"  > </asp:DropDownList>
       
        </td>
        <td width="15%" align="left">
        </td>
        <td width="25%">
             <asp:Button ID="btnsave1" runat="server" Text="Save" 
              style="width:70%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="btnsave1_Click"  OnClientClick="return getConfirmation();"
                 /> 
        </td>
    </tr>

    <tr>
    <td width="15%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Sales Person  &nbsp; </label>  
        </td>
        <td colspan="3">
             
             <asp:DropDownList ID="ddlsalesperson" runat="server" 
                 style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  
                 AutoPostBack="True" 
                 onselectedindexchanged="ddlsalesperson_SelectedIndexChanged" > </asp:DropDownList>
       
        </td>
    </tr>

     <tr >
      <td width="15%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Country  &nbsp; </label>  
        </td>
     <td colspan="5" >
                               <asp:RadioButtonList ID="rbListCountries" runat="server" 
                                   RepeatDirection="Horizontal"  RepeatColumns="5" AutoPostBack="true" Font-Size="small" 
                                   onselectedindexchanged="rbListCountries_SelectedIndexChanged" ></asp:RadioButtonList>
                              </td>
     </tr>

</table>
</fieldset>
</asp:Panel>

</td>
</tr>

<tr>
<td> &nbsp;</td>
</tr>

<tr>
    <td width="80%" align="center" colspan="2">
    
    <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" 
                ForeColor="#333333"  Width="70%" BorderWidth="1px" BorderColor="Red"    >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="20%" HeaderText="BU">

                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Font-Size="Small" Height="25px"
                                  Text='<%#Eval("BU")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemleftID" />
                            <HeaderStyle CssClass="gvHeaderCenterID" />

                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Revenue Targets">
                            <ItemTemplate>
                                  <asp:TextBox ID="txtrevenuetarget" Text='<%# DataBinder.Eval(Container.DataItem, "revenue_targets")%>' runat="server" MaxLength="10" Width="150px"  Font-Size="Small" Height="25px" onkeypress="javascript:return isNumberKey(event);" ></asp:TextBox>
                            
                                <%--<asp:Label ID="lblMenu" runat="server" Text='<%#Eval("revenue_targets")%>'></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="10%" HeaderText="GP Targets">
                            <ItemTemplate >
                                  <asp:TextBox ID="txtgptargets" Text='<%# DataBinder.Eval(Container.DataItem, "GP_targets")%>' runat="server" MaxLength="10" Width="150px"  Font-Size="Small" Height="25px" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                            
                               <%-- <asp:Label ID="lblFormName" runat="server" Text='<%#Eval("GP_targets")%>' ></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                    </Columns>

                 <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333"   />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
               <AlternatingRowStyle BackColor="#FFFFFF" />
                </asp:GridView>
    
    </td>

</tr>


</table>

    

<table align="left" width="70%">

   <tr>
        <td width="100%" align="center" >
          &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save" 
                style="width:15%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnSave_Click"  OnClientClick="return getConfirmation();"
                 />  &nbsp;&nbsp; 

       &nbsp;&nbsp; 
           <asp:Button ID="BtnCancel" runat="server" Text="Cancel" 
                
                style="width:15%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  
                />
        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>

</table>


<style type="text/css">

.gvItemCenter { text-align: left; }
.gvHeaderCenter {  text-align: left; }
.gvSelectButton { ForeColor: Blue}
.gvHeaderCenterID { text-align: center; }
.gvItemCenterID { text-align: center; font-family:Verdana; font-size:medium }
.gvItemleftID { text-align: left;margin-left:5px;  font-size:small;padding-left:8px; }
</style>



</asp:Content>


