<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ReportTimeRNCharts.aspx.cs" Inherits="RMC.Web.Users.WebForm5" Title="" %>

<%@ Register Src="../UserControls/ReportTimeRNCharts.ascx" TagName="ReportTimeRNCharts"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportTimeRNCharts ID="ReportTimeRNCharts1" runat="server" />
</asp:Content>
