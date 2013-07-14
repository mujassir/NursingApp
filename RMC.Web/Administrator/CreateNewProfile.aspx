<%@ Page Title="RMC :: Create a New Profile" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="CreateNewProfile.aspx.cs" Inherits="RMC.Web.Administrator.CreateNewProfile" %>

<%@ Register Src="../UserControls/CreateNewProfile.ascx" TagName="CreateNewProfile"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:CreateNewProfile ID="CreateNewProfile1" runat="server" />
</asp:Content>
