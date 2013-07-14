<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" Title="RMC :: Profile Detail"
    AutoEventWireup="true" CodeBehind="ViewProfileDetail.aspx.cs" Inherits="RMC.Web.Users.ViewProfileDetail"  MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../UserControls/ProfileDetail.ascx" TagName="ProfileDetail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ProfileDetail ID="ProfileDetail1" runat="server" />
</asp:Content>
