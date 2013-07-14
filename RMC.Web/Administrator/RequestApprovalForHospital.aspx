<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RequestApprovalForHospital.aspx.cs" Inherits="RMC.Web.Administrator.RequestApprovalForHospital" Title="Request Approval" %>
<%@ Register src="../UserControls/RequestApprovalForHospital.ascx" tagname="RequestApprovalForHospital" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:RequestApprovalForHospital ID="RequestApprovalForHospital1" 
        runat="server" />
</asp:Content>
