<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddUpdateConfig.aspx.cs" Inherits="RMC.Web.Administrator.AddUpdateConfig" Title="Untitled Page" %>
<%@ Register src="../UserControls/AddOrUpdateConfiguration.ascx" tagname="AddOrUpdateConfiguration" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AddOrUpdateConfiguration ID="AddOrUpdateConfiguration1" runat="server" />
</asp:Content>
