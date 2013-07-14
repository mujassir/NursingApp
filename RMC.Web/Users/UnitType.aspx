<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="UnitType.aspx.cs" Inherits="RMC.Web.Users.UnitType" Title="RMC :: Unit Type" %>
<%@ Register src="../UserControls/UnitType.ascx" tagname="UnitType" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:UnitType ID="UnitType1" runat="server" />

</asp:Content>
