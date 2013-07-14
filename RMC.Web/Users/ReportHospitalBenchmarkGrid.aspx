<%@ Page Language="C#" MasterPageFile="~/Users/ReportPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="ReportHospitalBenchmarkGrid.aspx.cs" Inherits="RMC.Web.Users.WebForm8" Title="Untitled Page" %>
<%@ Register src="../UserControls/ReportHospitalBenchmarkGrid.ascx" tagname="ReportHospitalBenchmarkGrid" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportHospitalBenchmarkGrid ID="ReportHospitalBenchmarkGrid1" 
        runat="server" />
</asp:Content>
