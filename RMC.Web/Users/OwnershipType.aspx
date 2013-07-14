<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="OwnershipType.aspx.cs" Inherits="RMC.Web.Users.OwnershipType" Title="RMC :: Ownership Type" %>
<%@ Register src="../UserControls/OwnerShipType.ascx" tagname="OwnerShipType" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:OwnerShipType ID="OwnerShipType1" runat="server" />

</asp:Content>
