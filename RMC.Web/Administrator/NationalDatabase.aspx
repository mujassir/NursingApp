<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
 CodeBehind="NationalDatabase.aspx.cs" Inherits="RMC.Web.Administrator.WebForm10" Title="Manage National Database" %>
<%@ Register src="../UserControls/NationalDatabase.ascx" tagname="NationalDatabase" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:NationalDatabase ID="NationalDatabase1" runat="server" />
</asp:Content>
