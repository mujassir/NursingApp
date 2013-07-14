<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="AdminUnitType.aspx.cs" Inherits="RMC.Web.Administrator.AdminUnitType"
    Title="RMC :: Unit Type" %>

<%@ Register Src="../UserControls/UnitType.ascx" TagName="UnitType" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:UnitType ID="UnitType1" runat="server" />
</asp:Content>
