<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="forecast.aspx.cs" Inherits="IntranetNew_Targets_Forecast" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <script language="javascript" type="text/javascript">

     function updateValues(rw) {
         /*  Important note to use this function designed for grid elements
         This function supports calculation for 2 asp:texts,  1 Label for totals. These fields should be in seperate cols of the grid and 1st element of the cell
         */
         //alert(rw);
         debugger;
         var grid = document.getElementById('<%=Gridview1.ClientID %>');

         rw = rw + 1;  //add 1 to row as header row (0th row) is the first row in html where code has sent row index value which starts from  0 excl;uding header row
         var rev = grid.rows[rw].cells[1];  //Rev Target
         var revrr = grid.rows[rw].cells[2]; // Rev Forecast RR
         var revbtb = grid.rows[rw].cells[3]; // Rev Forecat BTB

         var gp = grid.rows[rw].cells[4]; // GP Target
         var gprr = grid.rows[rw].cells[5]; // GP Forecast RR
         var gpbtb = grid.rows[rw].cells[6]; //GP Forecast BTB 
         var revpercent = grid.rows[rw].cells[7];
         var gppercent = grid.rows[rw].cells[8];
         var rvval = rev.children[0].value;
         var rvrrval = revrr.children[0].value;
         var rvbtbval = revbtb.children[0].value;

         var gpval = gp.children[0].value;
         var gprrval = gprr.children[0].value;
         var gpbtbval = gpbtb.children[0].value;

         revpercent.children[0].innerHTML = (rvrrval + rvbtbval) / rvval * 100;
         gppercent.children[0].innerHTML = (gpbtbval + gprrval) / gpval * 100;


     }
 
 </script>


 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

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
        return confirm(' Email notification will be sent to management. Are you sure you want to save forecast ?');
    }

    </script>

 <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="top:0%;left:35%;width:160px;height:90px;position:absolute;opacity:0.3;background-color:Gray;text-align:center" ><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loadingImgWait.gif" /></div>
            </ProgressTemplate>
     </asp:UpdateProgress>

 <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
   <ContentTemplate>

    <table width="100%" align="center">
        <tr>
            <td width="100%" align="center">
                <lable style="color: #d71313; font-size: x-large; font-weight: bold; font-family: Raleway;"> &nbsp;&nbsp;&nbsp; Forecast  </lable>
            </td>
        </tr>
        <tr>
            <td width="50%"> &nbsp;</td>
            <td width="50%">&nbsp; </td>
        </tr>
        <tr>
            <td width="100%" align="center">

                <asp:Label ID="lblMsg" runat="server" Style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px; color: Red; font-weight: bold;" /> &nbsp;&nbsp;

            </td>
        </tr>
       </table>
 <asp:Panel ID="pnlForms" runat="server" Width="91%"   BorderWidth="1px" BorderColor="Red" EnableTheming="true">
