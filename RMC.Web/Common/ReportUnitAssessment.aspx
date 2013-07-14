<%@ Page Title="" Language="C#" MasterPageFile="~/Users/ReportPopupMasterPage.Master" AutoEventWireup="true" CodeBehind="ReportUnitAssessment.aspx.cs" Inherits="RMC.Web.Users.ReportUnitAssessment" %>
<%@ Register src="../UserControls/UnitAssessmentReport.ascx" tagname="UnitAssessmentReport" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:UnitAssessmentReport ID="UnitAssessmentReport1" runat="server" />
</asp:Content>
