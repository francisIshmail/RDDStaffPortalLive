<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="SalesOrder.aspx.cs" Inherits="IntranetNew_Orders_SalesOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!--Import jQuery before export.js-->
    <script src="https://code.jquery.com/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.7.7/xlsx.core.min.js"></script>
    <!--Data Table-->
    <style type="text/css">
        .SoftHardBlocked
        {
            color: #d71313;
            font-size: large;
            font-weight: bold;
            font-family: Raleway;
        }
        
        .Dormant
        {
            color: #0d05f5;
            font-size: large;
            font-weight: bold;
            font-family: Raleway;
        }
        
        .Active
        {
            color: #0db854;
            font-size: large;
            font-weight: bold;
            font-family: Raleway;
        }
        
        .Ok
        {
            color: #0db854;
            font-size: large;
            font-weight: bold;
            font-family: Raleway;
        }
        
        .LimitExpiredClosed
        {
            color: #d71313;
            font-size: large;
            font-weight: bold;
            font-family: Raleway;
        }
        #html
        {
            margin: 40px auto;
        }
        .btn-search
        {
            background: orange;
            border-radius: 0;
            color: white;
            border-width: 1px;
            border-style: solid;
            border-color: darkorange;
        }
        .btn-search:link, .btn-search:visited
        {
            color: white;
        }
        .btn-search:active, .btn-search:hover
        {
            background: darkorange;
            color: white;
        }
        
        .btn-sap
        {
            background: #0099cc;
            border-radius: 0;
            color: white;
            border-width: 1px;
            border-style: solid;
            border-color: #0099cc;
        }
        .btn-sap:link, .btn-sap:visited
        {
            color: white;
        }
        .btn-sap:active, .btn-sap:hover
        {
            background: #0086b3;
            color: white;
        }
        
        #classModal
        {
        }
        
        .modal-body
        {
            overflow-x: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../Orders/js/SalesOrder.js" type="text/javascript"></script>
    <div>
        <table width="100%" align="center">
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
                    <asp:Label ID="lblMsg" runat="server" Style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;
                        font-family: Raleway; font-size: 14px; color: Red; font-weight: bold;" />
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="pgHeader" class="pageHeader" style="background-color: #ffe6e6; color: #d71313;
        font-size: x-large; font-weight: bold; font-family: Raleway; text-align: center;
        border: 1px solid red;">
        <span>Sales Order</span>
    </div>
    <div style="border: 1px solid red;">
        <div>
            <fieldset>
                <div id="forecast1" style="margin-top: 10px; margin-left: 15px; margin-bottom: 10px;
                    width: 100%">
                    <table style="width: 100%; margin-left: -15px" cellpadding="3px" cellspacing="3px">
                        <tr>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: bold; color: red">
                                <span>DataBase *</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:DropDownList ID="cbDataBase" runat="server" class="form-control" AutoPostBack="false"
                                    Style="width: 180px; height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;
                                    background-color: #ffbf00" TabIndex="0" placeholder="Select DB">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <button id="btnSer" type="button" style="font-weight: bold; font-family: Verdana;
                                    height: 33px; width: 80px;" class="btn btn-search">
                                    <i class="fa fa-search fa-fw"></i>Search
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Customer Code
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:TextBox ID="txtCardCode" runat="server" MaxLength="254" class="form-control"
                                    Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;"
                                    Enabled="false">  </asp:TextBox>
                            </td>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>RDD Project</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                    *</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:DropDownList ID="cbRDDProject" runat="server" class="form-control" Style="width: 180px;
                                    height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                    <asp:ListItem>Select RDD Project </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Posting Date</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:TextBox ID="txtPostingDate" runat="server" MaxLength="10" Style="width: 180px;
                                    height: 33px; font-family: Verdana; font-size: 13px; font-weight: normal;"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtPostingDate_CalendarExtender1" runat="server" Enabled="True"
                                    TargetControlID="txtPostingDate" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"
                                    Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                                <%-- <input id="txtPostingDate" type="text" class="calendar compact-md"   Style="width: 180px;
                                    height: 33px; font-family: Verdana; font-size: 13px; font-weight: normal;">--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Customer Name <span style="color: Red; font-size: 20px; font-weight: bold;">*</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:TextBox ID="txtCardName" runat="server" MaxLength="254" placeholder="Select Customer"
                                    class="form-control" Width="180px" Height="33px" Style="font-family: Raleway;
                                    font-size: 14px; font-weight: normal;">  </asp:TextBox>
                            </td>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Business Type</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                    *</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:DropDownList ID="cbBusinessType" runat="server" class="form-control" Style="width: 180px;
                                    height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                    <asp:ListItem>Select Business Type</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Delivery Date</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:TextBox ID="txtDelDate" runat="server" MaxLength="10" Style="width: 180px; height: 33px;
                                    font-family: Verdana; font-size: 13px; font-weight: normal;" Enabled="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDelDate_Calendarextender1" runat="server" Enabled="True"
                                    TargetControlID="txtDelDate" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"
                                    Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Ref. Num</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:TextBox ID="txtRefNum" runat="server" MaxLength="254" class="form-control" Width="180px"
                                    Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;">  </asp:TextBox>
                            </td>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Invoice PayTerms</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                    *</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:DropDownList ID="cbInvPayTerm" runat="server" class="form-control" Style="width: 180px;
                                    height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                    <asp:ListItem>Select Inv PayTerm</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Document Status</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:TextBox ID="txtDocStatus" runat="server" MaxLength="254" class="form-control"
                                    Enabled="false" Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px;
                                    font-weight: bold; color: Blue">Draft</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Forwarder Details</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:TextBox ID="txt_ForworderDet" runat="server" MaxLength="254" class="form-control"
                                    Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;">  </asp:TextBox>
                            </td>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Cust. PayTerms</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                    *</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:DropDownList ID="cbCustPayTerm" runat="server" class="form-control" AutoPostBack="false"
                                    Style="width: 180px; height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;"
                                    TabIndex="0">
                                    <asp:ListItem>Select PayTerms</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Approved By</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:TextBox ID="txtApprvBy" runat="server" MaxLength="254" class="form-control"
                                    Enabled="false" Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px;
                                    font-weight: normal;">  </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Sales Employee</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                    *</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:DropDownList ID="cbSalesEmp" runat="server" class="form-control" Style="width: 180px;
                                    height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                    <asp:ListItem>-No Sales Employee-</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td colspan="3" style="width: 17%; padding-left: 38.6%; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                <span>Created By</span>
                            </td>
                            <td style="padding-top: 0.5%;">
                                <asp:TextBox ID="txtCreatedBy" runat="server" MaxLength="254" class="form-control"
                                    Enabled="false" Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px;
                                    font-weight: normal;">  </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <%--<table id="example" class="display" style="width: 100%">
            </table>--%>
        </div>
        <div>
            <fieldset>
                <div id="Div1" style="margin-top: 10px; margin-left: 15px; margin-bottom: 10px; width: 100%">
                    <table style="width: 100%; margin-left: -15px; margin-right: 3px;" cellpadding="3px"
                        cellspacing="3px">
                        <tr>
                            <td style="width: 130px; padding-left: 5px; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal; text-align: center;">
                                <span>Credit Limit</span>
                            </td>
                            <td style="width: 130px; padding-left: 5px; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal; text-align: center;">
                                <span>0-30</span>
                            </td>
                            <td style="width: 130px; padding-left: 5px; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal; text-align: center;">
                                <span>31-45</span>
                            </td>
                            <td style="width: 130px; padding-left: 5px; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal; text-align: center;">
                                <span>46-60</span>
                            </td>
                            <td style="width: 130px; padding-left: 5px; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal; text-align: center;">
                                <span>61-90</span>
                            </td>
                            <td style="width: 130px; padding-left: 5px; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal; text-align: center;">
                                <span>91+</span>
                            </td>
                            <td style="width: 130px; padding-left: 0px; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal; text-align: center;">
                                <span>Transaction Status</span>
                            </td>
                            <td style="width: 130px; padding-left: 0px; padding-top: 0.5%; font-family: Raleway;
                                font-size: 14px; font-weight: normal; text-align: center;">
                                <span>Credit Limit Status</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="padding-top: 0.5%; vertical-align: top;">
                                <asp:TextBox ID="txtCreditLimit" runat="server" MaxLength="254" class="form-control"
                                    Width="130px" Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                    text-align: right" Enabled="false">  </asp:TextBox>
                            </td>
                            <td align="center" style="padding-top: 0.5%;">
                                <asp:TextBox ID="txt_0_30" runat="server" MaxLength="254" class="form-control" Width="130px"
                                    Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                    text-align: right" Enabled="false">  </asp:TextBox>
                            </td>
                            <td align="center" style="padding-top: 0.5%;">
                                <asp:TextBox ID="txt_31_45" runat="server" MaxLength="254" class="form-control" Width="130px"
                                    Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                    text-align: right" Enabled="false">  </asp:TextBox>
                            </td>
                            <td align="center" style="padding-top: 0.5%;">
                                <asp:TextBox ID="txt_46_60" runat="server" MaxLength="254" class="form-control" Width="130px"
                                    Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                    text-align: right" Enabled="false">  </asp:TextBox>
                            </td>
                            <td align="center" style="padding-top: 0.5%;">
                                <asp:TextBox ID="txt_61_90" runat="server" MaxLength="254" class="form-control" Width="130px"
                                    Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                    text-align: right" Enabled="false">  </asp:TextBox>
                            </td>
                            <td align="center" style="padding-top: 0.5%;">
                                <asp:TextBox ID="txt_91_Above" runat="server" MaxLength="254" class="form-control"
                                    Width="130px" Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                    text-align: right" Enabled="false">  </asp:TextBox>
                            </td>
                            <td align="center" style="padding-top: 0.5%;">
                                <asp:TextBox ID="txtTrnStatus" runat="server" MaxLength="254" class="form-control"
                                    Style="font-family: Verdana; font-size: 14px; font-weight: normal; text-align: center;
                                    width: 130px; height: 33px;" Enabled="false">  </asp:TextBox>
                            </td>
                            <td align="center" style="padding-top: 0.5%;">
                                <asp:TextBox ID="txtCLStatus" runat="server" MaxLength="254" class="form-control"
                                    Style="font-family: Verdana; font-size: 14px; font-weight: normal; text-align: center;
                                    width: 130px; height: 33px;" Enabled="false">  </asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </div>
        <div>
            <table cellpadding="1" cellspacing="0" style="text-align: left; margin-left: 6px;
                width: 99%">
                <tr>
                    <td colspan="4">
                        <div id="tabs" style="background: transparent; border-style: solid; border-width: 1px;
                            border-color: Red;">
                            <ul>
                                <li class="ui-state-active"><a href="#tabs-1">Contents</a></li>
                                <li><a href="#tabs-2">Payment Terms</a></li>
                            </ul>
                            <div id="tabs-1" style="height: 425px; width: 99%;">
                                <div style="border: 1px solid red;">
                                    <fieldset>
                                        <div id="Div5" style="margin-top: 10px; margin-left: 15px; margin-bottom: 10px; width: 100%">
                                            <table style="width: 100%; margin-left: -15px" cellpadding="3px" cellspacing="3px">
                                                <tr>
                                                    <td style="width: 12%; padding-left: 1%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        <span>Item Code</span><span style="color: Red; font-size: 12px; font-weight: bold;">
                                                            *</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:TextBox ID="txtItem" runat="server" MaxLength="254" class="form-control" Width="180px"
                                                            Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;">  </asp:TextBox>
                                                    </td>
                                                    <td style="width: 73%;">
                                                        <asp:TextBox ID="txtDescr" runat="server" MaxLength="254" class="form-control" Width="400px"
                                                            Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;"
                                                            Enabled="false">  </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table style="width: 100%; margin-left: -15px" cellpadding="3px" cellspacing="3px">
                                                <tr>
                                                    <td style="width: 12%; padding-left: 1%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        Whs Code<span style="color: Red; font-size: 12px; font-weight: bold;"> *</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:DropDownList ID="cbWhs" runat="server" class="form-control" AutoPostBack="false"
                                                            Style="font-family: Verdana; font-size: 14px; font-weight: normal;" Width="180px"
                                                            Font-Size="Medium">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10%; padding-left: 2%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        <span>Qty</span><span style="color: Red; font-size: 12px; font-weight: bold;"> *</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:TextBox ID="txtQt" runat="server" MaxLength="254" class="form-control" Width="120px"
                                                            Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                                            text-align: right;" onkeypress="javascript:return isNumberKey(event);">  </asp:TextBox>
                                                    </td>
                                                    <td style="width: 10%; padding-left: 2%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        <span>Qty In Whs</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:TextBox ID="txtQtyWhs" runat="server" MaxLength="254" class="form-control" Width="120px"
                                                            Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                                            text-align: right;" Enabled="false">  </asp:TextBox>
                                                    </td>
                                                    <td style="width: 10%; padding-left: 2%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        <span>Qty Aval.</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:TextBox ID="txtQtAval" runat="server" MaxLength="254" class="form-control" Width="120px"
                                                            Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                                            text-align: right;" Enabled="false">  </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%; padding-left: 1%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        Tax Code<span style="color: Red; font-size: 12px; font-weight: bold;"> *</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:DropDownList ID="cbTax" runat="server" class="form-control" AutoPostBack="false"
                                                            Style="font-family: Verdana; font-size: 14px; font-weight: normal;" Width="180px"
                                                            Font-Size="Medium">
                                                        </asp:DropDownList>
                                                        <input type="hidden" id="txtTaxRate" value="" />
                                                    </td>
                                                    <td style="width: 10%; padding-left: 2%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        <span>Unit Price</span><span style="color: Red; font-size: 12px; font-weight: bold;">
                                                            *</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:TextBox ID="txtUnitPrice" runat="server" MaxLength="254" class="form-control"
                                                            AutoCompleteType="Disabled" Width="120px" Height="33px" Style="font-family: Verdana;
                                                            font-size: 14px; font-weight: normal; text-align: right;" onkeypress="javascript:return isNumberKey(event);">  </asp:TextBox>
                                                    </td>
                                                    <td style="width: 10%; padding-left: 2%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        <span>Dis(%)</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:TextBox ID="txtDisc" runat="server" MaxLength="254" class="form-control" Width="120px"
                                                            Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                                            text-align: right;" Enabled="false" Text="0.00" onkeypress="javascript:return isNumberKey(event);">  </asp:TextBox>
                                                    </td>
                                                    <td style="width: 10%; padding-left: 2%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        <span>Total ($)</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:TextBox ID="txtTot" runat="server" MaxLength="254" class="form-control" Width="120px"
                                                            Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                                            text-align: right;" Enabled="false">  </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 12%; padding-left: 1%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        opgRefAlpha<span style="color: Red; font-size: 12px; font-weight: bold;"> *</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:DropDownList ID="cbopg" runat="server" class="form-control" AutoPostBack="false"
                                                            Style="font-family: Verdana; font-size: 14px; font-weight: normal;" Width="180px"
                                                            Font-Size="Medium">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 10%; padding-left: 2%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        <span>GP</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <asp:TextBox ID="txt_GP" runat="server" MaxLength="254" class="form-control" Width="120px"
                                                            Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                                            text-align: right;" Enabled="false">  </asp:TextBox>
                                                    </td>
                                                    <td style="width: 10%; padding-left: 2%; padding-top: 0.5%; font-family: Verdana;
                                                        font-size: 14px; font-weight: normal;">
                                                        <span>GP (%)</span>
                                                    </td>
                                                    <td style="padding-top: 0.5%;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_GPPer" runat="server" MaxLength="254" class="form-control" Width="90px"
                                                                        Height="33px" Style="font-family: Verdana; font-size: 14px; font-weight: normal;
                                                                        text-align: right;" Enabled="false">  </asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <img id="btnRefresh" src="../../images/toolbar/Refresh.gif" style="width: 33px; height: 29px;" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td colspan="2" style="padding-top: 0.5%; width: 10%; padding-left: 1%;">
                                                        <%-- <asp:Button ID="btn_AddRow" Text="Add Row" class="btn btn-danger" Enabled="true"
                                                        Font-Bold="true" Font-Size="Medium" Style="font-family: Raleway; height: 33px;
                                                        width: 150px;" TabIndex="20" />--%>
                                                        <button id="btn_AddRow" type="button" style="font-family: Verdana; height: 33px;
                                                            width: 90px;" class="btn btn-primary">
                                                            <span></span><b>Add Row</b>
                                                        </button>
                                                        <button id="btn_DelRow" type="button" style="font-family: Verdana; height: 33px;
                                                            width: 90px;" class="btn btn-danger disabled">
                                                            <span></span><b>Del Row</b>
                                                        </button>
                                                        <button id="btn_clear" type="button" style="font-family: Verdana; height: 33px; width: 90px;"
                                                            class="btn btn-info">
                                                            <span></span><b>Clear</b>
                                                        </button>
                                                        <input type="hidden" id="uxTrowindex" value="" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </fieldset>
                                </div>
                                <div>
                                    <div>
                                        <div style="width: 100%; height: 210px; border: 1px solid red;">
                                            <div class="box-body table-responsive table-container" style="margin-top: 10px;">
                                                <div id="example2_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
                                                    <div class="col-sm-12" style="overflow: auto; height: 175px;">
                                                        <table class="table table-bordered table-striped dataTable no-footer " id="gvItem"
                                                            role="grid">
                                                            <thead class="thead">
                                                                <tr class="header-bg" role="row" style="background-color: #cc0000; color: white;">
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        #
                                                                    </th>
                                                                    <th class="sorting_asc " tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        aria-sort="ascending" aria-label="ID: activate to sort column descending">
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        Item Code
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        Descriptiion
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        Quantity
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        Unit Price ($)
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        Disc (%)
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        Tax Code
                                                                    </th>
                                                                    <th class="hide" tabindex="0" aria-controls="example2" rowspan="1" colspan="1" style="color: White;">
                                                                        Tax Rate
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        Total ($)
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        Warehouse
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        Qty In Whs
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        Qty Aval
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        opgRefAlpha
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        GP
                                                                    </th>
                                                                    <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                                        style="color: White;">
                                                                        GP(%)
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<table id="gvItem" class="grid-container">
                                                <thead>
                                                    <tr class="grid-item" style="border: 1px solid red; color: White">
                                                        <th class="grid-item">
                                                        </th>
                                                        <th class="grid-item">
                                                            #
                                                        </th>
                                                        <th class="grid-item">
                                                            Item Code
                                                        </th>
                                                        <th class="grid-item" style="width: 207px">
                                                            Descriptiion
                                                        </th>
                                                        <th class="grid-item">
                                                            Quantity
                                                        </th>
                                                        <th class="grid-item">
                                                            Unit Price ($)
                                                        </th>
                                                        <th class="grid-item">
                                                            Tax Code
                                                        </th>
                                                        <th class="grid-item">
                                                            Dic(%)
                                                        </th>
                                                        <th class="grid-item" style="width: 180px">
                                                            Total ($)
                                                        </th>
                                                        <th class="grid-item">
                                                            Warehose
                                                        </th>
                                                        <th class="grid-item">
                                                            Qty In Whs
                                                        </th>
                                                        <th class="grid-item">
                                                            Qty Aval.
                                                        </th>
                                                        <th class="grid-item">
                                                            opgRefAlpha
                                                        </th>
                                                        <th class="grid-item">
                                                            GP
                                                        </th>
                                                        <th class="grid-item">
                                                            GP (%)
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>--%>
                                        </div>
                                    </div>
                                </div>
                                <%--  <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                Width="100%" CellPadding="4" ForeColor="#333333" AllowPaging="True" AllowSorting="True"
                                BorderColor="#CC3300" BorderStyle="Solid" BorderWidth="1px">
                                <AlternatingRowStyle BackColor="#CCFFFF" BorderStyle="Solid" Wrap="True" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                            <asp:Label ID="lblPVLineId" runat="server" Text='<%#Eval("PVLineId") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="2.5%" />
                                        <HeaderStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                        <ItemStyle Width="2.5%" BackColor="#990000" ForeColor="White" HorizontalAlign="Center"
                                            VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemCode" runat="server" placeholder="ItemCode" class="form-control"
                                                Font-Size="Medium" Text='<%#Eval("ItemCode") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" placeholder="Enter Description" autocomplete="off"
                                                class="form-control" Font-Size="Medium" Text='<%#Eval("Description") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtgvQty" runat="server" autocomplete="off" Font-Size="Medium" AutoPostBack="true"
                                                        placeholder="Enter Qty" class="form-control" Text='<%#Eval("Qty") %>' onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Price ($)" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtgvPrice" runat="server" autocomplete="off" Font-Size="Medium"
                                                        AutoPostBack="true" placeholder="Price" class="form-control" Text='<%#Eval("Price") %>'
                                                        onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax Code" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTaxCode" runat="server" placeholder="TaxCode" autocomplete="off"
                                                class="form-control" Font-Size="Medium" Text='<%#Eval("TaxCode") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="4%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dis %" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtgvDis" runat="server" autocomplete="off" Font-Size="Medium" AutoPostBack="true"
                                                        placeholder="Price" class="form-control" Text='<%#Eval("Dis") %>' onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total (USD)" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtgvTotal" runat="server" autocomplete="off" Font-Size="Medium"
                                                        AutoPostBack="false" placeholder="Price" class="form-control" Text='<%#Eval("Total") %>'
                                                        onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warehoue" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtWhsCode" runat="server" placeholder="TaxCode" autocomplete="off"
                                                class="form-control" Font-Size="Medium" Text='<%#Eval("WhsCode") %>'></asp:TextBox>
                            <asp:DropDownList ID="cbWhsCode" runat="server" class="form-control" AutoPostBack="false"
                                Font-Size="Medium">
                            </asp:DropDownList>
                            </ItemTemplate>
                            <itemstyle width="4%"></itemstyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty in WHS" ItemStyle-Width="4%">
                                <itemtemplate>
                                            <asp:TextBox ID="txtQtyInWhs" runat="server" placeholder="TaxCode" autocomplete="off"
                                                class="form-control" Font-Size="Medium" Text='<%#Eval("QtyInWhs") %>'></asp:TextBox>
                                        </itemtemplate>
                                <itemstyle width="4%"></itemstyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty Available" ItemStyle-Width="4%">
                                <itemtemplate>
                                            <asp:TextBox ID="txtQtyAval" runat="server" autocomplete="off" class="form-control"
                                                Font-Size="Medium" Text='<%#Eval("QtyAval") %>'></asp:TextBox>
                                        </itemtemplate>
                                <itemstyle width="4%"></itemstyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="opgRefAlpha" ItemStyle-Width="4%">
                                <itemtemplate>
                                            <asp:TextBox ID="txtopgRefAlpha" runat="server" autocomplete="off" class="form-control"
                                                Font-Size="Medium" Text='<%#Eval("opgRefAlpha") %>'></asp:TextBox>
                                        </itemtemplate>
                                <itemstyle width="4%"></itemstyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GP" ItemStyle-Width="4%">
                                <itemtemplate>
                                            <asp:TextBox ID="txtGP" runat="server" autocomplete="off" class="form-control" Font-Size="Medium"
                                                Text='<%#Eval("GP") %>'></asp:TextBox>
                                        </itemtemplate>
                                <itemstyle width="4%"></itemstyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GP %" ItemStyle-Width="4%">
                                <itemtemplate>
                                            <asp:TextBox ID="txtGPPer" runat="server" autocomplete="off" class="form-control"
                                                Font-Size="Medium" Text='<%#Eval("GPPer") %>'></asp:TextBox>
                                        </itemtemplate>
                                <itemstyle width="4%"></itemstyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="7%">
                                <itemtemplate>
                                            <asp:Button ID="btnDel" runat="server" Text="Delete" Width="60%" CommandName="Delete"
                                                class="btn btn-danger" />
                                        </itemtemplate>
                                <itemstyle width="7%"></itemstyle>
                                <footerstyle horizontalalign="Right" />
                                <footertemplate>
                                            <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />
                                        </footertemplate>
                            </asp:TemplateField>
                            </Columns>
                            <footerstyle backcolor="#990000" font-bold="True" forecolor="White" height="20px" />
                            <headerstyle backcolor="#990000" font-bold="True" forecolor="White" height="65px"
                                bordercolor="White" borderstyle="Solid" borderwidth="1px" />
                            <pagerstyle backcolor="#FFCC66" forecolor="#333333" horizontalalign="Center" />
                            <rowstyle backcolor="#FFFBD6" forecolor="#333333" bordercolor="#CCCCCC" borderstyle="Solid"
                                borderwidth="1px" />
                            <selectedrowstyle backcolor="#FFCC66" font-bold="True" forecolor="Navy" />
                            <sortedascendingcellstyle backcolor="#FDF5AC" />
                            <sortedascendingheaderstyle backcolor="#4D0000" />
                            <sorteddescendingcellstyle backcolor="#FCF6C0" />
                            <sorteddescendingheaderstyle backcolor="#820000" />
                            </asp:GridView>--%>
                            </div>
                            <div id="tabs-2" style="height: 425px; width: 100%;">
                                <div id="Div2" style="margin-top: 10px; margin-left: 15px; margin-bottom: 10px; width: 100%">
                                    <table style="width: 100%; margin-left: -15px" cellpadding="3px" cellspacing="3px">
                                        <tr>
                                            <td style="width: 25%; padding-left: 6%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Payment Method 1</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                                    *</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:DropDownList ID="cbPayMth1" runat="server" class="form-control" Style="width: 180px;
                                                    height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 20%; padding-left: 1%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Payment Method 2</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:DropDownList ID="cbPayMth2" runat="server" class="form-control" Style="width: 180px;
                                                    height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%; padding-left: 6%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Receipt/Check No 1</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                                    *</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:TextBox ID="txtCheck1" runat="server" MaxLength="254" class="form-control" Width="180px"
                                                    Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;">  </asp:TextBox>
                                            </td>
                                            <td style="width: 20%; padding-left: 1%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Receipt/Check No 2</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:TextBox ID="txtCheck2" runat="server" MaxLength="254" class="form-control" Width="180px"
                                                    Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;">  </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%; padding-left: 6%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Receipt/Check Date 1</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                                    *</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:TextBox ID="txtChkDate1" runat="server" MaxLength="254" 
                                                    Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;">  </asp:TextBox>
                                                <cc1:CalendarExtender ID="txtChkDate1_CalendarExtender1" runat="server" Enabled="True"
                                                    TargetControlID="txtChkDate1" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 20%; padding-left: 1%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Receipt/Check Date 2</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:TextBox ID="txtChkDate2" runat="server" MaxLength="254" 
                                                    Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;">  </asp:TextBox>
                                                <cc1:CalendarExtender ID="txtChkDate2_CalendarExtender1" runat="server" Enabled="True"
                                                    TargetControlID="txtChkDate2" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%; padding-left: 6%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Remarks 1</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                                    *</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:TextBox ID="txtRemarks1" runat="server" MaxLength="254" class="form-control"
                                                    Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;">  </asp:TextBox>
                                            </td>
                                            <td style="width: 20%; padding-left: 1%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Remarks 2</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:TextBox ID="txtRemarks2" runat="server" MaxLength="254" class="form-control"
                                                    Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;">  </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%; padding-left: 6%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Currency 1</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                                    *</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:DropDownList ID="cbCur1" runat="server" class="form-control" Style="width: 180px;
                                                    height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 20%; padding-left: 1%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Currency 2</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:DropDownList ID="cbCur2" runat="server" class="form-control" Style="width: 180px;
                                                    height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%; padding-left: 6%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Amount 1</span><span style="color: Red; font-size: 20px; font-weight: bold;">
                                                    *</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:TextBox ID="txtAmount1" runat="server" MaxLength="254" class="form-control"
                                                    Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;"
                                                    onkeypress="javascript:return isNumberKey(event);">  </asp:TextBox>
                                            </td>
                                            <td style="width: 20%; padding-left: 1%; padding-top: 0.5%; font-family: Raleway;
                                                font-size: 14px; font-weight: normal;">
                                                <span>Amount 2</span>
                                            </td>
                                            <td style="padding-top: 0.5%;">
                                                <asp:TextBox ID="txtAmount2" runat="server" MaxLength="254" class="form-control"
                                                    Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;"
                                                    onkeypress="javascript:return isNumberKey(event);">  </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <fieldset>
                <div id="Div3" style="margin-top: 10px; margin-left: 15px; margin-bottom: 10px; width: 100%">
                    <table style="width: 100%; margin-left: -15px" cellpadding="3px" cellspacing="3px">
                        <tr>
                            <td style="width: 50%">
                                <table style="width: 100%; margin-left: -15px" cellpadding="3px" cellspacing="3px">
                                    <tr>
                                        <td style="width: 25%; padding-left: 5%; padding-top: 0.5%; font-family: Raleway;
                                            font-size: 14px; font-weight: bold;">
                                            <span>Download Template</span>
                                        </td>
                                        <td style="padding-top: 0.5%;">
                                            <%--<button id="btnGetTemplate" class="button primary">...</button>--%>
                                            <button id="btnGetTemplate" type="button" style="font-weight: bold; font-family: Verdana;
                                                height: 29px; width: 45px;" class="btn btn-primary">
                                                <span></span><b>...</b>
                                            </button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; padding-left: 5%; padding-top: 0.5%; font-family: Raleway;
                                            font-size: 14px; font-weight: bold;">
                                            <span>Import Excel </span>
                                        </td>
                                        <td style="padding-top: 0.5%;">
                                            <%--<div class="input-group">
                                                <div class="custom-file">--%>
                                            <input id="btnExcelImport" data-cls-button="bg-darkOrange fg-white" style="font-family: Verdana;
                                                font-size: 13px; font-weight: normal;" type="file" data-role="file" data-button-title="...">
                                            <%--<input id="btnExcelImport" type="file" data-role="file" data-cls-caption="bg-orange fg-white" data-cls-button="bg-darkOrange fg-white" />--%>
                                            <%--<input type="file" class="custom-file-input" id="btnExcelImport" />--%>
                                            <%-- <label class="custom-file-label" for="btnExcelImport">
                                                        Choose file</label>--%>
                                            <%--</div>
                                            </div>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; height: 33px; padding-left: 5%; padding-top: 0.5%; font-family: Raleway;
                                            font-size: 14px; font-weight: bold;">
                                            <span></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%; padding-left: 5%; padding-top: 0.5%; font-family: Raleway;
                                            font-size: 14px; font-weight: bold;">
                                            <span>Remarks </span>
                                        </td>
                                        <td style="padding-top: 0.5%;">
                                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="254" class="form-control"
                                                Width="100%" Height="70px" Style="font-family: Verdana; font-size: 13px; font-weight: normal;
                                                padding-top: 0.5%;" TextMode="MultiLine">  </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%">
                                <table style="width: 100%; margin-left: -15px" cellpadding="3px" cellspacing="3px">
                                    <tr>
                                        <td style="width: 68%; padding-left: 40%; padding-top: 0.5%; font-family: Raleway;
                                            font-size: 14px; font-weight: bold;">
                                            <span>Total Before Tax </span>
                                        </td>
                                        <td style="padding-top: 0.5%;">
                                            <asp:TextBox ID="txtTotBefTax" runat="server" MaxLength="254" class="form-control"
                                                Width="180px" Height="33px" Style="font-family: Verdana; font-size: 13px; font-weight: normal;
                                                text-align: right; padding-top: 0.5%;" Enabled="false" onkeypress="javascript:return isNumberKey(event);">  </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 68%; padding-left: 40%; padding-top: 0.5%; font-family: Raleway;
                                            font-size: 14px; font-weight: bold;">
                                            <span>Tax</span>
                                        </td>
                                        <td style="padding-top: 0.5%;">
                                            <asp:TextBox ID="txtTotalTax" runat="server" MaxLength="254" class="form-control"
                                                Width="180px" Height="33px" Style="font-family: Verdana; font-size: 13px; font-weight: normal;
                                                text-align: right; padding-top: 0.5%;" Enabled="false" onkeypress="javascript:return isNumberKey(event);">  </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 68%; padding-left: 40%; padding-top: 0.5%; font-family: Raleway;
                                            font-size: 14px; font-weight: bold;">
                                            <span>Total</span>
                                        </td>
                                        <td style="padding-top: 0.5%;">
                                            <asp:TextBox ID="txtTotal" runat="server" MaxLength="254" class="form-control" Width="180px"
                                                Height="33px" Style="font-family: Verdana; font-size: 13px; font-weight: normal;
                                                text-align: right; padding-top: 0.5%;" Enabled="false" onkeypress="javascript:return isNumberKey(event);">  </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 68%; padding-left: 40%; padding-top: 0.5%; font-family: Raleway;
                                            font-size: 14px; font-weight: bold;">
                                            <span>GP USD </span>
                                        </td>
                                        <td style="padding-top: 0.5%;">
                                            <asp:TextBox ID="txtGP" runat="server" MaxLength="254" class="form-control" Width="180px"
                                                Height="33px" Style="font-family: Verdana; font-size: 13px; font-weight: normal;
                                                text-align: right; padding-top: 0.5%;" Enabled="false" onkeypress="javascript:return isNumberKey(event);">  </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40%; padding-left: 40%; padding-top: 0.5%; font-family: Raleway;
                                            font-size: 14px; font-weight: bold;">
                                            <span>GP %</span>
                                        </td>
                                        <td style="padding-top: 0.5%;">
                                            <asp:TextBox ID="txtGPPer" runat="server" MaxLength="254" class="form-control" Width="180px"
                                                Height="33px" Style="font-family: Verdana; font-size: 13px; font-weight: normal;
                                                text-align: right; padding-top: 0.5%;" Enabled="false" onkeypress="javascript:return isNumberKey(event);">  </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </div>
        <div>
            <fieldset>
                <div id="Div4" style="margin-top: 10px; margin-left: 15px; margin-bottom: 10px; width: 100%">
                    <table style="width: 100%; margin-left: -15px" cellpadding="3px" cellspacing="3px">
                        <tr>
                            <td style="padding-top: 0.5%; padding-left: 3%; width: 1%">
                                <button id="btnSave" type="button" style="font-weight: bold; font-family: Verdana;
                                    height: 33px; width: 150px;" class="btn btn-primary">
                                    <span></span><b>Save</b>
                                </button>
                            </td>
                            <td style="padding-top: 0.5%; padding-left: 0%; width: 1%">
                                <button id="btnCancel" type="button" style="font-weight: bold; font-family: Verdana;
                                    height: 33px; width: 150px;" class="btn btn-danger">
                                    <span></span><b>Cancel</b>
                                </button>
                            </td>
                            <td style="padding-top: 0.5%; padding-left: 0%; width: 1%">
                                <button id="btnSAPPost" type="button" style="font-weight: bold; font-family: Verdana;
                                    height: 33px; width: 150px;" class="btn btn-sap">
                                    Post To SAP B1
                                </button>
                            </td>
                            <td style="padding-top: 0.5%; padding-left: 0%; width: 25%">
                                <button id="btnSearch" type="button" style="font-weight: bold; font-family: Verdana;
                                    height: 33px; width: 150px;" class="btn btn-search">
                                    <i class="fa fa-search fa-fw"></i>Search
                                </button>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </div>
    </div>
    <div id="classModal" class="modal fade large bs-example-modal-lg" tabindex="-1" role="dialog"
        aria-labelledby="classInfo" aria-hidden="true">
        <div class="modal-dialog modal-lg" style="width: 80%; height: 50%;">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #ffc34d;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×
                    </button>
                    <h4 class="modal-title" id="classModalLabel" style="color: White; font-weight: bold;
                        font-family: Raleway;">
                        Sales Order Search
                    </h4>
                </div>
                <div>
                    <fieldset>
                        <div id="Div6" style="margin-top: 10px; margin-left: 15px; margin-bottom: 10px; width: 100%">
                            <table style="width: 100%; margin-left: -15px" cellpadding="3px" cellspacing="3px">
                                <tr>
                                    <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                        font-size: 14px; font-weight: normal;">
                                        <span>From Date</span>
                                    </td>
                                    <td style="padding-top: 0.5%;">
                                        <asp:TextBox ID="txtSerFrmDate" runat="server" MaxLength="10" Style="width: 180px;
                                            height: 33px; font-family: Verdana; font-size: 13px; font-weight: normal;"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFrmDate_CalendarExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtSerFrmDate" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                        font-size: 14px; font-weight: normal;">
                                        <span>To Date</span>
                                    </td>
                                    <td style="padding-top: 0.5%;">
                                        <asp:TextBox ID="txtSerToDate" runat="server" MaxLength="10" Style="width: 180px;
                                            height: 33px; font-family: Verdana; font-size: 13px; font-weight: normal;"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender1" runat="server" Enabled="True"
                                            TargetControlID="txtSerToDate" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                        font-size: 14px; font-weight: normal;">
                                        <span>RDD Project</span>
                                    </td>
                                    <td style="padding-top: 0.5%;">
                                        <asp:DropDownList ID="cbSerProject" runat="server" class="form-control" Style="width: 180px;
                                            height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                            <asp:ListItem>Select RDD Project </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                        font-size: 14px; font-weight: normal;">
                                        Customer Code
                                    </td>
                                    <td style="padding-top: 0.5%;">
                                        <asp:TextBox ID="txtSerCardCode" runat="server" MaxLength="254" class="form-control"
                                            Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;"
                                            Enabled="true">  </asp:TextBox>
                                    </td>
                                    <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                        font-size: 14px; font-weight: normal;">
                                        Customer Name
                                    </td>
                                    <td style="padding-top: 0.5%;">
                                        <asp:TextBox ID="txtSerCardName" runat="server" MaxLength="254" placeholder="Select Customer"
                                            class="form-control" Width="180px" Height="33px" Style="font-family: Raleway;
                                            font-size: 14px; font-weight: normal;">  </asp:TextBox>
                                    </td>
                                    <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                        font-size: 14px; font-weight: normal;">
                                        <span>Document Status</span>
                                    </td>
                                    <td style="padding-top: 0.5%;">
                                        <asp:DropDownList ID="cbSerDocStatus" runat="server" class="form-control" Style="width: 180px;
                                            height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                        font-size: 14px; font-weight: normal;">
                                        <span>Ref. Num</span>
                                    </td>
                                    <td style="padding-top: 0.5%;">
                                        <asp:TextBox ID="txtSerRefNum" runat="server" MaxLength="254" class="form-control"
                                            Width="180px" Height="33px" Style="font-family: Raleway; font-size: 14px; font-weight: normal;">  </asp:TextBox>
                                    </td>
                                    <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                        font-size: 14px; font-weight: normal;">
                                        <span>Sales Employee</span>
                                    </td>
                                    <td style="padding-top: 0.5%;">
                                        <asp:DropDownList ID="cbSerSalesEmp" runat="server" class="form-control" Style="width: 180px;
                                            height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                            <asp:ListItem>-No Sales Employee-</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 17%; padding-left: 3%; padding-top: 0.5%; font-family: Raleway;
                                        font-size: 14px; font-weight: normal;">
                                        <span>Approval Status</span>
                                    </td>
                                    <td style="padding-top: 0.5%;">
                                        <asp:DropDownList ID="cbSerApprStatus" runat="server" class="form-control" Style="width: 180px;
                                            height: 33px; font-family: Raleway; font-size: 14px; font-weight: normal;" TabIndex="0">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="padding-top: 0.5%; padding-left: 71.5%; width: 100%">
                                        <button id="btnClearFilter" type="button" style="font-weight: bold; font-family: Verdana;
                                            height: 33px; width: 180px;" class="btn btn-info">
                                            Clear Filter
                                        </button>
                                        <button id="btnApplyFilter" type="button" style="font-weight: bold; font-family: Verdana;
                                            height: 33px; width: 180px;" class="btn btn-search">
                                            Apply Filter
                                        </button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>
                <div class="modal-body" style="overflow: auto; height: 50%;">
                    <table id="classTable" class="table table-bordered" style="padding-left: 0.5%; padding-right: 0.5%;">
                        <thead>
                        </thead>
                        <tbody>
                            <tr style="background-color: #ffe6e6; font-weight: bold">
                                <td style="text-align: center;">
                                    #
                                </td>
                                <td style="text-align: center;">
                                    SRC
                                </td>
                                <td style="text-align: center;">
                                    SO_ID
                                </td>
                                <td style="text-align: center;">
                                    Ref. No.
                                </td>
                                <td>
                                    SAP DocNum
                                </td>
                                <td>
                                    Posting Date
                                </td>
                                <td>
                                    CardCode
                                </td>
                                <td>
                                    CardName
                                </td>
                                <td>
                                    Project
                                </td>
                                <td>
                                    Business Type
                                </td>
                                <td>
                                    Tax
                                </td>
                                <td>
                                    Total
                                </td>
                                <td>
                                    Status
                                </td>
                                <td>
                                    GP & GP(%)
                                </td>
                                <td>
                                    Approval Status
                                </td>
                                <td>
                                    Approver
                                </td>
                                <td>
                                    Remark
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">
                        Close
                    </button>
                </div>
            </div>
        </div>
    </div>
    <a id="downloadfile" style="text-decoration: none; display: none;" target="_blank">Please
        Wait...</a>
    <div id="divLoader" style="visibility: hidden; position: fixed; text-align: center;
        height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000;
        opacity: 0.7;">
        <span style="border-width: 0px; position: fixed; padding: 20px; background-color: #FFFFFF;
            font-size: 30px; left: 40%; top: 40%; border-radius: 50px;">Please wait ...</span>
    </div>
    <%--<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable id="lblformName" runat="server" style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Sales Order  </Lable>
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
</table>--%>
</asp:Content>
