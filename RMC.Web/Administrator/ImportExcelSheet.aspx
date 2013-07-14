<%@ Page Title="RMC :: Import Excel Sheet" Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ImportExcelSheet.aspx.cs" Inherits="RMC.Web.Administrator.ImportExcelSheet" %>
<%@ Register src="../UserControls/ImportExcelSheet.ascx" tagname="ImportExcelSheet" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ImportExcelSheet ID="ImportExcelSheet1" runat="server" />
</asp:Content>
