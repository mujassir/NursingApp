<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="HospitalType.aspx.cs" Inherits="RMC.Web.Administrator.HospitalType"
    Title="RMC :: Hospital Type" %>

<%@ Register Src="../UserControls/HospitalType.ascx" TagName="HospitalType" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:HospitalType ID="HospitalType1" runat="server" />
</asp:Content>
