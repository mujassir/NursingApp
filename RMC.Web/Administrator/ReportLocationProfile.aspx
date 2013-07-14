<%@ Page Language="C#" MasterPageFile="~/Administrator/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ReportLocationProfile.aspx.cs" Inherits="RMC.Web.Administrator.WebForm16"
    Title="Location Profile Report" %>

<%@ Register Src="../UserControls/ReportLocationProfile.ascx" TagName="ReportLocationProfile"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportLocationProfile ID="ReportLocationProfile1" runat="server" />
</asp:Content>
