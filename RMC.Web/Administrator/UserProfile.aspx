<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="UserProfile.aspx.cs" Inherits="RMC.Web.Administrator.UserProfile"
    Title="RMC:User Profile" %>

<%@ Register Src="../UserControls/UserProfile.ascx" TagName="UserProfile" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:UserProfile ID="UserProfile1" runat="server" />
</asp:Content>
