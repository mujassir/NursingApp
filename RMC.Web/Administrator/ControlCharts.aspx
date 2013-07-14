<%@ Page Language="C#" MasterPageFile="~/Administrator/PopupMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ControlCharts.aspx.cs" Inherits="RMC.Web.Administrator.WebForm14"
    Title="RMC :: Run Charts" %>

<%@ Register Src="../UserControls/ControlCharts.ascx" TagName="ControlCharts" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ControlCharts ID="ControlCharts1" runat="server" />
</asp:Content>