<fieldset style="border-width:1px; border-color:Red">
                    <table width="90%" align="center" >
                       
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
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Month  &nbsp; </label>  
        </td>
        <td width="15%">
             
             <asp:DropDownList ID="ddlMonth" runat="server" 
                 style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  
                 AutoPostBack="True" onselectedindexchanged="ddlMonth_SelectedIndexChanged" > </asp:DropDownList>
       
        </td>
        <td width="15%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> &nbsp; </label>  
        </td>
        <td width="25%">
             
             <asp:DropDownList ID="ddltomonth" runat="server" Visible="false"
                 
                 style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                 onselectedindexchanged="ddltomonth_SelectedIndexChanged" Enabled="False" > </asp:DropDownList>
       
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
        <td>
        <asp:Button ID="btnsave1" runat="server" Text="Save" 
                                    
                Style="width:80%; padding: 5px 12px;
                                    margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;" onclick="btnsave1_Click" 
                                    OnClientClick="return getConfirmation();"
                                  />
        </td>
        <td>
       <%-- <asp:Button ID="BtnExportToExcel" runat="server" Text="Export To Excel" 
                
                Style="width:80%; padding: 5px 12px;
                                    margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;" 
                onclick="BtnExportToExcel_Click"  />--%>
        </td>

    </tr>

                        <tr>
                          <td width="15%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Country  &nbsp; </label>  
        </td>
                            <td colspan="5">
                                <asp:RadioButtonList ID="rbListCountries" runat="server" RepeatDirection="Horizontal" Font-Size="small"
                                    RepeatColumns="5" AutoPostBack="true" OnSelectedIndexChanged="rbListCountries_SelectedIndexChanged">
                                </asp:RadioButtonList>
                            </td>
                        </tr>

                    </table>
                    </fieldset>
                    </asp:Panel>
                    <br />
                    <table align="center" width="100%">
                     <tr>
                            <td width="100%" >
                                <asp:Panel ID="pnlFormList" runat="server" Width="100%" Height="25%"  EnableTheming="true">
                                    <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" 
                                        ForeColor="#333333" ShowFooter="true"
                                        Width="96%" onrowdatabound="Gridview1_RowDataBound" >
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20%" HeaderText="BU" HeaderStyle-Font-Size="Smaller">
                                                <ItemTemplate><asp:Label ID="lblID" runat="server" Font-Size="Smaller" Height="22px"  Text='<%#Eval("BU")%>'></asp:Label></ItemTemplate>
                                                
                                                 <FooterTemplate>
                                                    <asp:Label ID="lblTotal" runat="server" Font-Size="Small" Font-Bold="true" Height="22px"  Text="Total" ></asp:Label>
                                                 </FooterTemplate>
                                                
                                                <ItemStyle HorizontalAlign="Left" Font-Size="Smaller" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <FooterStyle HorizontalAlign="Center"  />
                                                
                                            </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="6%" HeaderText="Rev Target" HeaderStyle-Font-Size="Smaller">
                                                <ItemTemplate><asp:Label ID="lblrevenue" runat="server" Font-Size="Smaller" Height="22px"  Text='<%#Eval("Revenue")%>' ></asp:Label></ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="lblRevTargetTotal" runat="server" Font-Size="Small" Font-Bold="true" Height="22px"  Text="0.00" ></asp:Label>
                                                 </FooterTemplate>
                                                
                                                <ItemStyle HorizontalAlign="Right" Font-Size="Smaller" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <FooterStyle HorizontalAlign="Right"  />

                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="10%" HeaderText="Rev Forecast RR" HeaderStyle-Width="15%" HeaderStyle-Font-Size="Smaller">
                                                <ItemTemplate><asp:TextBox ID="txtrevenuetarget" Width="100px"  Font-Size="Smaller" Height="22px" Text='<%# DataBinder.Eval(Container.DataItem, "RevenueRR")%>' runat="server" MaxLength="10" AutoPostBack="true" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                                </ItemTemplate>

                                                 <FooterTemplate>
                                                    <asp:Label ID="lblRevForecastRRTotal" runat="server" Font-Size="Small" Font-Bold="true" Height="22px"  Text="0.00" ></asp:Label>
                                                 </FooterTemplate>

                                                <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                                                <HeaderStyle HorizontalAlign="Center"  Font-Bold="false" />
                                                <FooterStyle HorizontalAlign="Center"  />

                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-Width="15%" HeaderText="Rev Forecast BTB" HeaderStyle-Font-Size="Smaller">                                                
                                             <ItemTemplate><asp:TextBox ID="txtrevenuetargetbtb" Width="100px"  Font-Size="Smaller" Height="22px" Text='<%# DataBinder.Eval(Container.DataItem, "RevenueBTB")%>' runat="server" MaxLength="10" AutoPostBack="true" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                                </ItemTemplate>

                                                 <FooterTemplate>
                                                    <asp:Label ID="lblRevForecastBTBTotal" runat="server" Font-Size="Small" Font-Bold="true" Height="22px"  Text="0.00" ></asp:Label>
                                                 </FooterTemplate>

                                                <ItemStyle HorizontalAlign="Center" Font-Size="Smaller" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <FooterStyle HorizontalAlign="Center"  />

                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="6%" HeaderText="GP Target" HeaderStyle-Font-Size="Smaller" >
                                                <ItemTemplate><asp:Label ID="lblgp" runat="server" Font-Size="Smaller" Height="22px"  Text='<%#Eval("GP")%>'></asp:Label></ItemTemplate>
                                               
                                                 <FooterTemplate>
                                                    <asp:Label ID="lblGPTargetTotal" runat="server" Font-Size="Small" Font-Bold="true" Height="22px"  Text="0.00" ></asp:Label>
                                                 </FooterTemplate>
                                               
                                                <ItemStyle HorizontalAlign="Right" Font-Size="Smaller"/>
                                                <HeaderStyle HorizontalAlign="Center"  Font-Bold="false" />
                                                <FooterStyle HorizontalAlign="Right"  />

                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="GP Forecast RR" HeaderStyle-Font-Size="Smaller">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgptargets"  Height="22px" Text='<%# DataBinder.Eval(Container.DataItem, "GPRR")%>'
                                                        runat="server" MaxLength="10" Font-Size="Smaller" Width="100px" AutoPostBack="true" onkeypress="javascript:return isNumberKey(event);"  ></asp:TextBox>   <%--onfocusout="javascript:return ValidateDoCalc();"--%>
                                                </ItemTemplate>

                                                  <FooterTemplate>
                                                    <asp:Label ID="lblGPForecastRRTotal" runat="server" Font-Size="Small" Font-Bold="true" Height="22px"  Text="0.00" ></asp:Label>
                                                 </FooterTemplate>

                                                <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <FooterStyle HorizontalAlign="Center"  />

                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15%" HeaderText="GP Forecast BTB" HeaderStyle-Font-Size="Smaller">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgptargetsbtb"  Height="22px" Text='<%# DataBinder.Eval(Container.DataItem, "GPBTB")%>'
                                                        runat="server" MaxLength="10" Font-Size="Smaller" Width="100px" AutoPostBack="true" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblGPForecastBTBTotal" runat="server" Font-Size="Small" Font-Bold="true" Height="22px"  Text="0.00" ></asp:Label>
                                                 </FooterTemplate>

                                                <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <FooterStyle HorizontalAlign="Center"  />

                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="25%" HeaderText="RevForcst V RevTrget %" HeaderStyle-Font-Size="Smaller">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtforcastvstarget" Height="22px"  Text='<%# DataBinder.Eval(Container.DataItem, "forcastvsterget")%>'
                                                        runat="server" MaxLength="6" Width="75px" Font-Size="Smaller" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="27%" HeaderText="GPForcst V GPTrget %" HeaderStyle-Font-Size="Smaller">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtforcastvsgptarget" Height="22px"  Text='<%# DataBinder.Eval(Container.DataItem, "forcastvsgptarget")%>'
                                                        runat="server" MaxLength="6" Font-Size="Smaller" Width="75px" Enabled="false" ></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#990000"  ForeColor="White" />
                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Height="25px" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                        <SortedDescendingHeaderStyle BackColor="#820000" />
                                         <AlternatingRowStyle BackColor="#FFFFFF" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" align="center" >
                                &nbsp;&nbsp;
                                <asp:Button ID="BtnSave" runat="server" Text="Save" 
                                    Style="width: 10%; padding: 5px 12px;
                                    margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;" 
                                    onclick="BtnSave_Click" OnClientClick="return getConfirmation();"/>
                                &nbsp;&nbsp;
                               
                                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" Style="width: 10%; padding: 5px 12px;
                                    margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;" />
                            </td>
                            <td width="20%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>


   </ContentTemplate>
</asp:UpdatePanel>
 
   

</asp:Content>
