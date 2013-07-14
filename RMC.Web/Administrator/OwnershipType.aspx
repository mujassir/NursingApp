<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="OwnershipType.aspx.cs" Inherits="RMC.Web.Administrator.OwnershipType"
    Title="RMC :: Ownership Type" %>

<%@ Register Src="../UserControls/OwnerShipType.ascx" TagName="OwnerShipType" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:OwnerShipType ID="OwnerShipType1" runat="server" />
</asp:Content>
