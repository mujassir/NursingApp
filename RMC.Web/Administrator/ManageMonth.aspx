<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageMonth.aspx.cs" Inherits="RMC.Web.Administrator.ManageMonth" Title="Untitled Page" %>
<%@ Register src="../UserControls/ManageMonth.ascx" tagname="ManageMonth" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:ManageMonth ID="ManageMonth1" runat="server" />

</asp:Content>
