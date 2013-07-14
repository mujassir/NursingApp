<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DataManagementFileList.aspx.cs" Inherits="RMC.Web.Administrator.DataManagementFileList" Title="Untitled Page" %>
<%@ Register src="../UserControls/DataManagementFileList.ascx" tagname="DataManagementFileList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:DataManagementFileList ID="DataManagementFileList1" runat="server" />

</asp:Content>
