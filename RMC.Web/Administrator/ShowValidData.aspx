<%@ Page Language="C#" MasterPageFile="~/Administrator/MasterPageValidData.Master" AutoEventWireup="true" CodeBehind="ShowValidData.aspx.cs" Inherits="RMC.Web.Administrator.ShowValidData" Title="Untitled Page" %>
<%@ Register src="../UserControls/ShowValidData.ascx" tagname="ShowValidData" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:ShowValidData ID="ShowValidData1" runat="server" />

</asp:Content>
