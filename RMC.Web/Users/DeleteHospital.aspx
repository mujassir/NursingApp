<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="DeleteHospital.aspx.cs" Inherits="RMC.Web.Users.WebForm11" Title="Untitled Page" %>
<%@ Register src="../UserControls/DeleteHospital.ascx" tagname="DeleteHospital" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:DeleteHospital ID="DeleteHospital1" runat="server" />
</asp:Content>
