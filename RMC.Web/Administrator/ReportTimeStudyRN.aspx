<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ReportTimeStudyRN.aspx.cs" Inherits="RMC.Web.Administrator.WebForm7"
    Title="Untitled Page" %>

<%@ Register Src="~/UserControls/ReportTimeStudyRN.ascx" TagName="ReportTimeStudyRN"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ReportTimeStudyRN ID="ReportTimeStudyRN1" runat="server" />
</asp:Content>
