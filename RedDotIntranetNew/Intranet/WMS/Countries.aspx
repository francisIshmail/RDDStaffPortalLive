<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="Countries.aspx.cs" Inherits="Intranet_WMS_Countries" %>

<%@ Register Src="UserControl/WebUserControl.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="http://maps.google.com/maps?file=api&v=2&sensor=false&key=ABQIAAAAcBlCkeOPJin8k-qaQXzU7BQGBuCPWxdxlOb3ZaeDB3q0vlcH5BTABsi26yLfIbuiM1r2veUY3cXjEA"
        type="text/javascript"></script>
    <script type="text/javascript">

        var map = null;
        var geocoder = null;
        function initialize() {

            if (GBrowserIsCompatible()) {
                map = new GMap2(document.getElementById("map_canvas"));
                map.setCenter(new GLatLng(21.4419, 0), 1);
                geocoder = new GClientGeocoder();
            }
        }

        function getLatLng(point) {
            var matchll = /\(([-.\d]*), ([-.\d]*)/.exec(point);
            if (matchll) {
                var lat = parseFloat(matchll[1]);
                var lon = parseFloat(matchll[2]);
                lat = lat.toFixed(6);
                lon = lon.toFixed(6);

            } else {
                var message = "<b>Error extracting info from</b>:" + point + "";
                var messagRoboGEO = message;
            }

            return new GLatLng(lat, lon);
        }

        function searchPlace(place) {

            if (geocoder) {

                geocoder.getLatLng(place, function (point) {
                    alert(place);

                    if (!point) {
                        alert(place + " not found");
                    } else {

                        var latLng = getLatLng(point);
                        var info = "<h3>" + place + "</h3>Latitude: " + latLng.lat() + "  Longitude:" + latLng.lng();

                        var marker = new GMarker(point);
                        map.addOverlay(marker);
                        marker.openInfoWindowHtml(info);

                    }
                });
            }
        }
    </script>
    <table width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td>
                <uc1:WebUserControl ID="WebUserControl1" runat="server" />
            </td>
            <td>
                <table class="style1">
                    <tr>
                        <td style="text-align: center" class="style2" colspan="2">
                            <h4>
                                Countries of Origin
                            </h4>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Button ID="btncountry" runat="server" Text="Add New Country" OnClientClick="searchPlace(document.getElementById('ctl00_ContentPlaceHolder1_txtcountry').value)"
                                            OnClick="Button1_Click" />
                                    </td>
                                    <td>
                                        <table id="tbcountry" runat="server" visible="false">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Please enter the name of the country"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtcountry" runat="server" Width="228px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbmsg" runat="server" ForeColor="#FF3300" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:GridView ID="gvcontry" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                                            CellPadding="4" OnPageIndexChanging="gvcontry_PageIndexChanging" OnRowCommand="gvcontry_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Country Name ">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btncountry" runat="server" Text='<%# Bind("country_name") %>' BackColor="Transparent"
                                                            BorderColor="Transparent" ForeColor="Blue" OnClientClick="searchPlace(this.value)"
                                                            CommandName="do_id" CommandArgument="<%# Container.DataItemIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                            <RowStyle BackColor="White" ForeColor="#003399" />
                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                            <SortedDescendingHeaderStyle BackColor="#002876" />
                                        </asp:GridView>
                                    </td>
                                    <td>
                                        <div id="map_canvas" style="width: 500px; height: 350px">
                                        </div>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
