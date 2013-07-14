<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportTimeRNCharts.ascx.cs"
    Inherits="RMC.Web.UserControls.ReportTimeRNCharts" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .Background
    {
        position: fixed;
        left: 0;
        top: 0;
        z-index: 10;
        width: 100%;
        height: 100%;
        filter: alpha(opacity=60);
        opacity: 0.6;
        background-color: Black;
        visibility: hidden;
    }
</style>

<script type="text/javascript">
    function pageLoad() {

        var manager = Sys.WebForms.PageRequestManager.getInstance();

        manager.add_endRequest(endRequest);

        manager.add_beginRequest(onBeginRequest);

    }

    function onBeginRequest(sender, args) {

        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = 'Background';
        divParent.style.visibility = 'visible';
        divImage.style.visibility = 'visible';

    }

    function endRequest(sender, args) {

        var divParent = $get('ParentDiv');
        var divImage = $get('IMGDIV');

        divParent.className = '';
        divParent.style.visibility = 'hidden';
        divImage.style.visibility = 'hidden';
    }
</script>

<script language="javascript" type="text/javascript">
    function saveImageAs(imgOrURL) {
        if (typeof imgOrURL == 'object')
            imgOrURL = imgOrURL.src;
        window.win = open(imgOrURL);
        setTimeout('win.document.execCommand("SaveAs")', 500);
    }

    function msg() {

        var imgOrURL = document.getElementById('embedImage');
        saveImageAs(imgOrURL);
    }

    function copyToClipboard() {

        var div = document.getElementById('divChart');
        div.contentEditable = 'true';
        var controlRange;
        if (document.body.createControlRange) {
            controlRange = document.body.createControlRange();
            controlRange.addElement(div);
            controlRange.execCommand('Copy');
        }
        div.contentEditable = 'false';
    }
</script>

<table width="99%" visible="false">
    <tr>
        <th align="left" style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
            <%--<u>Time RN Summary</u>--%>
            <asp:Label ID="LabelHeading" runat="server" Text="" Font-Underline="true"></asp:Label>
        </th>
        <th>
        </th>
        <th>
        </th>
        <th align="right" style="padding-left: 50px; padding-top: 10px; text-align: right;">
            <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                TabIndex="6" OnClick="ImageButtonBack_Click" />
        </th>
    </tr>
    <tr>
        <td style="height: 10px;">
        </td>
    </tr>
