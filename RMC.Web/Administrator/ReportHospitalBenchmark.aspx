<%@ Page Language="C#" MasterPageFile="~/Administrator/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ReportHospitalBenchmark.aspx.cs" Inherits="RMC.Web.Administrator.WebForm4"
    Title="Hospital Benchmark Report" %>

<%@ Register Src="~/UserControls/ReportHospitalBenchmark.ascx" TagName="ReportHospitalBenchmark"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportHospitalBenchmark ID="ReportHospitalBenchmark1" runat="server" />
</asp:Content>
