<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="SalesOrderList.aspx.cs" Inherits="IntranetNew_Orders_SalesOrderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://code.jquery.com/jquery-3.3.1.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../Orders/js/DBList.js" type="text/javascript"></script>
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
        <span>Userwsie DataBase SetUp</span>
    </div>
    <div style="border: 1px solid red;">
        <div>
            <fieldset>
                <div id="forecast1" style="margin-top: 10px; margin-left: 15px; margin-bottom: 10px;
                    width: 98%">
                    <div class="box-body table-responsive table-container" style="margin-top: 10px;">
                        <div id="example2_wrapper" class="dataTables_wrapper form-inline dt-bootstrap no-footer">
                            <div class="col-sm-12" style="overflow: auto; height: 175px;">
                                <table class="table table-bordered table-striped dataTable no-footer " id="gvDB"
                                    role="grid">
                                    <thead class="thead">
                                        <tr class="header-bg" role="row" style="background-color: #cc0000; color: white;">
                                            <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                style="color: White;">
                                                #
                                            </th>
                                            <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                style="color: White;">
                                                User Code
                                            </th>
                                            <%--<th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                style="color: White;">
                                                User Name
                                            </th>
                                            <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                style="color: White;">
                                                Default DB
                                            </th>--%>
                                            <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                style="color: White;">
                                                SAPKE
                                                
                                            </th>
                                            <%--<th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                style="color: White;">
                                                SAPTZ
                                            </th>
                                            <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                style="color: White;">
                                                SAPZM
                                            </th>
                                            <th class="hide" tabindex="0" aria-controls="example2" rowspan="1" colspan="1" style="color: White;">
                                                SAPUG
                                            </th>
                                            <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                style="color: White;">
                                                SAPAE
                                            </th>
                                            <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                style="color: White;">
                                                SAPTRI
                                            </th>
                                            <th class="sorting" tabindex="0" aria-controls="example2" rowspan="1" colspan="1"
                                                style="color: White;">
                                                SAPMAL
                                            </th>--%>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
