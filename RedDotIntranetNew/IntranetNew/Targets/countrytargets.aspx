<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="countrytargets.aspx.cs" Inherits="IntranetNew_Targets_CountryTargets" EnableEventValidation="false"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="Javascript" type="text/javascript">
    function isNumberKey(evt) {
        debugger;
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 48 && charCode <= 57) || charCode == 46 ) {
        return true;
    } else {
    alert("Enter only numbers");
        return false;
            }

}


function DecimalValidate(control) {
    // regular expression
    var rgexp = new RegExp("^\d*([.]\d{2})?$");
    var input = document.getElementById(control).value;

    if (input.match(rgexp))
        alert("ok");
    else
        alert("no");
}

    </script>

<table width="100%" align="center"> 
<tr>
    <td width="100%" colspan="2" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway; " > Country Targets  </Lable>
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
        <td width="80%" colspan="2" align="center">
        <asp:Label ID="lblMsg" runat="server" 
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />  &nbsp;&nbsp; 
        </td>
</tr>

<tr>
    <td width="80%" colspan="2" align="center"> 
        
        <asp:Panel ID="pnlForms" runat="server" Width="70%"    BorderWidth="1px" BorderColor="Red" EnableTheming="true">
<fieldset style="border-width:1px; border-color:Red">
<table width="100%" align="center" cellpadding="3px" cellspacing="3px"  >
    <tr>
         <td width="15%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Year  &nbsp; </label>  
        </td>
        <td width="15%">
                  <asp:DropDownList ID="ddlyear" runat="server" 
                      style="width:95%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  
                      AutoPostBack="True" onselectedindexchanged="ddlyear_SelectedIndexChanged" > </asp:DropDownList>
        </td>
        <td width="15%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">For Month  &nbsp; </label>  
        </td>
        <td width="15%">
             
             <asp:DropDownList ID="ddlfrmMonth" runat="server" 
                 style="width:95%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  
                 AutoPostBack="True" onselectedindexchanged="ddlfrmMonth_SelectedIndexChanged" > </asp:DropDownList>
       
        </td>
         <td width="15%" align="left">
      <%--     <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">To Month  &nbsp; </label>  
      --%>  </td>
        <td width="15%">
          <asp:Button ID="btnsave1" runat="server" Text="Save" 
                
                
                
                style="width:90%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="btnsave1_Click"
                 />
        </td>

        <td width="10%">
             
           
        </td>
    </tr>

     <tr >
     <td width="15%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Country  &nbsp; </label>  
        </td>
     <td colspan="6" >
                               <asp:RadioButtonList ID="rbListCountries" runat="server" 
                                   RepeatDirection="Horizontal"  RepeatColumns="6" AutoPostBack="true"  Font-Size="Small"
                                   onselectedindexchanged="rbListCountries_SelectedIndexChanged" ></asp:RadioButtonList>
                              </td>
     </tr>

</table>
</fieldset>

</asp:Panel>

    </td>
</tr>
<tr>
<td>
    &nbsp;
</td>
</tr>

<tr>
<td width="80%" colspan="2" align="center">
<asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false"  
                ForeColor="#333333"  Width="70%" BorderWidth="1px" BorderColor="Red"  >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="40%"  HeaderText="BU" >

                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" 
                                  Text='<%#Eval("BU")%>' Height="25px" ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemleftID"  />
                            <HeaderStyle CssClass="gvHeaderCenterID" />

                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="20%"  HeaderText="Revenue Targets" >
                            <ItemTemplate  >
                                  <asp:TextBox ID="txtrevenuetarget" Font-Size="Small"   Height="25px" Text='<%# DataBinder.Eval(Container.DataItem, "revenue_targets")%>' runat="server" MaxLength="10" Width="150px"   onkeypress="javascript:return isNumberKey(event);"   ></asp:TextBox>
                           
                                 <%--<asp:Label ID="lblMenu" runat="server" Text='<%#Eval("revenue_targets")%>'></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center"/>
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="20%" HeaderText="GP Targets">
                            <ItemTemplate >
                                  <asp:TextBox ID="txtgptargets" Height="25px" Font-Size="Small"  Text='<%# DataBinder.Eval(Container.DataItem, "GP_targets")%>' runat="server" MaxLength="10" Width="150px" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                            
                               <%-- <asp:Label ID="lblFormName" runat="server" Text='<%#Eval("GP_targets")%>' ></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                    </Columns>

                 <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"  HorizontalAlign="Center" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
                  <AlternatingRowStyle BackColor="#FFFFFF" />
                </asp:GridView>

</td>

</tr>

 <tr>
        <td width="100%" align="center" colspan="2">
          &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save" 
                
                
                style="width:10%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnSave_Click" 
                 />  &nbsp;&nbsp; 

       &nbsp;&nbsp; 
           <asp:Button ID="BtnCancel" runat="server" Text="Cancel" 
                
                style="width:10%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  
                />
        </td>
        
    </tr>

</table>


<style type="text/css">

.gvItemCenter { text-align: left; }
.gvHeaderCenter {  text-align: center; }
.gvSelectButton { ForeColor: Blue}
.gvHeaderCenterID { text-align: center; }
.gvItemCenterID { text-align: center;  font-size:small }
.gvItemleftID { text-align: left;margin-left:5px;  font-size:small;padding-left:8px; }
</style>

</asp:Content>

