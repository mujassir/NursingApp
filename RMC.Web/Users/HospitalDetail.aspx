<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="HospitalDetail.aspx.cs" Inherits="RMC.Web.Users.HospitalDetail" Title="RMC :: Hospital Information" %>

<%@ Register Src="../UserControls/HospitalEditRegistration.ascx" TagName="HospitalEditRegistration"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc1:HospitalEditRegistration ID="HospitalEditRegistration1" runat="server" />
</asp:Content>
