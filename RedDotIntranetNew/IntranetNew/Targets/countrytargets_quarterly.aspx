<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="countrytargets_quarterly.aspx.cs" Inherits="IntranetNew_Targets_countrytargets_quarterly"  EnableEventValidation="false" %>

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
    </script>

 <table width="100%" align="center" > 
   
 <tr>
    <td width="80%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway; " >Country Targets (Quarterly)  </Lable>
    </td>
</tr>


<tr>
        <td width="80%" align="center">
        <asp:Label ID="lblMsg" runat="server" 
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />  &nbsp;&nbsp; 
        </td>
</tr>

<tr>
<td align="center">
    <asp:Panel ID="pnlForms" runat="server" Width="80%"   BorderWidth="1px" BorderColor="Red" EnableTheming="true">
<fieldset style="border-width:1px; border-color:Red">
<table width="90%" align="left" cellpadding="3px" cellspacing="3px"  >
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
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Quarter  &nbsp; </label>  
        </td>
        <td width="15%">
             
             <asp:DropDownList ID="ddlQuarter" runat="server" 
                 style="width:95%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  
                 AutoPostBack="True" onselectedindexchanged="ddlQuarter_SelectedIndexChanged" > </asp:DropDownList>
       
        </td>
         <td width="15%" align="left">
        </td>
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
    <td width="100%>
    
<table width="80%" align="center">

  <tr>
  
    <td width="100%" align="center" >
        <%--<asp:Panel ID="pnlFormList" runat="server" Width="75%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">
--%><br />
        <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false"  
                ForeColor="#333333"  Width="80%" BorderWidth="1px" BorderColor="Red" >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="40%"  HeaderText="BU" >

                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" 
                                  Text='<%#Eval("BU")%>' Height="25px" ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemleftID"  />
                            <HeaderStyle CssClass="gvHeaderCenterID" />

                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="20%"  HeaderText="Revenue Targets">
                            <ItemTemplate> 
                                  <asp:TextBox ID="txtrevenuetarget" Font-Size="Small"   Height="25px" Text='<%# DataBinder.Eval(Container.DataItem, "revenue_targets")%>' runat="server" MaxLength="10" Width="150px" onkeypress="javascript:return isNumberKey(event);"   ></asp:TextBox>
                            
                                <%--<asp:Label ID="lblMenu" runat="server" Text='<%#Eval("revenue_targets")%>'></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="20%" HeaderText="GP Targets">
                            <ItemTemplate >
                                  <asp:TextBox ID="txtgptargets" Height="25px" Font-Size="Small"  Text='<%# DataBinder.Eval(Container.DataItem, "GP_targets")%>' runat="server" MaxLength="10" Width="150px" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                            
                               <%-- <asp:Label ID="lblFormName" runat="server" Text='<%#Eval("GP_targets")%>' ></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
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

        <%--</asp:Panel>--%>
    </td>

   </tr>
   <tr>
        <td width="100%" align="center" >
          &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save" 
                
                
                style="width:10%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnSave_Click" 
                 />  &nbsp;&nbsp; 

       &nbsp;&nbsp; 
           <asp:Button ID="BtnCancel" runat="server" Text="Cancel" 
                
                style="width:10%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  
                />
        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>



</table>
    
    
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

