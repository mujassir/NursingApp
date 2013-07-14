<%@ Page Language="C#" MasterPageFile="~/Administrator/PopupMasterPage.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="ReportTimeRN.aspx.cs" Inherits="RMC.Web.Administrator.WebForm6" Title="Monthly Summary Dashboard Report" %>

<%@ Register Src="~/UserControls/ReportTimeRN.ascx" TagName="ReportTimeRM" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportTimeRM ID="ReportTimeRM1" runat="server" />
</asp:Content>
