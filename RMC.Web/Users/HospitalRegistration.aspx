<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="HospitalRegistration.aspx.cs" Inherits="RMC.Web.Users.HospitalRegistration"
    Title="RMC :: Hospital Registration" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc11" %>
<%@ Register Src="../UserControls/HospitalRegistration.ascx" TagName="HospitalRegistration"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:HospitalRegistration ID="HospitalRegistration1" runat="server" />
</asp:Content>
