<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dv.aspx.cs" Inherits="RMC.Web.dv"
    EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="UserControls/DataManagementFileList.ascx" TagName="DataManagementFileList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

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

</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="Gridview1" runat="server" OnRowDataBound="Gridview1_RowDataBound"
        HorizontalAlign="Center" BackColor="Black">
        <HeaderStyle BackColor="#ffff80" HorizontalAlign="Center" VerticalAlign="Middle" />
        <RowStyle BackColor="#80ffff" HorizontalAlign="Center" VerticalAlign="Middle" />
    </asp:GridView>
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">ExportToExcel</asp:LinkButton>
    <div id="divChart">
        <asp:Chart ID="ChartControlCharts" runat="server" BackColor="226, 205, 214" BorderColor="#1A3B69"
            BackSecondaryColor="White" BackGradientStyle="TopBottom" Width="765px" Height="450px">
            <Titles>
                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 11pt, style=Bold" ShadowOffset="3"
                    Text="Line Chart" Name="Title1" ForeColor="26, 59, 105">
                </asp:Title>
            </Titles>
            <Legends>
                <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                    Alignment="Center" Docking="Bottom" LegendStyle="Row">
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
    </form>
</body>
</html>
