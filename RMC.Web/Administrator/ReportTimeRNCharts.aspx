<%@ Page Language="C#" MasterPageFile="~/Administrator/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ReportTimeRNCharts.aspx.cs" Inherits="RMC.Web.Administrator.WebForm8"
    Title="" %>

<%@ Register Src="~/UserControls/ReportTimeRNCharts.ascx" TagName="ReportTimeRNCharts"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportTimeRNCharts ID="ReportTimeRNCharts1" runat="server" />
</asp:Content>
