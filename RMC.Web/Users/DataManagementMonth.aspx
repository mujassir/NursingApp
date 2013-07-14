<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="DataManagementMonth.aspx.cs" Inherits="RMC.Web.Users.DataManagementMonth" Title="Untitled Page" %>
<%@ Register src="../UserControls/DataManagementMonth.ascx" tagname="DataManagementMonth" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:DataManagementMonth ID="DataManagementMonth1" runat="server" />
</asp:Content>
