<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ContactUs.aspx.cs" Inherits="RMC.Web.Users.ContactUs" Title="RMC :: Contact Us" %>

<%@ Register Src="../UserControls/ContactUs.ascx" TagName="ContactUs" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ContactUs ID="ContactUs1" runat="server" />
</asp:Content>
