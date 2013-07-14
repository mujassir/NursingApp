<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="DataManagementYear.aspx.cs" Inherits="RMC.Web.Users.DataManagementYear" Title="Untitled Page" %>
<%@ Register src="../UserControls/DataManamentYear.ascx" tagname="DataManamentYear" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:DataManamentYear ID="DataManamentYear1" runat="server" />

</asp:Content>
