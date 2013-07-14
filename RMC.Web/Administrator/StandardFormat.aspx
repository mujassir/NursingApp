<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StandardFormat.aspx.cs"
    Inherits="RMC.Web.Administrator.StandardFormat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/ControlStyles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td align="center" style="color: #06569d; font-size: 12px; font-weight: bold;" colspan="2">
                    <%=MessageString%>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td align="right" style="color: #06569d; font-size: 11px; font-weight: bold;">
                    Configuration Name :
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropDownListConfigName" runat="server" CssClass="aspDropDownList"
                        ForeColor="#06569d" DataSourceID="LinqDataSourceConfigName" DataTextField="ConfigurationName"
                        DataValueField="ConfigurationID">
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="LinqDataSourceConfigName" runat="server" ContextTypeName="RMC.DataService.RMCDataContext"
                        Select="new (ConfigurationID, ConfigurationName)" TableName="DataImportConfigLocations">
                    </asp:LinqDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td></td>
                <td align="left">
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" CssClass="aspButton" OnClick="ButtonSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
