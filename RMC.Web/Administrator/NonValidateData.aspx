<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="NonValidateData.aspx.cs" Inherits="RMC.Web.Administrator.NonValidateData" Title="Untitled Page" %>
<%@ Register src="../UserControls/NonValidatedData.ascx" tagname="NonValidatedData" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:NonValidatedData ID="NonValidatedData1" runat="server" />

</asp:Content>
