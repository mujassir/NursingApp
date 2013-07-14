<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="HospitalUnitInformation.aspx.cs" Inherits="RMC.Web.Administrator.HospitalUnitInformation" Title="RMC :: Hospital Unit Information" %>
<%@ Register src="../UserControls/HospitalUnitInformation.ascx" tagname="HospitalUnitInformation" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    

    <uc1:HospitalUnitInformation ID="HospitalUnitInformation1" runat="server" />

    

</asp:Content>
