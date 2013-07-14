<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="DeletecategoryProfile.aspx.cs" Inherits="RMC.Web.Users.DeletecategoryProfile" Title="Untitled Page" %>
<%@ Register src="../UserControls/DeleteCategoryProfile.ascx" tagname="DeleteCategoryProfile" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:DeleteCategoryProfile ID="DeleteCategoryProfile1" runat="server" />
</asp:Content>
