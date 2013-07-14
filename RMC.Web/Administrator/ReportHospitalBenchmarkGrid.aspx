<%@ Page Language="C#" MasterPageFile="~/Administrator/ReportPopupMasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" 
    CodeBehind="ReportHospitalBenchmarkGrid.aspx.cs" Inherits="RMC.Web.Administrator.WebForm18" Title="Report Hospital Benchmark" %>

<%@ Register Src="../UserControls/ReportHospitalBenchmarkGrid.ascx" TagName="ReportHospitalBenchmarkGrid"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportHospitalBenchmarkGrid ID="ReportHospitalBenchmarkGrid1" runat="server" />
</asp:Content>
