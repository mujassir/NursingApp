<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DataManagementUnit.aspx.cs" Inherits="RMC.Web.Administrator.DataManagementUnit" Title="Untitled Page" %>
<%@ Register src="../UserControls/DataManagementHospitalUnit.ascx" tagname="DataManagementHospitalUnit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:DataManagementHospitalUnit ID="DataManagementHospitalUnit1" 
        runat="server" />

</asp:Content>
