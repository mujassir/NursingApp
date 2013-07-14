<%@ Page Title="" Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="DeleteNursePDAInfo.aspx.cs" Inherits="RMC.Web.Users.DeleteNursePDAInfo" %>
<%@ Register src="../UserControls/DeleteNursePDAInfo.ascx" tagname="DeleteNursePDAInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:DeleteNursePDAInfo ID="DeleteNursePDAInfo1" runat="server" />

</asp:Content>
