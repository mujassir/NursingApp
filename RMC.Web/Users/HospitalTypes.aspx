<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="HospitalTypes.aspx.cs" Inherits="RMC.Web.Users.HospitalTypes" Title="RMC :: Hospital Type" %>
<%@ Register src="../UserControls/HospitalType.ascx" tagname="HospitalType" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:HospitalType ID="HospitalType1" runat="server" />
    
</asp:Content>
