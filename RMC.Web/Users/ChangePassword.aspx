<%@ Page Title="RMC :: Change Password" Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="RMC.Web.Users.ChangePassword" %>

<%@ Register Src="../UserControls/ChangePassword.ascx" TagName="ChangePassword" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ChangePassword ID="ChangePassword1" runat="server" />
</asp:Content>
