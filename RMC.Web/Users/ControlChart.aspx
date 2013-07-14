<%@ Page Language="C#" MasterPageFile="~/Users/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ControlChart.aspx.cs" Inherits="RMC.Web.Users.WebForm10" Title="RMC :: Control Charts" %>

<%@ Register src="../UserControls/ControlCharts.ascx" tagname="ControlCharts" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ControlCharts ID="ControlCharts1" runat="server" />
</asp:Content>
