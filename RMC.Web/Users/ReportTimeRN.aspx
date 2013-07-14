<%@ Page Language="C#" MasterPageFile="~/Users/PopupMasterPage.Master" AutoEventWireup="true"  EnableEventValidation="false"
    CodeBehind="ReportTimeRN.aspx.cs" Inherits="RMC.Web.Users.WebForm4" Title="Time Study RN Report" %>

<%@ Register Src="~/UserControls/ReportTimeRN.ascx" TagName="ReportTimeRN" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportTimeRN ID="ReportTimeRN1" runat="server" />
</asp:Content>
