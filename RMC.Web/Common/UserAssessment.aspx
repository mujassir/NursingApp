<%@ Page Title="Unit Assessment Report" Language="C#" AspCompat="true" MasterPageFile="~/Administrator/ReportPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="UserAssessment.aspx.cs" Inherits="RMC.Web.Administrator.UserAssessment" %>
<%@ Register Src="../UserControls/ReportHospitalBenchmarkGrid.ascx" TagName="ReportHospitalBenchmarkGrid"
    TagPrefix="uc1" %>
<%@ Register src="../UserControls/UserAssessmentReport.ascx" tagname="UserAssessmentReport" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc2:UserAssessmentReport ID="UserAssessmentReport1" runat="server" />
  
     
</asp:Content>
