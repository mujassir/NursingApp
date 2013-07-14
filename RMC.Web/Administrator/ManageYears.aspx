<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageYears.aspx.cs" Inherits="RMC.Web.Administrator.ManageYears" Title="Untitled Page" %>
<%@ Register src="../UserControls/ManageYears.ascx" tagname="ManageYears" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:ManageYears ID="ManageYears1" runat="server" />

</asp:Content>
