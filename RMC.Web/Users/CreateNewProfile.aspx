<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="CreateNewProfile.aspx.cs" Inherits="RMC.Web.Users.CreateNewProfile" Title="RMC :: Create New Profile" %>
<%@ Register Src="~/UserControls/CreateNewProfile.ascx" TagName="CreateNewProfile" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:CreateNewProfile ID="CreateNewProfile1" runat="server" />
</asp:Content>