</table>
<asp:MultiView ID="MultiViewCharts" runat="server">
    <asp:View ID="ViewPieCharts" runat="server">
    </asp:View>
    <asp:View ID="ViewControlCharts" runat="server">
        <table>
            <tr>
                <td align="left" style="padding-left: 12px;">
                    <asp:LinkButton ID="LinkButtonSaveImage" Font-Bold="true" Font-Size="11px" runat="server"
                        OnClick="LinkButtonSaveImage_Click">SaveImage</asp:LinkButton>
                </td>
                <td align="right" style="padding-right: 20px;">
                    <%--<input name="Salva Imagine" type="button" onclick="copyToClipboard();" value="CopyImage" />--%>
                    <a href="#" onclick="copyToClipboard(); return false" style="font-size: 11px; font-weight: bold;">
                        CopyImage</a>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="divChart">
                        <asp:Chart ID="ChartControlCharts" runat="server" BackColor="226, 205, 214" BorderColor="#1A3B69"
                            BackSecondaryColor="White" BackGradientStyle="TopBottom" Width="765px" 
                            Height="450px">
                            <Titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 11pt, style=Bold" ShadowOffset="3"
                                    Text="Line Chart" Name="Title1" ForeColor="26, 59, 105">
                                </asp:Title>
                            </Titles>
                            <Legends>
                                <asp:Legend IsTextAutoFit="true" Name="Default" BackColor="Transparent" 
                                    Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Center" Docking="Bottom" 
                                    LegendStyle="Row">
                                </asp:Legend>
                            </Legends>
                            <BorderSkin SkinStyle="Emboss" />
                            <Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" Area3DStyle-Rotation="10">
                                    <Area3DStyle Rotation="10"></Area3DStyle>
                                    <AxisY LineColor="64, 64, 64, 64">
                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </AxisY>
                                    <AxisX LineColor="64, 64, 64, 64" IntervalAutoMode="VariableCount" Enabled="True"
                                        Interval="1">
                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 20px;">
                    <table>
                        <tr>
                            <td style="width: 105px;">
                            </td>
                            <td>
                                <asp:Panel ID="PanelMarker" runat="server">
                                    <fieldset>
                                        <legend style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                                            Marker</legend>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelMarkerStyle" runat="server" Text="Marker Style" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListMarkerStyle" runat="server" ForeColor="#06569D"
                                                        CssClass="aspDropDownList" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceMarkerStyle"
                                                        DataTextField="Key" DataValueField="Value" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMarkerStyle_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelMarkerSize" runat="server" Text="Marker Size" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListMarkerSize" runat="server" ForeColor="#06569D"
                                                        CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMarkerSize_SelectedIndexChanged">
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelMarkerColor" runat="server" Text="Marker Color" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListMarkerColor" runat="server" ForeColor="#06569D"
                                                        CssClass="aspDropDownList" AppendDataBoundItems="True" DataSourceID="ObjectDataSourceColor"
                                                        DataTextField="Key" DataValueField="Value" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMarkerColor_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelPointLabel" runat="server" Text="Point Label" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListPointLabel" runat="server" ForeColor="#06569D"
                                                        CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPointLabel_SelectedIndexChanged">
                                                        <asp:ListItem Value="None" Selected="True">None</asp:ListItem>
                                                        <asp:ListItem Value="Auto">Auto</asp:ListItem>
                                                        <asp:ListItem Value="TopLeft">TopLeft</asp:ListItem>
                                                        <asp:ListItem Value="Top">Top</asp:ListItem>
                                                        <asp:ListItem Value="TopRight">TopRight</asp:ListItem>
                                                        <asp:ListItem Value="Right">Right</asp:ListItem>
                                                        <asp:ListItem Value="BottomRight">BottomRight</asp:ListItem>
                                                        <asp:ListItem Value="Bottom">Bottom</asp:ListItem>
                                                        <asp:ListItem Value="BottomLeft">BottomLeft</asp:ListItem>
                                                        <asp:ListItem Value="Left">Left</asp:ListItem>
                                                        <asp:ListItem Value="Center">Center</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                            </td>
                            <td>
                                <asp:Panel ID="PanelChart" runat="server">
                                    <fieldset>
                                        <legend style="font-size: 14px; padding-left: 10px; padding-top: 10px; color: #06569d;">
                                            Chart</legend>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelChartType" runat="server" Text="Chart Type" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListChartType" runat="server" ForeColor="#06569D" CssClass="aspDropDownList"
                                                        AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListChartType_SelectedIndexChanged">
                                                        <asp:ListItem Value="Line">Line</asp:ListItem>
                                                        <asp:ListItem Value="Spline">Spline</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelLineWidth" runat="server" Text="Line Width" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListLineWidth" runat="server" ForeColor="#06569D" CssClass="aspDropDownList"
                                                        AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListLineWidth_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelColor" runat="server" Text="Color" ForeColor="#06569d" Font-Bold="true"
                                                        Font-Size="11px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListColor" runat="server" ForeColor="#06569D" CssClass="aspDropDownList"
                                                        AppendDataBoundItems="True" DataSourceID="ObjectDataSourceColor" DataTextField="Key"
                                                        DataValueField="Value" AutoPostBack="True" OnSelectedIndexChanged="DropDownListColor_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LabelShadowOffset" runat="server" Text="Shadow Offset" ForeColor="#06569d"
                                                        Font-Bold="true" Font-Size="11px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownListShadowOffset" runat="server" ForeColor="#06569D"
                                                        CssClass="aspDropDownList" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownListShadowOffset_SelectedIndexChanged">
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>
<asp:ObjectDataSource ID="ObjectDataSourceColor" runat="server" SelectMethod="GetColor"
    TypeName="RMC.BussinessService.BSChartControl"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceMarkerStyle" runat="server" SelectMethod="GetMarkerStyle"
    TypeName="RMC.BussinessService.BSChartControl"></asp:ObjectDataSource>
<div id="ParentDiv" class="Background" style="visibility: hidden;">
    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
        z-index: 40;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
            AlternateText="Loading"></asp:Image>
    </div>
</div>
