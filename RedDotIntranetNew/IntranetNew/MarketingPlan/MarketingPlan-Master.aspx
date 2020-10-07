<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="MarketingPlan-Master.aspx.cs" Inherits="IntranetNew_MarketingPlan_MarketingPlan_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">


        function AddNewRow() {

            var rownum = 1;
            var div = document.createElement("div");
            var divid = "dv" + rownum;
            div.setAttribute("ID", divid);
            rownum++;

            var _upload = document.createElement("input");
            _upload.setAttribute("type", "file");
            _upload.setAttribute("ID", "upload" + rownum);
            _upload.setAttribute("runat", "server");
            _upload.setAttribute("name", "uploads" + rownum);
            rownum++;

            var hyp = document.createElement("a");
            hyp.setAttribute("style", "cursor:Pointer");
            hyp.setAttribute("onclick", "return RemoveDv('" + divid + "');");
            hyp.innerHTML = " Remove";
            rownum++;

            //var br=document.createElement("br");

            var _pdiv = document.getElementById("divFileUploads");

         
            div.appendChild(_upload);
            div.appendChild(hyp);
            _pdiv.appendChild(div);
        }

        function RemoveDv(obj) {
            var p = document.getElementById("divFileUploads");
            var chld = document.getElementById(obj);
            p.removeChild(chld);

        }
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


        function Validate() {

            var ddlCountry = document.getElementById('<%= ddlCountry.ClientID %>').value;
            if (ddlCountry == "--SELECT--") {
                alert('Please select country');
                return false;
            }

            var ddlsourcefd = document.getElementById('<%= ddlsourcefd.ClientID %>').value;
            if (ddlsourcefd == "--SELECT--") {
                alert('Please select Source Of Fund');
                return false;
            }
            var ddlBU = document.getElementById('<%= ddlBU.ClientID %>').value;
            if (ddlBU == "--SELECT--") {
                alert('Please select BU');
                return false;
            }

            var ddlplanstatus = document.getElementById('<%= ddlplanstatus.ClientID %>').value;
            if (ddlplanstatus == "--SELECT--") {
                alert('Please select Plan Status');
                return false;
            }


            var txtappamount = document.getElementById('<%= txtappamount.ClientID %>').value;
            if (txtappamount.trim() == "" || txtappamount.trim() == null) {
                alert('Please enter AppAmount');
                document.getElementById('<%= txtappamount.ClientID %>').focus();
                return false;
            }

            var txtrddappamt = document.getElementById('<%= txtrddappamt.ClientID %>').value;
            if (txtrddappamt.trim() == "" || txtrddappamt.trim() == null) {
                alert('Please enter RDD AppAmount');
                document.getElementById('<%= txtrddappamt.ClientID %>').focus();
                return false;
            }


            var txtstartdate = document.getElementById('<%= txtstartdate.ClientID %>').value;
            if (txtstartdate.trim() == "" || txtstartdate.trim() == null) {
                alert('Please enter Start Date');
                document.getElementById('<%= txtstartdate.ClientID %>').focus();
                return false;
            }

            var txtEndDate = document.getElementById('<%= txtEndDate.ClientID %>').value;
            if (txtEndDate.trim() == "" || txtEndDate.trim() == null) {
                alert('Please enter End Date');
                document.getElementById('<%= txtEndDate.ClientID %>').focus();
                return false;
            }

            var txtdesc = document.getElementById('<%= txtdesc.ClientID %>').value;
            if (txtdesc.trim() == "" || txtdesc.trim() == null) {
                alert('Please enter Description');
                document.getElementById('<%= txtdesc.ClientID %>').focus();
                return false;
            }

            var stdate = new Date(txtstartdate);
            var endDt = new Date(txtEndDate);

            if (stdate > endDt) {
                alert('EndDate Should be Greater Than Startdate');
                document.getElementById('<%= txtstartdate.ClientID %>').focus();
                return false;
            }


        }

        function newreseller() {
          
            document.getElementById("Button").style.visibility = "hidden";
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.3.min.js"></script>
    <%--<asp:UpdatePanel ID="UPManualCLStatusChangeAlert" runat="server">
        <ContentTemplate>--%>
            <table width="95%" align="center" cellpadding="3px" cellspacing="3px">
                <tr>
                    <td width="100%" align="center">
                        <lable style="color: #d71313; font-size: x-large; font-weight: bold; font-family: Raleway;"> &nbsp;&nbsp;&nbsp; Marketing Master </lable>
                    </td>
                </tr>
                <%--<tr>
            <td width="50%">
                &nbsp;
            </td>
            <td width="50%">
                &nbsp;
            </td>
        </tr>
                --%>
                <tr>
                    <td width="100%" align="center">
                        <asp:Label ID="lblMsg" runat="server" Style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;
                            font-family: Raleway; font-size: 14px; color: Red; font-weight: bold;" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="100%" align="center">
                        <asp:Panel ID="pnlForms" runat="server" Width="100%" Height="25%" BorderWidth="1px"
                            BorderColor="Red" EnableTheming="true">
                            <table width="100%" align="center" cellpadding="3px"
                                cellspacing="3px">
                                <tr>
                                    <td width="15%">
                                        &nbsp;
                                    </td>
                                    <td width="18%">
                                        &nbsp;
                                    </td>
                                    <td width="15%">
                                        &nbsp;
                                    </td>
                                    <td width="18%">
                                        &nbsp;
                                    </td>
                                    <td width="15%">
                                        &nbsp;
                                    </td>
                                    <td width="18%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%">
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Source Of Fund &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:DropDownList ID="ddlsourcefd" TabIndex="1"  runat="server"
                                            Style="width: 80%" class="form-control">
                                            <asp:ListItem>--SELECT--</asp:ListItem>
                                            <asp:ListItem>Internal</asp:ListItem>
                                            <asp:ListItem>Vendor</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td width="15%">
                                         <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Vendor/BU &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:DropDownList ID="ddlBU" runat="server" class="form-control"
                                            TabIndex="2" Style="width: 80%">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="15%">
                                        <label>
                                            Country &nbsp;
                                        </label>
                                    </td>
                                    <td width="18%">
                                 
                                        <asp:DropDownList ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                            Style="width: 80%" runat="server" class="form-control" TabIndex="3">
                                        </asp:DropDownList>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%">  
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Approved Amount &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:TextBox ID="txtappamount" runat="server" autocomplete="off" class="form-control"
                                            onkeypress="javascript:return isNumberKey(event);" TabIndex="4" Style="width: 80%"
                                            AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td align="left" width="5%">
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Balance From Approved &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:TextBox ID="txtbalfromapp" runat="server" autocomplete="off" class="form-control"
                                            Text="0" AutoPostBack="true" Style="width: 80%"></asp:TextBox>
                                        <%-- <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server"
                                    TargetControlID="txtcreateDate" Format="MM/dd/yyyy">
                                </cc1:CalendarExtender>--%>
                                    </td>
                                    <td width="15%">
                                         <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Ref.No&nbsp; </label>  
                                        <%--<asp:Label ID="lblrefNO" runat="server" BorderColor="Black" Font-Bold="true">Ref.No&nbsp;</asp:Label>--%>
                                    </td>
                                    <td width="18%">
                                        <asp:TextBox ID="txtrefno" runat="server" class="form-control" Style="width: 80%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%">
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >RDD Approved Amount &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:TextBox ID="txtrddappamt" runat="server" Style="width: 80%" autocomplete="off"
                                            class="form-control" onkeypress="javascript:return isNumberKey(event);" TabIndex="5"
                                            Enabled="false" Text="0" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td width="15%">
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Bal Amt from RDD apprvd&nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:TextBox ID="txtrddBalAmt" runat="server" class="form-control" Text="0" autocomplete="off"
                                            Style="width: 80%"></asp:TextBox>
                                    </td>
                                    <td width="15%">
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Plan Status &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:DropDownList ID="ddlplanstatus"  runat="server" class="form-control"
                                            TabIndex="6" Style="width: 80%">
                                            <asp:ListItem Value="0">Draft</asp:ListItem>
                                            <asp:ListItem Value="1">Open</asp:ListItem>
                                            <asp:ListItem Value="2">On Hold</asp:ListItem>
                                            <asp:ListItem Value="3">Closed Unpaid</asp:ListItem>
                                            <asp:ListItem Value="4">Closed Paid</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%">
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Description &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:TextBox ID="txtdesc" runat="server" autocomplete="off" class="form-control"
                                            TabIndex="7" TextMode="MultiLine" Style="width: 80%"></asp:TextBox>
                                    </td>
                                    <td width="15%">
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Start Date(MM/dd/YYYY) &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:TextBox ID="txtstartdate" runat="server" autocomplete="off" class="form-control"
                                            TabIndex="8" Style="width:80%"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgPopup" runat="server"
                                            TargetControlID="txtstartdate" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td width="15%">
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >End Date(MM/dd/YYYY) &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:TextBox ID="txtEndDate" runat="server" Style="width: 80%" autocomplete="off"
                                            class="form-control" TabIndex="9"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server"
                                            TargetControlID="txtEndDate" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%">
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Approval Status &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:DropDownList ID="ddlappstatus" runat="server" Style="width: 80%" class="form-control">
                                            <asp:ListItem>Pending</asp:ListItem>
                                            <asp:ListItem>Approved</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>
                                            <%--  <asp:ListItem>Draft</asp:ListItem>
                                            --%>
                                        </asp:DropDownList>
                                    </td>
                                    <td width="15%">
                                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Approver Remark &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:TextBox ID="txtapprmk" runat="server" Style="width: 80%" class="form-control"
                                            autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td width="15%">
                                            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  >Create Date  &nbsp; </label>  
                                    </td>
                                    <td width="18%">
                                        <asp:Label ID="lbltodaydate" runat="server" Text="" Enabled="false"></asp:Label>
                                        <%--<asp:TextBox ID="txtcreateDate" runat="server" Style="width: 65%; padding: 5px 12px;
                                    margin: 3px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;" autocomplete="off" class="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtcreateDate_CalendarExtender1" PopupButtonID="imgPopup"
                                    runat="server" TargetControlID="txtcreateDate" Format="MM/dd/yyyy">
                                </cc1:CalendarExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                   
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="6">
                                        &nbsp;&nbsp;
                                        <asp:Button ID="BtnSave" runat="server" Text="Save" Style="width: 20%; padding: 5px 12px;
                                            margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;"
                                            OnClick="BtnSave_Click" TabIndex="9999" OnClientClick="return Validate();" class="btn btn-success" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="BtnCancel" runat="server" Text="Back To List" Style="width: 20%;
                                            padding: 5px 12px; margin: 4px 0; box-sizing: border-box; font-family: Raleway;
                                            font-size: 14px;" OnClick="BtnCancel_Click" class="btn btn-primary" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnaddow" runat="server" Text="Add Row" OnClick="ButtonAdd_Click"
                                            Style="width: 20%; padding: 5px 12px; margin: 4px 0; box-sizing: border-box;
                                            font-family: Raleway; font-size: 14px;" OnClientClick="return Validate();" class="btn btn-info"
                                            TabIndex="9999" />
                                    </td>  
                                 
                                    <asp:HiddenField ID="hdnplanid" runat="server" Visible="false" />
                                </tr>  
                            </table> <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup" runat="server"
                                            TargetControlID="TextBox1" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>  
                           
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="100%" align="center">
                        <%--   <asp:Panel ID="pnlFormList" runat="server" Width="85%"  Height="50%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">--%>
                        <asp:GridView ID="GvPlan" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            OnRowDataBound="GvPlan_RowDataBound" Width="100%" Height="120%" CellPadding="4"
                            ForeColor="#333333" GridLines="None" OnPageIndexChanging="GvPlan_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                        <asp:Label ID="lblplanlineid" runat="server" Text='<%#Eval("PlanLineId") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdate" runat="server" autocomplete="off" class="form-control"
                                            Text='<%#Eval("ActivityDate","{0:d}")  %>'></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server"
                                            TargetControlID="txtgvdate" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Country">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlgvcountry" runat="server" autocomplete="off" class="form-control"
                                          >  <%--AutoPostBack="true"--%>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblgrvCountry" runat="server" Text='<%#Eval("CountryName") %>' Visible="false"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvvendor" runat="server" autocomplete="off" class="form-control"
                                            Text='<%#Eval("Vendor") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdesc" runat="server" autocomplete="off" class="form-control"
                                            Text='<%#Eval("Description") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvamt" runat="server" OnTextChanged="txtgvamt_TextChanged" autocomplete="off"
                                            class="form-control" Text='<%#Eval("Amount") %>' onkeypress="javascript:return isNumberKey(event);"
                                            AutoPostBack="true"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtgvamt"
                                    ForeColor="Red" ErrorMessage="Amount Field can't be blanked"></asp:RequiredFieldValidator>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Vender Po.No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtpono" runat="server" autocomplete="off" class="form-control"
                                            Text='<%#Eval("VenderPoNo") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:TemplateField>
                                <%--      <asp:BoundField DataField="RowNumber" HeaderText="Sr.No" />--%>
                                <%--  <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>' ></asp:Label>
                                --%>
                                <asp:TemplateField ItemStyle-Width="10%" HeaderText="Sap Po.No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtsappono" runat="server" autocomplete="off" class="form-control"
                                            Text='<%#Eval("SAPPONo") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlstatus1" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="Status1_SelectedIndexChanged">
                                            <asp:ListItem>Planned</asp:ListItem>
                                            <asp:ListItem>Closed</asp:ListItem>
                                     
                                        </asp:DropDownList>
                                        <asp:Label ID="lblstatus1" runat="server" Text='<%#Eval("Status1") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Appr Status">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlstatus" runat="server" class="form-control">
                                            <asp:ListItem>Pending</asp:ListItem>
                                            <asp:ListItem>Approved</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approver Remark">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAppremark" runat="server" autocomplete="off" class="form-control"
                                           Text='<%#Eval("ApproverRemark") %>'></asp:TextBox>
                                        <%--   <asp:Label ID="lblremark" runat="server" Text='<%#Eval("ApproverRemark") %>' Visible="false"></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Key">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKey" Text='<%#Eval("LineRefNo") %>' runat="server" autocomplete="off"
                                            class="form-control"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:BoundField DataField="RowNumber1" HeaderText="Key" />--%>
                                <%-- <asp:TemplateField HeaderText="Keyy">
                            <ItemTemplate>
                                <asp:TextBox ID="txtKey" runat="server" AutoPostBack="true"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDel" runat="server" Text="Delete" OnClick="btnDel_Click" CommandName="Delete"
                                            class="btn btn-danger" OnClientClick="return confirm('Are you sure you want delete');"
                                            TabIndex="17" />
                                    </ItemTemplate>



                                </asp:TemplateField>

                             <%--   <tr>
                        <td style="width:16%">Upload Actual Plan</td>
                        <td colspan="3">
                         <asp:FileUpload ID="FileUpload1" runat="server" />
                         &nbsp;<asp:Label ID="lblFile" runat="server" Text=""></asp:Label>
                        </td>
                       </tr>--%>
<%--
                         <asp:TemplateField HeaderText="Upload Actual Plan">
                                    <ItemTemplate>
                                   <asp:FileUpload ID="FileUpload1" runat="server" />
                     <asp:Label ID="lblFile" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                            </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                            <SortedDescendingHeaderStyle BackColor="#820000" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>


                    </td>
                </tr>
                <table width="100%">
                <tr>
                <td width="1%">
                
                </td>
                <td width="5%">
                
                </td>
                </tr>
                    <tr>

                           <td  width="1%"> <asp:FileUpload ID="fileUpload2" runat="server"  CssClass=""  /> <div id="divFileUploads">
                            </div></td> 
                         &nbsp; &nbsp; <td  width="5%"> <input type="button" onclick="AddNewRow(); return false;"  value="Attach More Files....." style="font-size: 12px"  />&nbsp;
                           &nbsp;
                            <asp:Button ID="Button2" Text="Cancel All" runat="server" Visible="false"  style="font-size: 12px" />

                           <asp:Label ID="lblFile" runat="server" Text="" Visible="false"></asp:Label> </td>
                       


                    </tr>
</table>
                        <tr>
                            <td style="width:16%">
                             <%--   Upload Actual Plan
                                <asp:FileUpload ID="FileUpload1" runat="server" />--%><%--<asp:Label ID="lblFile" runat="server" Text="" Visible="false"></asp:Label>--%>
                                <%--<asp:Button ID="Button1" runat="server" Text="Upload" 
                                    />--%>

                                    <asp:GridView ID="GridFiles" runat="server" 
                        AutoGenerateColumns="False" BorderColor="White" ShowHeader="False" 
                        BorderStyle="None" BorderWidth="0px" GridLines="None" >

                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                 <asp:Label ID="lbldocid" runat="server" Text='<%#Eval("DocFile_Id") %>' Visible="false"></asp:Label>&nbsp;
                                   <b><asp:Label ID="lblstAttachedFiles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AttachmentFile")%>'></asp:Label></b> &nbsp; &nbsp;
                                    <%--<asp:CheckBox ID="chklstAttachedFiles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AttachmentFile")%>' Visible="false"/> &nbsp;
                                    <asp:HyperLink ID="lnkFileLoc" Text="Download" runat="server"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkdel" runat="server" onclick="lnkdel_Click"  OnClientClick="return confirm('Are you sure you want delete');">Delete</asp:LinkButton>--%>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                              
                               <asp:TemplateField>
                                <ItemTemplate>                               
                                    <asp:CheckBox ID="chklstAttachedFiles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AttachmentFile")%>' Visible="false"/> &nbsp;
                                    <asp:HyperLink ID="lnkFileLoc" Text="Download" runat="server"></asp:HyperLink>
                                    <asp:LinkButton ID="lnkdel" runat="server" onclick="lnkdel_Click"  OnClientClick="return confirm('Are you sure you want delete');">Delete</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>

                            
                        </Columns>

                    </asp:GridView>
                              
                            </td>
                </tr>
                  

      
         </tr>
     
            </table>
     <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

