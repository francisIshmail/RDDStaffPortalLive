<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="TargetDesigMasterNew.aspx.cs" Inherits="IntranetNew_DesignationsTarget_TargetDesigMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
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
        //        function getConfirmationOnDelete() {
        //            return confirm("Are you sure you want to Delete ?");
        //        }



        //     $(document).ready(function () {
        //         $(function () {
        //             $('[id*=lstEmail1]').multiselect({
        //                 includeSelectAllOption: true,
        //                 buttonWidth: '230px'
        //             });
        //         });
        $(document).ready(function () {
            $(function () {
                $('[id*=lstEmail]').multiselect({
                    //                includeSelectAllOption: true,
                    nonSelectedText: 'Select Email',
                    maxHeight: 350,

                    dropDown: true,
                    enableFiltering: true,
                    buttonWidth: '230px'
                });
            });

        });


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


        $(document).ready(function () {
            $(function () {
                $('[id*=lstEmailRead]').multiselect({
                    //                includeSelectAllOption: true,
                    nonSelectedText: 'Select Email',
                    maxHeight: 350,

                    dropDown: true,
                    enableFiltering: true,
                    buttonWidth: '230px'
                });
            });

        });


    </script>
    <table width="50%" align="center">
        <asp:Label ID="lblMsg" runat="server" Style="color: #d71313; font-size: 25px; font-weight: bold;
            font-family: Raleway;" Text=""></asp:Label>
        <tr>
            <td align="right" colspan="4">
                <lable style="color: #d71313; font-size: x-large; font-weight: bold; font-family: Raleway;">
                    &nbsp;&nbsp;&nbsp;  SETUP - Reporting Frequency & Targets
                </lable>
            </td>
           
            
           
        </tr>

      
        <tr>
            <td>
             &nbsp;
            </td>
            <td>
             &nbsp;
            </td>
            <td width="8px">
             &nbsp;
            </td>
            <td>
             &nbsp;
            </td>
        </tr>

    
        <tr>
            <td>
             &nbsp;
            </td>
            <td>
             &nbsp;
            </td>
            <td width="8px">
             &nbsp;
            </td>
            <td>
             &nbsp;
            </td>
        </tr>
       
        <tr>
            <td width="5%" align="right">
               <b><asp:Label ID="lblcoun" runat="server" Text="Select Country"></asp:Label></b> 
            </td>
            <td align="center">
                <asp:DropDownList ID="ddlCountry" runat="server" class="form-control" Width="105px"
                    AutoPostBack="true" BackColor="AliceBlue" TabIndex="1" Font-Size="Small" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td width="8px">
            </td>
            <td align="center">
                <asp:Button ID="btInsert" runat="server" Class="btn btn-success" OnClick="btInsert_Click"
                    Text="Save Data" />
            </td>
        </tr>
        <tr>
            <td width="100%">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" align="center">
        <tr>
            <td align="center" width="100%">
                <asp:Panel ID="pnlAutoCLStatus" runat="server" BorderColor="Red" BorderWidth="1px"
                    EnableTheming="true" Height="25%" Width="85%">
                    <asp:GridView ID="GRVAutoCLStatusChange" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                        Class="table table-bordered table-condensed table-hover" OnRowCancelingEdit="GRVAutoCLStatusChange_RowCancelingEdit"
                        OnRowDataBound="GRVAutoCLStatusChange_RowDataBound" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="SR.NO" ItemStyle-HorizontalAlign="center"
                                ItemStyle-Width="2%" SortExpression="TargetId">
                                <ItemTemplate>
                                    <asp:Label ID="lblsrno" runat="server" Text="<%#Container.DataItemIndex + 1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderStyle-Width="10%" HeaderText="SALES PERSON"
                                ItemStyle-HorizontalAlign="center" SortExpression="SALES PERSON">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpid" runat="server" Text='<%#Eval("Empid")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderStyle-Width="6%" HeaderText="DESIGNATION"
                                ItemStyle-HorizontalAlign="center" SortExpression="DESIGNATION">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesigId" runat="server" Text='<%#Eval("DesigId")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbldesignation" runat="server" Text='<%#Eval("designation")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderStyle-Width="6%" HeaderText="VISIT PER MONTH"
                                ItemStyle-HorizontalAlign="center" SortExpression="VISIT PER MONTH">
                                <ItemTemplate>
                                    <%--  <asp:Label ID="lblvisitpermonth" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>--%>
                                    <asp:TextBox ID="txtvisiepermonth" runat="server" BackColor="White" Text='<%# Bind("VisitPerMonth")%>'
                                        onkeypress="javascript:return isNumberKey(event);" MaxLength="3" BorderColor="Brown"
                                        class="form-control" Style="font-size: small;" Width="50%"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderStyle-Width="6%" HeaderText="FREQ OF RPT"
                                ItemStyle-HorizontalAlign="center" SortExpression="FREQ OF RPT">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlFrqRPtEdit" runat="server" BackColor="White" Text='<%# Bind("freqOfRpt")%>'
                                        BorderColor="Brown" class="form-control" Style="font-size: small;" Width="90%">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem>DAILY</asp:ListItem>
                                        <asp:ListItem>WEEKLY</asp:ListItem>
                                        <%--<asp:ListItem>FORTHNIGHT</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderStyle-Width="15%" HeaderText="TOP MANAGEMENT"
                                ItemStyle-HorizontalAlign="center" SortExpression="RPT EMAIL TO">
                                <ItemTemplate>
                                    <asp:ListBox ID="lstEmail" runat="server" DataTextField="Text" SelectionMode="Multiple"
                                        Width="200">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                    </asp:ListBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderStyle-Width="15%" HeaderText="RPT MUST READ BY"
                                ItemStyle-HorizontalAlign="center" SortExpression="RPT READ BY">
                                <ItemTemplate>
                                    <asp:ListBox ID="lstEmailRead" runat="server" DataTextField="Text" SelectionMode="Multiple"
                                        Width="200">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                    </asp:ListBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <FooterStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
