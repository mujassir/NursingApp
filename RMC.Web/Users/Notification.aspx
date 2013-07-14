<%@ Page Title="RMC :: Notification" Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="RMC.Web.Users.Notification" %>
<%@ Register src="../UserControls/SendNotificationForUsers.ascx" tagname="SendNotificationForUsers" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:SendNotificationForUsers ID="SendNotificationForUsers1" runat="server" />
</asp:Content>
