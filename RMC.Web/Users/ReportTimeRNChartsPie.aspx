<%@ Page Language="C#" MasterPageFile="~/Users/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ReportTimeRNChartsPie.aspx.cs" Inherits="RMC.Web.Users.WebForm6"
    Title="" %>

<%@ Register Src="../UserControls/ReportTimeRNChartsPie12.ascx" TagName="ReportTimeRNChartsPie12"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportTimeRNChartsPie12 ID="ReportTimeRNChartsPie1" runat="server" />
</asp:Content>
