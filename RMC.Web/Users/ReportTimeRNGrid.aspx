<%@ Page Language="C#" MasterPageFile="~/Users/ReportPopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ReportTimeRNGrid.aspx.cs" Inherits="RMC.Web.Users.WebForm9" Title="Monthly Summary Dashboard Report" %>

<%@ Register Src="../UserControls/ReportTimeRNGrid.ascx" TagName="ReportTimeRNGrid"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportTimeRNGrid ID="ReportTimeRNGrid1" runat="server" />
</asp:Content>
