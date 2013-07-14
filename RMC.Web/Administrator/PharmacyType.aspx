<%@ Page Title="RMC :: Pharmacy Type" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="PharmacyType.aspx.cs" Inherits="RMC.Web.Administrator.PharmacyType" %>

<%@ Register Src="../UserControls/PharmacyType.ascx" TagName="PharmacyType" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:PharmacyType ID="PharmacyType1" runat="server" />
</asp:Content>
