<%@ Page Language="C#" MasterPageFile="~/Users/ReportPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="ReportLocationProfileGrid.aspx.cs" Inherits="RMC.Web.Users.ReportLocationProfileGrid" Title="From/To Trips Report" %>
<%@ Register Src="../UserControls/ReportLocationProfileGrid.ascx" TagName="ReportLocationProfileGrid"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:ReportLocationProfileGrid ID="ReportLocationProfileGrid1" runat="server" />

</asp:Content>
