<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="RequestApproval.aspx.cs" Inherits="RMC.Web.Administrator.RequestApproval"
    Title="RMC :: Request Approval" %>

<%@ Register Src="../UserControls/RequestApproval.ascx" TagName="RequestApproval"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:RequestApproval ID="RequestApproval1" runat="server" />
</asp:Content>
