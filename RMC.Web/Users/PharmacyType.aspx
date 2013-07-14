<%@ Page Title="RMC :: Pharmacy Type" Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="PharmacyType.aspx.cs" Inherits="RMC.Web.Users.PharmacyType" %>
<%@ Register src="../UserControls/PharmacyType.ascx" tagname="PharmacyType" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:PharmacyType ID="PharmacyType1" runat="server" />
</asp:Content>
