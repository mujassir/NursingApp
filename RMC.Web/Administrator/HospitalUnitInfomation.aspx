<%@ Page Language="C#" MasterPageFile="~/Administrator/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="HospitalUnitInfomation.aspx.cs" Inherits="RMC.Web.Administrator.HospitalUnitInfomation"
    Title="RMC :: Hospital Unit Information" %>

<%@ Register Src="../UserControls/HospitalUnitInformation.ascx" TagName="HospitalUnitInformation"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:HospitalUnitInformation ID="HospitalUnitInformation1" runat="server" />
</asp:Content>
