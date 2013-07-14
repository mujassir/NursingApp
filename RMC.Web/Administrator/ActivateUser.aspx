<%@ Page Title="RMC :: Activate User" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ActivateUser.aspx.cs" Inherits="RMC.Web.Administrator.ActivateUser" %>
<%@ Register src="../UserControls/ActivateUser.ascx" tagname="ActivateUser" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ActivateUser ID="ActivateUser1" runat="server" />
</asp:Content>
